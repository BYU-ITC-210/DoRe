using System;
using Azure.ResourceManager.Dns;
using System.Net;
using Azure;
using Microsoft.AspNetCore.Http;
using Azure.ResourceManager.Dns.Models;

namespace DnsForItLearningLabs
{
    internal class AaaaRecord : DnsRecord
    {
        public AaaaRecord()
        {
            Values = new List<string>();
        }

        public AaaaRecord(DnsAaaaRecordResource record)
            : base(record.Data)
        {
            var values = new List<string>();
            foreach (var recordInfo in record.Data.DnsAaaaRecords)
            {
                values.Add(recordInfo.IPv6Address.ToString());
            }
            Values = values;
        }

        public AaaaRecord(DnsRecordData record)
            : base(record)
        {
            var values = new List<string>();
            foreach (var recordInfo in record.DnsAaaaRecords)
            {
                values.Add(recordInfo.IPv6Address.ToString());
            }
            Values = values;
        }

        public IList<string> Values { get; }

        public DnsAaaaRecordData ToDnsRecord()
        {
            var data = new DnsAaaaRecordData();
            FillInDnsRecord(data);
            foreach (var address in Values)
            {
                data.DnsAaaaRecords.Add(new DnsAaaaRecordInfo()
                {
                    IPv6Address = IPAddress.Parse(address)
                });
            }
            return data;
        }
    }

    class AaaaRecordAccessor : IDnsRecordAccessor<AaaaRecord>
    {
        // Singleton pattern
        public static readonly AaaaRecordAccessor Instance = new AaaaRecordAccessor();
        private AaaaRecordAccessor() { }

        public async Task<AaaaRecord?> GetAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return null;
            return new AaaaRecord(resource);
        }

        public Task CreateOrUpdateAsync(DnsZoneResource dnsZone, AaaaRecord record)
        {
            return dnsZone.GetDnsAaaaRecords().CreateOrUpdateAsync(WaitUntil.Completed, record.Name, record.ToDnsRecord());
        }

        public async Task<bool> DeleteAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return false;
            await resource.DeleteAsync(WaitUntil.Completed);
            return true;
        }

        static async Task<DnsAaaaRecordResource?> GetResource(DnsZoneResource dnsZone, string domain)
        {
            try
            {
                return await dnsZone.GetDnsAaaaRecordAsync(domain);
            }
            catch (Azure.RequestFailedException err)
            {
                if (err.Status == StatusCodes.Status404NotFound) return null;
                throw;
            }
        }
    }

}