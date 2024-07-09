// When published as a CodeBit, this will be Bredd.AzureFunctions.AccessControl

using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FileMeta.CodeBit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging.Abstractions;

namespace Bredd.CodeBit;

/// <summary>
/// Access Control for Azure Functions
/// </summary>
/// <remarks>
/// <para>Provides access control by storing session tokens in two ways. The two
/// methods can be used together but usually one or the other will be used.
/// </para>
/// <para>For a UI application directly accessing date, session tokens are transmitted
/// in cookies through the use of <see cref="SetAuthCookie(SessionToken, HttpContext)"/>
/// </para>
/// <para>For an API back-end, session tokens are generated through the use of
/// <see cref="SignToken(HttpContext, SessionToken)"/> and expected to be included in
/// an Authentication Bearer header.
/// </para>
/// <para>The API authentication method handles CORS security requirements. It secures
/// cross-origin API calls by requiring that the origin of any request be the same as
/// the origin that posted the authentication credentials.
/// </para>
/// </remarks>
static class AccessControl {
    static readonly TimeSpan c_keyRenewalInterval = TimeSpan.FromDays(180);

    const string c_accessControlLogCategory = "AccessControl";
    const string c_keyFileName = "fackey.txt";
    const string c_keyFileDir = "data"; // Relative to %HOME%
    const string c_authCookie = "SessionToken";
    const string c_bearerPrefix = "Bearer ";
    const string c_itemsTokenKey = "_SessionToken_";
    const string c_authorizationHeader = "Authorization";
    const string c_authenticationInfoHeader = "Authentication-Info";

    static readonly Encoding s_UTF8 = new UTF8Encoding(false);

    static bool s_initialized = false;
    static SessionTokenManager s_tokenMgr = new SessionTokenManager();
    static ILogger s_log = NullLogger.Instance;

    #region Initialization

    /// <summary>
    /// Initialize the AccessControl system
    /// </summary>
    /// <remarks>
    /// <para>You may initialize in the program initialization or in a Function (Azure Functions entry point) that
    /// makes use of AccessControl. Program-level initialization is best. The system tolerates multiple calls to
    /// initialize with very small performance impact so, if necessary, you can initialize in multiple places.
    /// </para>
    /// <para>Program level initialization.</para>
    /// <code>
    /// var host = new HostBuilder()
    /// // Host configuration goes here.
    /// 
    /// // Initialization here
    /// AccessControl.Initialize(host.Services);
    /// 
    /// // Start the system
    /// host.Run()
    /// </code>
    /// 
    /// <para>Function level initialization</para>
    /// <code>
    /// class SomeFunction {
    /// 
    ///      public SomeFunction(IServiceProvider serviceProvider) {
    ///          AccessControl.Initialize(serviceProvider);
    ///      }
    ///      
    ///      [Function("SomeFunction")]
    ///      public Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "options", Route = "api/login")] HttpRequest req)) {
    ///          // Do stuff
    ///      }
    /// }
    /// </code>
    /// 
    /// <para>Within function initialization</para>
    /// <code>
    /// class SomeFunction {
    /// 
    ///      [Function("SomeFunction")]
    ///      public Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "options", Route = "api/login")] HttpRequest req)) {
    ///          AccessControl.Initialize(req.Context);
    ///          
    ///          // Do stuff
    ///      }
    /// }
    /// </code>
    /// </remarks>

    public static void Initialize(IServiceProvider serviceProvider) {
        if (s_initialized)
            return;

        // Get the log
        s_log = serviceProvider.GetService<ILoggerFactory>()!.CreateLogger(c_accessControlLogCategory);

        ReadKeys(s_tokenMgr);

        s_log.LogInformation($"AccessControl: Initialized with {s_tokenMgr.KeyCount} keys.");
        s_initialized = true;
    }

    public static void Initialize(HttpContext context) {
        Initialize(context.RequestServices);
    }

    private static void UpdateKeys() {
        ReadKeys(s_tokenMgr);
        s_log.LogInformation($"AccessControl: Update keys, count={s_tokenMgr.KeyCount}.");
    }

    #endregion Initialization

    #region Public Interface

    public static TimeSpan TokenExpiration { get { return s_tokenMgr.DefaultExpiration; } set { s_tokenMgr.DefaultExpiration = value; } }

    /// <summary>
    /// If authenticated, get the <see cref="SessionToken"/> for the current session.
    /// </summary>
    /// <returns>A <see cref="SessionToken"/> if authenticated. Null if not authenticated.</returns>
    public static SessionToken? GetAuthentication(HttpRequest req) {
        // Look for an authentication cookie
        var cookie = req.Cookies[c_authCookie];
        if (!string.IsNullOrEmpty(cookie)) {
            var token = ReadToken(req.HttpContext, cookie);
            if (token.IsValid) {
                // Refresh the cookie (so it doesn't expire)
                RefreshCookie(req.HttpContext, token);
                return token;
            }
        }

        // Look for an Authorization header
        foreach (var value in req.Headers[c_authorizationHeader]) {
            if (value!.StartsWith(c_bearerPrefix, StringComparison.OrdinalIgnoreCase)) {
                var token = ReadToken(req.HttpContext, value.Substring(c_bearerPrefix.Length).Trim());
                if (token.IsValid) {
                    RefreshToken(req.HttpContext, token);
                    return token;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Sign a token so that it can be used for future authentication.
    /// </summary>
    /// <param name="context">The context of the current http operation.</param>
    /// <param name="token">The <see cref="SessionToken"/> to be signed.</param>
    /// <returns>A string with the signed contents of the token.</returns>
    /// <remarks>
    /// Typically this would be used by a login or authentication function to generate a token
    /// after credentials have been validated. The token would be returned in a JSON
    /// response that would subsequently be send in an Authorization header.
    /// </remarks>
    public static string SignToken(HttpContext context, SessionToken token) {
        if (!token.ContainsKey("o")) {
            token["o"] = GetOrigin(context.Request);
        }
        return s_tokenMgr.SignToken(token);
    }

    /// <summary>
    /// Read (parse) a token and validate its signature.
    /// </summary>
    /// <param name="context">The context of the current http operation.</param>
    /// <param name="signedToken">A string containing a signed token</param>
    /// <returns>A <see cref="SessionToken"/> containing the unpacked token.</returns>
    /// <remarks>
    /// A token will always be returned. Be sure to check <see cref="SessionToken.IsValid"/> to
    /// see if it has a valid signature and has not expired.
    /// </remarks>
    public static SessionToken ReadToken(HttpContext context, string signedToken) {
        // If empty, return an invalid token
        if (string.IsNullOrEmpty(signedToken))
            return new SessionToken() {
                Status = SessionTokenStatus.Invalid
            };

        var token = s_tokenMgr.Decode(signedToken);

        // If key not found, reload keys and try again
        if (token.Status == SessionTokenStatus.KeyNotFound) {
            UpdateKeys();
            token = s_tokenMgr.Decode(signedToken);
        }

        // Check for origin match
        if (token.Status == SessionTokenStatus.Valid) {
            string? tokenOrigin;
            if (!token.TryGetValue("o", out tokenOrigin))
                tokenOrigin = string.Empty;
            if (!string.Equals(tokenOrigin, GetOrigin(context.Request)))
                token.Status = SessionTokenStatus.OriginMismatch;
        }

        return token;
    }

    /// <summary>
    /// Sign a token and set it as an authentication token cookie to be returned with the response.
    /// </summary>
    /// <param name="token">The <see cref="SessionToken"/> to be signed and set.</param>
    /// <param name="context">The <see cref="HttpContext"/> of the current request.</param>
    public static void SetAuthCookie(SessionToken token, HttpContext context) {
        var signed = s_tokenMgr.SignToken(token);
        var co = new CookieOptions();
        co.Expires = token.Expiration;
        co.Path = "/";
        context.Response.Cookies.Append(c_authCookie, signed, co);
    }

    /// <summary>
    /// Sign a token and set the Authentication-Info header with its value 
    /// </summary>
    /// <param name="token">The <see cref="SessionToken"/> to be signed and set.</param>
    /// <param name="context">The <see cref="HttpContext"/> of the current request.</param>
    /// <returns>The signed token in string form.</returns>
    public static string SetAuthHeader(SessionToken token, HttpContext context) {
        var signed = SignToken(context, token);
        var res = context.Response;
        res.Headers[c_authenticationInfoHeader] = "Bearer-Update = " + signed;
        res.Headers["Access-Control-Expose-Headers"] = c_authenticationInfoHeader;
        return signed;
    }

    /// <summary>
    /// Clear authentication cookie and/or authentication header.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> of the current request.</param>
    /// <remarks>This is typically used to log out the user.</remarks>
    public static void ClearAuthentication(HttpContext context) {
        if (context.Request.Cookies.ContainsKey(c_authCookie))
            context.Response.Cookies.Delete(c_authCookie);
        if (context.Request.Headers.ContainsKey(c_authorizationHeader))
            context.Response.Headers[c_authenticationInfoHeader] = "Bearer-Update = clear";
    }

    /// <summary>
    /// Handle CORS protocol for an anyonymous request
    /// </summary>
    /// <param name="req">An <see cref="HttpRequest"/>.</param>
    /// <returns>An <see cref="IActionResult"/> with the appropriate response if this was a preflight request.</returns>
    /// <remarks>
    /// <para>This is typically used on a Login api that requires CORS approval when
    /// submitting credentials. It handles a CORS preflight request by returning the appropriate
    /// response.
    /// </para>
    /// <para>If a regular (not preflight) request, checks for an Origin header. Returns an error
    /// if it's not present
    /// </para>
    /// <para>Adds the Access-Control-Allow-Origin and Access-Control-Expose-Headers headers so that
    /// API requests are accepted.
    /// </para>
    /// </remarks>
    public static IActionResult? AuthorizeAnonymousRequest(HttpRequest req) {
        if (req.Method == "OPTIONS")
            return new PreflightResult();

        var origin = GetOrigin(req);
        if (origin is null)
            return MessageResult.CorsOriginResult;

        var res = req.HttpContext.Response;
        res.Headers["Access-Control-Allow-Origin"] = origin;
        res.Headers["Access-Control-Expose-Headers"] = c_authenticationInfoHeader;
        return null;
    }

    /// <summary>
    /// Authenticate an http request.
    /// </summary>
    /// <param name="req"></param>
    /// <returns>True if authentication successes. Else, false.</returns>
    /// <remarks>
    /// <para>Looks in the cookies and the headers for a valid authentication token.
    /// </para>
    /// <para>If a token is found in the cookies, the cookie expiration is renewed.
    /// </para>
    /// <para>The unpacked <see cref="SessionToken"/> is stored in
    /// <see cref="HttpContext.Items"/>[<see cref="AccessControl.c_itemsTokenKey"/>] for
    /// future reference.
    /// </para>
    /// <para>Typically, a false return will result in sending an <see cref="UnauthorizedMessageResult"/> as follows:
    /// </para>
    /// <code>if (!AuthenticateRequest(req)) return new UnauthorizedMessageResult();
    /// </code>
    /// </remarks>
    public static IActionResult? AuthenticateRequest(HttpRequest req) {
        if (req.Method == "OPTIONS") {
            return new PreflightResult();
        }

        var token = GetAuthentication(req);
        if (null == token) {
            return MessageResult.UnauthorizedResult;
        }

        var res = req.HttpContext.Response;
        if (token.TryGetValue("o", out string? tokenOrigin))
            res.Headers["Access-Control-Allow-Origin"] = tokenOrigin;

        req.HttpContext.Items[c_itemsTokenKey] = token;
        return null;
    }

    /// <summary>
    /// Returns the current valid session token, if any, in string form.
    /// </summary>
    /// <param name="context">The current HttpContext</param>
    /// <returns>The token or null</returns>
    public static SessionToken? GetSessionToken(this HttpContext context) {
        return context.Items[c_itemsTokenKey] as SessionToken;
    }

    #endregion Public Interface

    /*****************************
     * Key Rotation Algorithm
     * 
     * When scaled up, there may be multiple instances running at the same time. They all need to be using
     * the same keys. Keys automatically rotate according to c_keyRenewalInterval but new keys are only
     * generated at load time. Theoretically, if an instance were constantly active for a very long time
     * a key could be active for that period. But in practice, there are idle times and Azure seems
     * to rotate instances.
     * 
     * The key file is stored in Azure-designated data folder: %HOME%/data.
     * 
     * At initialization, the application opens the key file with exclusive access (FileShare.None). If the
     * file doesn't exist, it is created. It then reads all keys from the file. Each key has an id (0-31),
     * a value, and a creation date.
     * 
     * If no keys are read, or if the creation date of the newest key is older than c_keyRenewalInterval
     * then SessionTokenManager.RotateKeys is called to generate a new key and (possibly) rotate out an
     * old one. Then, all keys are immediately written back to the file before it is closed.
     * 
     * An optimistic concurrency algorithm is used. If the file fails to open due to it being locked by
     * another instance, then the application waits 250 ms and tries again.
     * 
     * Importantly, read, key generation, and write are all done on one open file session thereby preventing
     * another instance from generating new keys at the same time.
     * 
     * It is possible that a new instance comes up and rotates keys while an existing instance is still
     * using an old keyset. In that case, the existing instance could get a token with a newer key that's
     * not in the keyset. When a keyId doesn't match any keys, then new keys are read from the file and
     * the token decode is tried again.
     *****************************/

    private static void ReadKeys(SessionTokenManager tm) {
        for (int tries = 0; tries < 3; ++tries) {
            try {
                using (var file = new FileStream(KeyFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)) {
                    using (var reader = new StreamReader(file, s_UTF8, true, -1, true)) {
                        for (; ; )
                        {
                            var line = reader.ReadLine();
                            if (line is null)
                                break;
                            tm.AddKey(new SessionTokenKey(line));
                        }
                    }

                    TimeSpan keyAge = DateTime.UtcNow - (tm.NewestKey?.Created ?? DateTime.MinValue);

                    // We only rotate keys at renewal time. If there was continuous
                    // use of the function for six months then keys wouldn't renew.
                    // But that's highly unlikely.
                    if (keyAge > c_keyRenewalInterval) {
                        tm.RotateKeys();

                        file.Position = 0;
                        file.SetLength(0);
                        using (var writer = new StreamWriter(file, s_UTF8, -1, true)) {
                            foreach (var key in tm.GetKeys()) {
                                writer.WriteLine(key.ToString());
                            }
                        }
                    }
                }

                break;
            }
            catch (DirectoryNotFoundException) {
                Directory.CreateDirectory(Path.GetDirectoryName(KeyFilePath)!);
                Console.WriteLine("Directory Created.");
            }
            catch (IOException) {
                if (tries == 0)
                    throw;
                // Typically happens if the file is in use by another process.
                System.Threading.Thread.Sleep(250); // Wait for a quarter second and try again.
            }
        }
    }

    static string? s_keyFilePath;

    // On Azure, files in %HOME%/data are shared between instances of the function app.
    static string KeyFilePath {
        get {
            if (s_keyFilePath is not null)
                return s_keyFilePath;
            if (string.Equals(Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT", EnvironmentVariableTarget.Process),
                "Development", StringComparison.OrdinalIgnoreCase)) {
                s_keyFilePath = Path.Combine(Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.Process)!,
                    string.Concat(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, "\\", c_keyFileName));
            }
            else {
                s_keyFilePath = Path.Combine(Environment.GetEnvironmentVariable("HOME", EnvironmentVariableTarget.Process)!,
                    c_keyFileDir, c_keyFileName);
            }
            return s_keyFilePath;
        }
    }

    private static void RefreshCookie(HttpContext http, SessionToken token) {
        // Refresh the cookie (so it doesn't expire)
        (var refreshed, var expiration) = s_tokenMgr.RefreshEx(token);
        var co = new CookieOptions();
        co.Expires = expiration;
        co.Path = "/";
        http.Response.Cookies.Append(c_authCookie, refreshed, co);
    }

    private static void RefreshToken(HttpContext http, SessionToken token) {
        // Refresh the toke (so it doesn't expire)
        // This only works if the client recognizes Authentication-Info: Refresh-Token
        var refreshed = s_tokenMgr.Refresh(token);
        var res = http.Response;
        res.Headers[c_authenticationInfoHeader] = "Bearer-Update = " + refreshed;
        res.Headers["Access-Control-Expose-Headers"] = c_authenticationInfoHeader;
    }

    private static string GetOrigin(HttpRequest req) {
        if (req.Headers.TryGetValue("Origin", out StringValues value) && !string.Equals(value, "null")) // Under certain circumstances, the value is the literal string "null"
        {
            return value!;
        }

        return $"{req.Scheme}://{req.Host}";
    }

} // Class AccessControl
