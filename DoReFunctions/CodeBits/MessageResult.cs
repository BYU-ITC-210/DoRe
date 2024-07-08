using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Bredd.CodeBit;

class MessageResult : ActionResult {
    // Do the preconfigured results this way to minimize Functions startup time

    static MessageResult? s_notFoundResult;
    public static MessageResult NotFoundResult {
        get {
            if (s_notFoundResult is null)
                s_notFoundResult = new MessageResult(StatusCodes.Status404NotFound, c_notFoundMessage);
            return s_notFoundResult;
        }
    }

    static MessageResult? s_unauthorizedResult;
    public static MessageResult UnauthorizedResult {
        get {
            if (s_unauthorizedResult is null)
                s_unauthorizedResult = new MessageResult(StatusCodes.Status401Unauthorized, c_unauthorizedMessage);
            return s_unauthorizedResult;
        }
    }

    static MessageResult? s_unauthorizedCookieResult;
    public static MessageResult UnauthorizedCookieResult {
        get {
            if (s_unauthorizedCookieResult is null)
                s_unauthorizedCookieResult = new MessageResult(StatusCodes.Status401Unauthorized, c_unauthorizedCookieMessage);
            return s_unauthorizedCookieResult;
        }
    }

    static MessageResult? s_unauthorizedHeaderResult;
    public static MessageResult UnauthorizedHeaderResult {
        get {
            if (s_unauthorizedHeaderResult is null)
                s_unauthorizedHeaderResult = new MessageResult(StatusCodes.Status401Unauthorized, c_unauthorizedHeaderMessage);
            return s_unauthorizedHeaderResult;
        }
    }

    static MessageResult? s_forbiddenResult;
    public static MessageResult ForbiddenResult {
        get {
            if (s_forbiddenResult is null)
                s_forbiddenResult = new MessageResult(StatusCodes.Status403Forbidden, c_forbiddenMessage);
            return s_forbiddenResult;
        }
    }

    static MessageResult? s_existsConflictResult;
    public static MessageResult ExistsConflictResult {
        get {
            if (s_existsConflictResult is null)
                s_existsConflictResult = new MessageResult(StatusCodes.Status409Conflict, c_existsConflictMessage);
            return s_existsConflictResult;
        }
    }

    static MessageResult? s_deletedResult;
    public static MessageResult DeletedResult {
        get {
            if (s_deletedResult is null)
                s_deletedResult = new MessageResult(StatusCodes.Status200OK, c_deletedMessage);
            return s_deletedResult;
        }
    }

    static MessageResult? s_corsOriginResult;
    public static MessageResult CorsOriginResult {
        get {
            if (s_corsOriginResult is null)
                s_corsOriginResult = new MessageResult(StatusCodes.Status400BadRequest, c_corsOriginMessage);
            return s_corsOriginResult;
        }
    }


    int m_statusCode;
    string m_message;

    public MessageResult(int statusCode, string message) {
        m_statusCode = statusCode;
        m_message = message;
    }

    public override Task ExecuteResultAsync(ActionContext context) {
        var res = context.HttpContext.Response;
        string body;
        if (IsHtmlPreferred(context)) {
            res.ContentType = "text/html";
            body = string.Format(c_htmlBody, m_statusCode,
                ((System.Net.HttpStatusCode)(m_statusCode)).ToString(),
                m_message, HttpUtility.HtmlEncode(m_message));
        }
        else {
            res.ContentType = "application/json";
            body = string.Format(c_jsonBody, m_statusCode,
                ((System.Net.HttpStatusCode)(m_statusCode)).ToString(),
                m_message, System.Text.Json.JsonEncodedText.Encode(m_message));
        }

        res.StatusCode = m_statusCode;
        return res.WriteAsync(body);
    }

    protected static bool IsHtmlPreferred(ActionContext context) {
        var req = context.HttpContext.Request;
        var reqContentType = req.ContentType;
        if (!(reqContentType is null) && reqContentType.StartsWith("application/json"))
            return false;

        float htmlq = 0.0F;
        float jsonq = 0.001F;
        foreach (var header in req.Headers["Accept"]) {
            foreach (var value in header!.Split(',')) {
                var semi = value.IndexOf(';');
                var contentType = (semi > 0) ? value.Substring(0, semi).Trim() : value.Trim();
                float q = 1.0F;
                if (semi > 0 && value.Substring(semi + 1, 2) == "q=" && float.TryParse(value.Substring(semi + 3), out float qf))
                    q = qf;

                switch (contentType) {
                case "text/html":
                    htmlq = q;
                    break;

                case "application/json":
                    jsonq = q;
                    break;
                }
            }
        }
        return htmlq > jsonq;
    }

    const string c_notFoundMessage = "Resource not found.";
    const string c_unauthorizedCookieMessage = "Authentication cookie not found or expired.";
    const string c_unauthorizedHeaderMessage = "Authentication header not found or expired.";
    const string c_unauthorizedMessage = "Authentication cookie or header not found or expired.";
    const string c_forbiddenMessage = "Account is not authorized to access this resource.";
    const string c_existsConflictMessage = "Creation failure, resource already exists.";
    const string c_deletedMessage = "Deleted.";
    const string c_corsOriginMessage = "CORS Origin header required for authentication.";

    const string c_htmlBody =
@"<!DOCTYPE html>
<html>
    <head>
        <title>{1}</title>
    </head>
    <body>
        <h1>{0} {1}</h1>
        <p>{2}</p>
    </body>
</html>";

    const string c_jsonBody =
@"{{
  ""status"": ""{0}"",
  ""title"": ""{1}"",
  ""message"": ""{2}""
}}";

} // Class MessageResult

class PreflightResult : ActionResult {
    public PreflightResult() {
        AllowMethods = "GET,POST,PUT,DELETE,OPTIONS";
        AllowHeaders = "Authorization,Content-Type";
        AllowCredentials = true;
        ExposeHeaders = "Authentication-Info";
    }

    public string AllowMethods { get; set; }
    public string AllowHeaders { get; set; }
    public bool AllowCredentials { get; set; }
    public string ExposeHeaders { get; set; }

    public override void ExecuteResult(ActionContext context) {
        var req = context.HttpContext.Request;
        var res = context.HttpContext.Response;
        res.StatusCode = 204;
        res.Headers["Access-Control-Allow-Origin"] = req.Headers["Origin"];
        res.Headers["Access-Control-Allow-Methods"] = AllowMethods;
        res.Headers["Access-Control-Allow-Headers"] = AllowHeaders;
        res.Headers["Access-Control-Allow-Credentials"] = AllowCredentials ? "true" : "false";
        res.Headers["Access-Control-Expose-Headers"] = ExposeHeaders;
        res.Headers["Access-Control-Max-Age"] = "86400"; // One day.
    }
}
