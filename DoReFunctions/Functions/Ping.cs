using System.Text;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Dns;
using Azure.ResourceManager.Dns.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DnsForItLearningLabs;


// Do nothing except in debug mode because this could violate security
#if DEBUG

public class Ping {
    ILogger<Ping> m_logger;

    public Ping(ILogger<Ping> logger) {
        m_logger = logger;
        Console.WriteLine("Ping constructor.");
        logger.LogInformation("Ping constructor information");
        logger.LogWarning("Ping constructor warning");
        logger.LogError("Ping constructor error");
        logger.LogCritical("Ping constructor critical");
    }

    [Function("Ping")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "api/ping")] HttpRequest req) {
        Console.WriteLine(m_logger.GetType().FullName);
        m_logger.LogInformation("Ping");

        var sb = new StringBuilder();

        try {
            sb.AppendLine($"DnsZone: {Configuration.DnsZone}");
            //sb.AppendLine($"KeyFile: {AccessControl.KeyFilePath}");
            sb.AppendLine();

            sb.AppendLine("Environment");
            foreach (System.Collections.DictionaryEntry de in Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process)) {
                string key = de.Key as string ?? string.Empty;
                string value = de.Value as string ?? string.Empty;
                sb.AppendLine($"{key}: {value}");
            }
            sb.AppendLine();

            sb.AppendLine("Current Directory");
            sb.AppendLine(Environment.CurrentDirectory);
            sb.AppendLine();

            sb.AppendLine("Environment:");
            sb.AppendLine($"static_root={Environment.GetEnvironmentVariable("static_root")}");
            sb.AppendLine($"AzureWebJobsScriptRoot={Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot")}");
            sb.AppendLine($"HOME={Environment.GetEnvironmentVariable("HOME")}");
            sb.AppendLine();

            var context = req.HttpContext;
            foreach (var pair in context.Features) {
                sb.AppendLine($"Feature: {pair.Key.FullName}");
            }
            sb.AppendLine();

            ILoggerFactory lf = context.RequestServices.GetService<ILoggerFactory>() ?? throw new ApplicationException("Failed to retrieve ILoggerFactory");
            var logger = lf.CreateLogger("Function.Longo.User");
            Console.WriteLine("Hello from Ping!");
            if (logger is not null)
                logger.LogInformation("To LoggerFactory.CreateLogger Hello from Ping!");
            m_logger.LogInformation("Direct log from Ping!");

            sb.AppendLine();
            sb.AppendLine("------------------");

            // Only allow ManagedIdentityCredential and EnvironmentCredential
            var credOptions = new DefaultAzureCredentialOptions() {
                ExcludeAzureCliCredential = true,
                ExcludeAzureDeveloperCliCredential = true,
                ExcludeAzurePowerShellCredential = true,
                ExcludeEnvironmentCredential = false,
                ExcludeInteractiveBrowserCredential = true,
                ExcludeManagedIdentityCredential = false,
                ExcludeSharedTokenCacheCredential = true,
                ExcludeVisualStudioCodeCredential = true,
                ExcludeVisualStudioCredential = true,
                ExcludeWorkloadIdentityCredential = true
            };

            var credential = new DefaultAzureCredential(credOptions);
            //var credential = new EnvironmentCredential();
            //var credential = new ClientSecretCredential(Configuration.TenantId, Configuration.ClientId, Configuration.ClientSecret);
            var armClient = new ArmClient(credential);

            var subscriptions = armClient.GetSubscriptions();
            foreach (var subscription in subscriptions) {
                sb.AppendLine($"Subscription: {subscription.Data.DisplayName}");

                var dnsZones = subscription.GetDnsZones();
                foreach (var zone in dnsZones) {
                    sb.AppendLine($"  ZoneId: {zone.Id}");
                    sb.AppendLine($"  Zone: {zone.Data.Name}");
                    foreach (var record in zone.GetAllRecordData()) {
                        sb.AppendLine($"    {record.Name} type={record.ResourceType}");
                        foreach (var ns in record.DnsNSRecords) {
                            sb.AppendLine($"      NS {ns.DnsNSDomainName}");
                        }
                        foreach (DnsTxtRecordInfo txt in record.DnsTxtRecords) {
                            sb.AppendLine($"      TXT {string.Join("; ", txt.Values)}");
                        }
                    }
                }
            }

            var dnsZone = AzureDns.GetDnsZone();
            sb.AppendLine($"Zone: {dnsZone.Data.Name}");
            //dnsZone = dnsZone.Get();
            var tagResource = dnsZone.GetTagResource().Get().Value;
            foreach (var tag in tagResource.Data.TagValues) {
                sb.AppendLine($"  TAG {tag.Key}={tag.Value}");
            }
            foreach (var recordSet in dnsZone.GetAllRecordData()) {
                sb.AppendLine($"  RecordSet: {recordSet.Name} {recordSet.ResourceType}");
                foreach (var datum in recordSet.Metadata) {
                    sb.AppendLine($"    m {datum.Key}={datum.Value}");
                }
                foreach (var ns in recordSet.DnsNSRecords) {
                    sb.AppendLine($"    NS {ns.DnsNSDomainName}");
                }
                foreach (var txt in recordSet.DnsTxtRecords) {
                    sb.AppendLine($"    TXT {string.Join("; ", txt.Values)}");
                }
                foreach (var a in recordSet.DnsARecords) {
                    sb.AppendLine($"    A {a.IPv4Address}");
                }

            }
        }
        catch (Exception err) {
            sb.AppendLine();
            sb.AppendLine("=================");
            sb.AppendLine(err.ToString());
        }

        return new OkObjectResult(sb.ToString());
    }
}

#endif
