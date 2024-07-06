using System;
using Azure.ResourceManager.Dns;
using Azure.ResourceManager.Dns.Models;
using Azure;
using Microsoft.AspNetCore.Http;

namespace DnsForItLearningLabs
{
    internal class MxRecord : DnsRecord
    {
        public MxRecord()
        {
            Values = new List<DnsMXRecordInfo>();
        }

        public MxRecord(DnsMXRecordResource record)
            : base(record.Data)
        {
            Values = new List<DnsMXRecordInfo>(record.Data.DnsMXRecords);
        }

        public MxRecord(DnsRecordData record)
            : base(record)
        {
            Values = new List<DnsMXRecordInfo>(record.DnsMXRecords);
        }

        public IList<DnsMXRecordInfo> Values { get; }

        public DnsMXRecordData ToDnsRecord()
        {
            var data = new DnsMXRecordData();
            FillInDnsRecord(data);
            foreach(var value in Values)
            {
                data.DnsMXRecords.Add(value);
            }
            return data;
        }
    }

    class MxRecordAccessor : IDnsRecordAccessor<MxRecord>
    {
        // Singleton pattern
        public static readonly MxRecordAccessor Instance = new MxRecordAccessor();
        private MxRecordAccessor() { }

        public async Task<MxRecord> GetAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return null;
            return new MxRecord(resource);
        }

        public Task CreateOrUpdateAsync(DnsZoneResource dnsZone, MxRecord record)
        {
            return dnsZone.GetDnsMXRecords().CreateOrUpdateAsync(WaitUntil.Completed, record.Name, record.ToDnsRecord());
        }

        public async Task<bool> DeleteAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return false;
            await resource.DeleteAsync(WaitUntil.Completed);
            return true;
        }

        static async Task<DnsMXRecordResource> GetResource(DnsZoneResource dnsZone, string domain)
        {
            try
            {
                return await dnsZone.GetDnsMXRecordAsync(domain);
            }
            catch (Azure.RequestFailedException err)
            {
                if (err.Status == StatusCodes.Status404NotFound) return null;
                throw;
            }
        }
    }


}