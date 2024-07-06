using System;
using Azure.ResourceManager.Dns;
using Azure;
using Microsoft.AspNetCore.Http;

namespace DnsForItLearningLabs
{
    internal class CnameRecord : DnsRecord
    {
        public CnameRecord() { }

        public CnameRecord(DnsCnameRecordResource record)
            : base(record.Data)
        {
            Cname = record.Data.Cname;
        }

        public CnameRecord(DnsRecordData record)
            : base(record)
        {
            Cname = record.Cname;
        }

        public string Cname { get; set; }

        public DnsCnameRecordData ToDnsRecord()
        {
            var data = new DnsCnameRecordData();
            FillInDnsRecord(data);
            data.Cname = Cname;
            return data;
        }

    }

    class CnameRecordAccessor : IDnsRecordAccessor<CnameRecord>
    {
        // Singleton pattern
        public static readonly CnameRecordAccessor Instance = new CnameRecordAccessor();
        private CnameRecordAccessor() { }

        public async Task<CnameRecord> GetAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return null;
            return new CnameRecord(resource);
        }

        public Task CreateOrUpdateAsync(DnsZoneResource dnsZone, CnameRecord record)
        {
            return dnsZone.GetDnsCnameRecords().CreateOrUpdateAsync(WaitUntil.Completed, record.Name, record.ToDnsRecord());
        }

        public async Task<bool> DeleteAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return false;
            await resource.DeleteAsync(WaitUntil.Completed);
            return true;
        }

        static async Task<DnsCnameRecordResource> GetResource(DnsZoneResource dnsZone, string domain)
        {
            try
            {
                return await dnsZone.GetDnsCnameRecordAsync(domain);
            }
            catch (Azure.RequestFailedException err)
            {
                if (err.Status == StatusCodes.Status404NotFound) return null;
                throw;
            }
        }
    }

}