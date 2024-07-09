using System;
using System.Text.Json;
using Azure.ResourceManager;
using Azure.ResourceManager.Dns;
using Bredd.CodeBit;
using Microsoft.AspNetCore.Mvc;

namespace DnsForItLearningLabs
{
    internal static class AzureDns
    {
        static DnsZoneResource? c_dnsZone;

        public static DnsZoneResource GetDnsZone()
        {
            if (c_dnsZone is not null) return c_dnsZone;

            var zoneName = Configuration.DnsZone;
            if (string.IsNullOrWhiteSpace(zoneName))
                throw new ApplicationException($"Property dns_zone not found in the configuration.");

            // It may seem inefficient to enumerate all of the subscriptions and zones
            // until we find the right one. But, under normal configuration the application
            // will only have access to one subscription and one zone so this is no less
            // performant than lookign them up directly and it makes configuration
            // a lot more convenient.
            var armClient = new ArmClient(Configuration.AzureCredential);
            foreach(var subscription in armClient.GetSubscriptions())
            {
                foreach (var zone in subscription.GetDnsZones())
                {
                    if (zone.Data.Name == Configuration.DnsZone)
                    {
                        c_dnsZone = zone;
                        return c_dnsZone;
                    }
                }
            }

            throw new ApplicationException($"DnsZone '{Configuration.DnsZone}' does not exist or access has not been granted.");
        }
        
        public static string? ReadTag(string tagName)
        {
            var tags = GetDnsZone().GetTagResource().Get().Value;
            if (tags.Data.TagValues.TryGetValue(tagName, out var value)) return value;
            return null;
        }

        public static async Task<IList<SubDomainRecord>> GetOwnedDomainsAsync(this DnsZoneResource zone, string owner)
        {
            var result = new List<SubDomainRecord>();
            await foreach (var dnsSrvRecord in zone.GetDnsSrvRecords().GetAllAsync(1000))
            {
                if (string.Equals(dnsSrvRecord.Data.Metadata.GetString("owner"), owner))
                {
                    result.Add(new SubDomainRecord(dnsSrvRecord));
                }
            }

            return result;
        }

        public static IActionResult ToMessageResult(this Azure.RequestFailedException ex)
        {
            // Must always succeed in some way
            try
            {
                var content = ex.GetRawResponse()?.Content?.ToString();
                if (content == null) return new MessageResult(ex.Status, ex.Message);
                var json = JsonSerializer.Deserialize<AzureErrorMessage>(content)!;
                return new MessageResult(ex.Status, json.Message);
            }
            catch
            {
                return new MessageResult(ex.Status, ex.Message);
            }
        }

        private class AzureErrorMessage
        {
            public string Message { get; set; }
        }

    }
}
