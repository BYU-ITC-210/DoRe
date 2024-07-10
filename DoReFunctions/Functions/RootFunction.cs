using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace DnsForItLearningLabs.Functions;

/*
 * For a root function to work in .Net 8 / Azure Functions Runtime 4 you must do four things:
 * 1. In host.json set "routePrefix" to "" (empty string).
 * 2. In the settings set "AzureWebJobsDisableHomepage" to "true"
 *    For the local copy do this in "local.settings.json"
 *    For the hosted Azure function do this in Settings > Environment variables
 * 3. Name the function something that will come last alphabetically (e.g. "ZZZRoot").
 *    The function patterns are applied in the alphabetical order of the function names. If
 *    the root function comes earler, it may preempt another function.
 * 4. Set the HttpTrigger Route parameter to a pattern that will match an empty path.
 *    A good option is "{page?}" like the example below. This will match zero or one word
 *    in the path. If you want to match everything that the other functions don't match
 *    then you can use "{*path}".
 *    
 *    For more path options, consult these references:
 *     * https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook-trigger#customize-the-http-endpoint
 *     * https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-8.0#route-templates
 *     * https://learn.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#constraints
 */
public static class RootFunction {
    [Function("ZZZRoot")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "{page?}")] HttpRequest req,
        string page = "") {

        return new OkObjectResult("Root Woot!");
    }
}
