using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DnsForItLearningLabs.Functions
{
    public static class RootFunction
    {
        [Function("RootFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "/")] HttpRequest req,
            ILogger log)
        {
            Console.WriteLine("Root");
            Console.WriteLine($"{req.Method} {req.Path}");
            string requestBody = new StreamReader(req.Body).ReadToEndAsync().GetAwaiter().GetResult();
            Console.WriteLine(requestBody);
            return new OkObjectResult("Root Woot!");
        }
    }
}
