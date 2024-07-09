using System;
using Azure.ResourceManager.Dns;

namespace DnsForItLearningLabs
{
    internal abstract class DnsRecord
    {
        protected DnsRecord() { }

        protected DnsRecord(DnsBaseRecordData data)
        {
            Name = data.Name;
            Ttl = data.TtlInSeconds ?? 3601;
            var meta = data.Metadata;
            Created = meta.GetDateTime("created");
            Updated = meta.GetDateTime("updated");
        }

        public string? Name { get; set; }
        public long Ttl { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        protected void FillInDnsRecord(DnsBaseRecordData data)
        {
            data.TtlInSeconds = Ttl;
            data.Metadata.Add("created", Created.ToJson());
            data.Metadata.Add("updated", Updated.ToJson());
        }
    }

    internal interface IDnsRecordAccessor<T>
    {
        Task<T?> GetAsync(DnsZoneResource dnsZone, string domain);
        Task CreateOrUpdateAsync(DnsZoneResource dnsZone, T record);
        Task<bool> DeleteAsync(DnsZoneResource dnsZone, string domain);
    }
}
