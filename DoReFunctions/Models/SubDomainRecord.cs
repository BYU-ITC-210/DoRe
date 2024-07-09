using System;
using Azure.ResourceManager.Dns;
using Azure.ResourceManager.Dns.Models;
using Microsoft.AspNetCore.Http;
using Azure;

namespace DnsForItLearningLabs
{
    /// <summary>
    /// The SubDomain record tracks user ownership of a subdomain.
    /// </summary>
    /// <remarks>
    /// We co-opt the DNS SRV record, prefix with an underscore, and store
    /// the ownership info in the metadata.
    /// </remarks>
    internal class SubDomainRecord : DnsRecord
    {
        public SubDomainRecord() { }

        public SubDomainRecord(DnsSrvRecordResource record)
            : base(record.Data)
        {
            // Override the base name handling because of the underscore
            var name = record.Data.Name;
            if (name[0] == '_') name = name.Substring(1);
            Name = name;
            var meta = record.Data.Metadata;
            Owner = meta.GetString("owner");
            Context = meta.GetString("context");
            OwnerName = meta.GetString("ownerName");
        }

        public string? Owner { get; set; }
        public string? Context { get; set; }
        public string? OwnerName { get; set; }

        public DnsSrvRecordData ToDnsRecord()
        {
            var data = new DnsSrvRecordData();
            FillInDnsRecord(data);
            data.DnsSrvRecords.Add(new DnsSrvRecordInfo()
            {
                Priority = 1,
                Weight = 1,
                Port = 1337,
                Target = "example.com"
            });
            data.Metadata.Add("owner", Owner);
            data.Metadata.Add("context", Context ?? string.Empty);
            data.Metadata.Add("ownerName", OwnerName ?? string.Empty);
            return data;
        }
    }

    class SubDomainRecordAccessor : IDnsRecordAccessor<SubDomainRecord>
    {
        // Singleton pattern
        public static readonly SubDomainRecordAccessor Instance = new SubDomainRecordAccessor();
        private SubDomainRecordAccessor() { }

        public async Task<SubDomainRecord?> GetAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return null;
            return new SubDomainRecord(resource);
        }

        public Task CreateOrUpdateAsync(DnsZoneResource dnsZone, SubDomainRecord record)
        {
            return dnsZone.GetDnsSrvRecords().CreateOrUpdateAsync(WaitUntil.Completed, "_" + record.Name, record.ToDnsRecord());
        }

        public async Task<bool> DeleteAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return false;
            await resource.DeleteAsync(WaitUntil.Completed);
            return true;
        }

        public async Task<DnsSrvRecordResource?> GetResource(DnsZoneResource dnsZone, string domain)
        {
            try
            {
                return await dnsZone.GetDnsSrvRecordAsync("_" + domain);
            }
            catch (Azure.RequestFailedException err)
            {
                if (err.Status == StatusCodes.Status404NotFound) return null;
                throw;
            }
        }

    }
}