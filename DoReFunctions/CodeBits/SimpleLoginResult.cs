using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bredd.CodeBit;

/// <summary>
/// Generate a simple login HTML page.
/// </summary>
/// <remarks>
/// The page will return a username and password in application/x-www-form-urlencoded format.
/// </remarks>
class SimpleLoginResult : ActionResult {
    string m_title;
    string m_message;

    public SimpleLoginResult(string title) {
        m_title = title;
        m_message = string.Empty;
        ActionPath = string.Empty;
    }

    public string ActionPath { get; set; }

    public bool TryAgain {
        get { return string.Equals(m_message, c_tryAgain, StringComparison.Ordinal); }
        set { m_message = value ? c_tryAgain : string.Empty; }
    }

    public string Message {
        get { return m_message; }
        set { m_message = value; }
    }

    public async override Task ExecuteResultAsync(ActionContext context) {
        var action = ActionPath ?? context.HttpContext.Request.Path.ToString();

        var res = context.HttpContext.Response;
        res.StatusCode = 200;
        await res.WriteAsync(c_response0);
        await res.WriteAsync(string.Format(c_response1, action));
        await res.WriteAsync(string.Format(c_response2, HttpUtility.HtmlEncode(m_title)));
        if (!string.IsNullOrEmpty(m_message))
            await res.WriteAsync(string.Format(c_response2_msg, m_message));
        await res.WriteAsync(c_response3);
    }

    const string c_tryAgain = "Try again.";

    const string c_response0 =
@"
<!DOCTYPE html>
<html>
<head>
<title>Login</title>
<style>
body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}
form {
    border: 1px solid black;
    border-radius: 5px;
    margin: 1em auto;
    padding: 1em;
    width: 25em;
    background-color: #E0E0E0;
}
#title {
    text-align: center;
    display: block;
    margin: 0;
}
#message {
    color: darkred;
    text-align: center;
}
table {
    margin: 0 auto;
}
#username, #password {
    width: 20em;
}
#submit {
    display: block;
    margin: 0 auto;
    background-color: white;
    border-radius: 5px;
    font-family: inherit;
    font-size: inherit;
}

</style>
</head>
<body>";
    const string c_response1 = @"<form action='{0}' method='post'>";
    const string c_response2 = @"<h2 id='title'>{0}</h2>";
    const string c_response2_msg = @"<div id='message'>{0}</div>";
    const string c_response3 =
@"<table>
<tr><td><label for='username'>Username:</label></td><td><input type='text' id='username' name='username'/></td></tr>
<tr><td><label for='password'>Password:</label></td><td><input type='password' id='password' name='password'/></td></tr>
</table>
<input id='submit' type='submit' value='Login'/>
</form>
</body>
</html>";
}
