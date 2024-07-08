using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Bredd.CodeBit;
using System.Text;

namespace DnsForItLearningLabs
{
    public static class DumpFunction
    {
        const string resTypePrefix = "dnszones/";

        [Function("Dump")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "options", Route = "api/dump")] HttpRequest req)
        {
            {
                var response = AccessControl.AuthenticateRequest(req);
                if (response is not null) return response;
            }
            var token = AccessControl.GetSessionToken(req.HttpContext);
            if (token is null || !string.Equals(token["r"], "admin"))
                return MessageResult.ForbiddenResult;

            var sb = new StringBuilder();

            var dnsZone = AzureDns.GetDnsZone();
            sb.AppendLine($"Zone: {dnsZone.Data.Name}");
            foreach (var recordSet in dnsZone.GetAllRecordData())
            {
                var resType = recordSet.ResourceType.Type;
                if (resType.StartsWith(resTypePrefix)) resType = resType.Substring(resTypePrefix.Length);
                sb.AppendLine($"  RecordSet: {recordSet.Name} {resType} ttl={recordSet.TtlInSeconds}");
                foreach (var datum in recordSet.Metadata)
                {
                    sb.AppendLine($"    m {datum.Key}={datum.Value}");
                }
                switch (resType)
                {
                    case "NS":
                        foreach (var ns in recordSet.DnsNSRecords)
                        {
                            sb.AppendLine($"    NS {ns.DnsNSDomainName}");
                        }
                        break;

                    case "TXT":
                        foreach (var txt in recordSet.DnsTxtRecords)
                        {
                            sb.AppendLine($"    TXT {string.Join("; ", txt.Values)}");
                        }
                        break;

                    case "A":
                        foreach (var a in recordSet.DnsARecords)
                        {
                            sb.AppendLine($"    A {a.IPv4Address}");
                        }
                        break;

                    case "AAAA":
                        foreach (var aaaa in recordSet.DnsAaaaRecords)
                        {
                            sb.AppendLine($"    AAAA {aaaa.IPv6Address}");
                        }
                        break;

                    case "MX":
                        foreach (var mx in recordSet.DnsMXRecords)
                        {
                            sb.AppendLine($"    MX {mx.Exchange} pref={mx.Preference}");
                        }
                        break;

                    case "CNAME":
                        sb.AppendLine($"    CNAME {recordSet.Cname}");
                        break;
                }
            }

            return new OkObjectResult(sb.ToString());
        }
    }
}
