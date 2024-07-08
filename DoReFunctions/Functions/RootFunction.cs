using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DnsForItLearningLabs.Functions
{
    public static class RootFunction
    {
        [Function("ZZZRoot")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "{page:?}")] HttpRequest req,
            string page = "")
        {
            Console.WriteLine("Root");
            Console.WriteLine($"{req.Method} {req.Path}");
            string requestBody = new StreamReader(req.Body).ReadToEndAsync().GetAwaiter().GetResult();
            Console.WriteLine(requestBody);
            return new OkObjectResult("Root Woot!");
        }
    }
}
