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

namespace DnsForItLearningLabs;

/// <summary>
/// Back door login for testing purposes
/// </summary>
public class LoginFunction {
    ILogger<LoginFunction> m_logger;

    public LoginFunction(ILogger<LoginFunction> logger) {
        m_logger = logger;
    }

    [Function("Login")]
    public Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "options", Route = "api/login")] HttpRequest req) {
        {
            var result = AccessControl.AuthorizeAnonymousRequest(req);
            if (result is not null)
                return Task.FromResult(result);
        }

        if (req.Method == "POST") {
            if (req.HasFormContentType) {
                return LoginForm(req);
            }
            if (req.HasJsonContentType()) {
                return LoginJson(req);
            }
            return Task.FromResult<IActionResult>(new MessageResult(StatusCodes.Status401Unauthorized, "Invalid username or password."));
        }

        var authToken = AccessControl.GetAuthentication(req);
        if (authToken is not null) {
            if (req.Query.ContainsKey("logout")) {
                m_logger.LogInformation("Logout.");
                AccessControl.ClearAuthentication(req.HttpContext);
                return Task.FromResult<IActionResult>(new ContentResult() {
                    Content = "Logged out.",
                    ContentType = "text/plain"
                });
            }

            // This is mostly for debugging purposes; we don't have anything else to return in this case.
            return Task.FromResult<IActionResult>(new JsonResult(authToken));
        }

        return Task.FromResult<IActionResult>(new SimpleLoginResult(req.Host.ToString()));
    }

    Task<IActionResult> LoginForm(HttpRequest req) {
        var token = ValidateUnPw(req.Form["username"], req.Form["password"]);
        if (token is not null) {
            m_logger.LogInformation(new EventId(3, "Login"), "un={un}", token["un"]);
            var signedToken = AccessControl.SetAuthHeader(token, req.HttpContext);
            return Task.FromResult<IActionResult>(
                new JsonResult(new { accessToken = signedToken, tokenType = "Bearer", expiresIn = (int)token.ExpiresIn.TotalSeconds })
            );
        }

        return Task.FromResult<IActionResult>(
            new SimpleLoginResult(req.Host.ToString()) { TryAgain = true }
        );
    }

    record LoginReqModel(string Password, string Username);

    async Task<IActionResult> LoginJson(HttpRequest req) {
        var login = (await JsonSerializer.DeserializeAsync<LoginReqModel>(req.Body, JsonHelp.SerializerOptions))!;
        var token = ValidateUnPw(login.Username, login.Password);
        if (token is null)
            return new MessageResult(StatusCodes.Status401Unauthorized, "Invalid username or password.");
        var signedToken = AccessControl.SetAuthHeader(token, req.HttpContext);

        m_logger.LogInformation(new EventId(3, "Login"), "un={un}", login.Username);
        return new JsonResult(new { accessToken = signedToken, tokenType = "Bearer", expiresIn = (int)token.ExpiresIn.TotalSeconds });
    }

    static SessionToken? ValidateUnPw(string? un, string? pw) {
        if (un is null || pw is null)
            return null;
        // See if there's a matching record
        var tag = AzureDns.ReadTag("user_" + un);
        if (string.IsNullOrWhiteSpace(tag))
            return null;

        var semi = tag.IndexOf(';');
        if (semi < 0)
            return null;
        var role = tag.Substring(0, semi);
        var hash = tag.Substring(semi + 1);

        // Validate the password
        if (!PasswordHash.Validate(pw, hash))
            return null;

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

}
