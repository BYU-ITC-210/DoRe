using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using Bredd.CodeBit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;

namespace DnsForItLearningLabs;

public static class UtilFunction {
    [Function("UtilFunction")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "util")] HttpRequest req) {
        if (req.Method == "GET") {
            return new ContentResult() {
                StatusCode = StatusCodes.Status200OK,
                ContentType = MediaTypeNames.Text.Html,
                Content = c_utilPage
            };
        }

        switch (req.Form["action"]) {
        case "hashPassword":
            return HashPasswordAction(req);

        case "generateKey":
            return GenerateKeyAction(req);
        }

        return DefaultAction(req);
    }

    static IActionResult HashPasswordAction(HttpRequest req) {
        return new ContentResult() {
            ContentType = MediaTypeNames.Text.Plain,
            Content = string.Format(c_hashPasswordContent, req.Form["un"], PasswordHash.Hash((string?)req.Form["pw"] ?? string.Empty), req.Form["accountType"])
        };
    }

    static IActionResult GenerateKeyAction(HttpRequest req) {
        var key = WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(24));
        var url = string.Concat(req.Scheme, "://", req.Host, "/api/lti");
        return new ContentResult() {
            ContentType = MediaTypeNames.Text.Plain,
            Content = string.Format(c_generateKeyContent, req.Form["keyName"], key, url)
        };
    }

    static IActionResult DefaultAction(HttpRequest req) {
        var sb = new StringBuilder();
        foreach (var pair in req.Form) {
            sb.AppendLine($"{pair.Key}={pair.Value}");
        }

        return new ContentResult() {
            StatusCode = StatusCodes.Status200OK,
            ContentType = MediaTypeNames.Text.Plain,
            Content = sb.ToString()
        };
    }

    const string c_utilPage =
@"<!DOCTYPE html>
<html>
<head>
    <title>Utility</title>
    <style>
    form {
        border: 1px solid black;
        max-width: 24em;
        margin: 1em;
        padding: 0.5em;
    }
    </style>
</head>
<body>

<form method='post' enctype='application/x-www-form-urlencoded'>
    <h2>Hash Password</h2>
    <h3>(Create a test or admin account.)</h3>
    <input type='hidden' name='action' value='hashPassword'/>
    <label for=""accountType"">Account Type:</label>
    <select name=""accountType"" id=""accountType"">
        <option value=""user"">user</option>
        <option value=""admin"">admin</option>
    </select><br/>
    <label for='un'>Username:</label> <input type='text' id='un' name='un'/><br/>
    <label for='pw'>Password:</label> <input type='text' id='pw' name='pw'/><br/>
    <input type='submit'/>
</form>

<form method='post' enctype='application/x-www-form-urlencoded'>
    <h2>Generate Key for LTI Connection</h2>
    <input type='hidden' name='action' value='generateKey'/>
    <label for='keyName'>Key Name:</label> <input type='text' id='keyName' name='keyName'/><br/>
    <input type='submit'/>
</form>

</body>
</html>";


    const string c_hashPasswordContent =
@"Direct accounts should only be used for administraton and testing. Most accounts should authenticate through LTI.

Use the Azure Portal to add the following tag to the DNS service:
name:  user_{0}
value: {2};{1}";

    const string c_generateKeyContent =
@"This key will grant a Tool Consumer such as an LMS access to the DNS tool. First, use the Azure Portal to add the following tag to the DNS service:
name:  lti_{0}
value: {1}

Next in your LMS create an LTI link with the following information:
url:    {2}
key:    {0}
secret: {1}
";

}
