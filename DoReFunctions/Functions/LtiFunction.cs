using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Bredd.CodeBit;
using System.Net.Mime;
using FileMeta.CodeBit;
using Microsoft.Extensions.Primitives;

namespace DnsForItLearningLabs
{
    public class LtiFunction
    {
        const string c_invalidLtiMessage = "Invalid LTI Authentication.";

        ILogger<LtiFunction> m_logger;
        public LtiFunction(ILogger<LtiFunction> logger)
        {
            m_logger = logger;
        }

        [Function("LtiFunction")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "options", Route = "api/lti")] HttpRequest req)
        {
            // Provide CORS support
            {
                var result = AccessControl.AuthorizeAnonymousRequest(req);
                if (result is not null) return result;
            }

            if (!AuthenticateLti(req))
            {
                return new MessageResult(StatusCodes.Status401Unauthorized, c_invalidLtiMessage);
            }

            var st =new SessionToken
            {
                { "un", (string?)req.Form["user_id"] ?? string.Empty },
                { "r", "user" },
                { "n", (string?)req.Form["lis_person_name_full"] ?? string.Empty },
                { "o", $"{req.Scheme}://{req.Host}" } // Set origin to the destination, not the origin of this call
            };

            m_logger.LogInformation(new EventId(4, "LTI"), "un={un} name={name}", req.Form["user_id"], req.Form["lis_person_name_full"]);

            return new SetTokenAndRedirectResult(AccessControl.SignToken(req.HttpContext, st), "/c/");
        }

        static bool AuthenticateLti(HttpRequest req)
        {
            if (!req.Form.TryGetValue("oauth_consumer_key", out StringValues keyName)) return false;

            var tag = AzureDns.ReadTag("lti_" + keyName);
            if (string.IsNullOrWhiteSpace(tag)) return false;

            return OAuth1p0.ValidateOAuthSignature(req.Method, UrlFromReq(req), req.Form, tag);
        }

        /*
context_id=Gn0TnPCSJp4Q
context_label=Exam Preview
context_title=Exam Preview
context_type=CourseSection
custom_nonascii=aàa
launch_presentation_document_target=window
launch_presentation_locale=en-US
launch_presentation_return_url=https://learningsuite.byu.edu/LTI/ltiEnd.php
lis_person_contact_email_primary=brandt.redd@byu.edu
lis_person_name_family=Redd
lis_person_name_full=Brandt Redd
lis_person_name_given=Brandt
lti_message_type=basic-lti-launch-request
lti_version=LTI-1p0
oauth_callback=about:blank
oauth_consumer_key=MyKey
oauth_nonce=38ca458800f9bf9c178455e3259b9ac2
oauth_signature_method=HMAC-SHA1
oauth_timestamp=1689101265
oauth_version=1.0
resource_link_id=4EtSy1Xth68a
resource_link_title=LTI With Non-ASCII
roles=Instructor
tool_consumer_instance_guid=learningsuite.byu.edu
tool_consumer_instance_name=BYU Learning Suite
user_id=520206222
oauth_signature=blPgWGei/4JypUGEdZ/iOKMdVSQ=
        */

        static string UrlFromReq(HttpRequest req)
        {
            return string.Concat(req.Scheme, "://", req.Host, req.Path);
        }
    }

    internal class SetTokenAndRedirectResult : ContentResult
    {
        public SetTokenAndRedirectResult(string token, string redirectPath)
        {
            ContentType = MediaTypeNames.Text.Html;
            StatusCode = StatusCodes.Status200OK;
            Content = String.Format(c_content, token, redirectPath);
        }

        const string c_content =
@"<!DOCTYPE html>
<html>
<head></head>
<body><script>
sessionStorage.setItem(""token"", ""{0}"");
window.location.href = ""{1}"";
</script></body>
</html>";
    }

}
