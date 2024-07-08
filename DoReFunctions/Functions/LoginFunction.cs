using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Bredd.CodeBit;
using FileMeta.CodeBit;
using System.Text;
using System.Text.Json;

namespace DnsForItLearningLabs
{
    /// <summary>
    /// Back door login for testing purposes
    /// </summary>
    public class LoginFunction
    {
        const string c_unHashPassword = "HashPassword";

        ILogger<LoginFunction> m_logger;

        public LoginFunction(ILogger<LoginFunction> logger, IServiceProvider serviceProvider) {
            m_logger = logger;
            Console.WriteLine(serviceProvider?.GetType().FullName);
        }

        [Function("Login")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "options", Route = "api/login")] HttpRequest req)
        {
            {
                var result = AccessControl.AuthorizeAnonymousRequest(req);
                if (result is not null) return result;
            }

            if (req.Method == "POST" && req.ContentType.StartsWith("application/json"))
            {
                return await LoginJson(req);
            }

            var authToken = AccessControl.GetAuthentication(req);
            if (authToken is not null)
            {
                if (req.Query.ContainsKey("logout"))
                {
                    m_logger.LogInformation("Logout.");
                    AccessControl.ClearAuthCookie(req.HttpContext);
                    return new ContentResult()
                    {
                        Content = "Logged out.",
                        ContentType = "text/plain"
                    };
                }

                var sb = new StringBuilder();
                foreach (var datum in req.HttpContext.GetSessionToken())
                {
                    sb.Append($"{datum.Key}={datum.Value} ");
                }
                return new ContentResult()
                {
                    Content = string.Format(c_authenticated, HttpUtility.HtmlEncode(sb.ToString())),
                    ContentType = "text/html"
                };
            }

            bool loginFailure = false;
            if (req.HasFormContentType)
            {
                string un = req.Form["username"];
                string pw = req.Form["password"];

                // Generate a password hash for direct login purposes
                if (string.Equals(un, c_unHashPassword) && !string.IsNullOrWhiteSpace(pw))
                {
                    return new ContentResult()
                    {
                        Content = $"hash={PasswordHash.Hash(pw)}",
                        ContentType = "text/plain"
                    };
                }

                var token = ValidateUnPw(un, pw);
                if (token is not null)
                {
                    var signedToken = AccessControl.SignToken(req.HttpContext, token);

                    m_logger.LogInformation(new EventId(3, "Login"), "un={un}", un);

                    return new JsonAccessTokenResult(signedToken, AccessControl.TokenExpiration);
                }

                loginFailure = true;
            }

            return new SimpleLoginResult(req.Host.ToString())
            {
                TryAgain = loginFailure
            };
        }

        async Task<IActionResult> LoginJson(HttpRequest req)
        {
            var login = await JsonSerializer.DeserializeAsync<LoginReqModel>(req.Body, JsonHelp.SerializerOptions);
            var token = ValidateUnPw(login.Username, login.Password);
            if (token is null) return new MessageResult(StatusCodes.Status401Unauthorized, "Invalid username or password.");
            var signedToken = AccessControl.SignToken(req.HttpContext, token);

            m_logger.LogInformation(new EventId(3, "Login"), "un={un}", login.Username);

            return new JsonAccessTokenResult(signedToken, AccessControl.TokenExpiration);
        }

        static SessionToken ValidateUnPw(string un, string pw)
        {
            // See if there's a matching record
            var tag = AzureDns.ReadTag("user_" + un);
            if (string.IsNullOrWhiteSpace(tag)) return null;

            var semi = tag.IndexOf(';');
            if (semi < 0) return null;
            var role = tag.Substring(0, semi);
            var hash = tag.Substring(semi + 1);

            // Validate the password
            if (!PasswordHash.Validate(pw, hash)) return null;

            return new SessionToken
                {
                    { "un", un },
                    { "r", role }
                };
        }


        const string c_authenticated =
@"<!DOCTYPE html>
<html>
<head>
<title>Authenticated</title>
</head>
<body>
<div>Authenticated: {0}</div>
<div><a href='?logout=1'>Logout</a></div>
</body>
</html>
";

        class LoginReqModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        // This matches the standard OAuth response per https://www.oauth.com/oauth2-servers/access-tokens/access-token-response/
        class LoginResModel
        {
            public string access_token { get; set; }

            public string token_type { get; set; }

            // Expiration time in seconds
            public int expires_in { get; set; }
        }

    }
}
