using System;
using Azure.ResourceManager.Dns;
using System.Net;
using Azure.ResourceManager.Dns.Models;
using Azure;
using Microsoft.AspNetCore.Http;

namespace DnsForItLearningLabs
{
    internal class ARecord : DnsRecord
    {
        public ARecord()
        {
            Values = new List<string>();
        }

        public ARecord(DnsARecordResource record)
            : base(record.Data)
        {
            var values = new List<string>();
            foreach (var recordInfo in record.Data.DnsARecords)
            {
                values.Add(recordInfo.IPv4Address.ToString());
            }
            Values = values;
        }

        public ARecord(DnsRecordData record)
            : base(record)
        {
            var values = new List<string>();
            foreach (var recordInfo in record.DnsARecords)
            {
                values.Add(recordInfo.IPv4Address.ToString());
            }
            Values = values;
        }

        public IList<string> Values { get; }

        public DnsARecordData ToDnsRecord()
        {
            var data = new DnsARecordData();
            FillInDnsRecord(data);
            foreach(var address in Values)
            {
                data.DnsARecords.Add(new DnsARecordInfo()
                {
                    IPv4Address = IPAddress.Parse(address)
                });
            }
            return data;
        }
    }

    class ARecordAccessor : IDnsRecordAccessor<ARecord>
    {
        // Singleton pattern
        public static readonly ARecordAccessor Instance = new ARecordAccessor();
        private ARecordAccessor() { }

        public async Task<ARecord> GetAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return null;
            return new ARecord(resource);
        }

        public Task CreateOrUpdateAsync(DnsZoneResource dnsZone, ARecord record)
        {
            return dnsZone.GetDnsARecords().CreateOrUpdateAsync(WaitUntil.Completed, record.Name, record.ToDnsRecord());
        }

        public async Task<bool> DeleteAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return false;
            await resource.DeleteAsync(WaitUntil.Completed);
            return true;
        }

        static async Task<DnsARecordResource> GetResource(DnsZoneResource dnsZone, string domain)
        {
            try
            {
                return await dnsZone.GetDnsARecordAsync(domain);
            }
            catch (Azure.RequestFailedException err)
            {
                if (err.Status == StatusCodes.Status404NotFound) return null;
                throw;
            }
        }
    }

}