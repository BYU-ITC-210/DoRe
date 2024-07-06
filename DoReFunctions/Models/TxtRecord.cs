using System;
using Azure;
using Azure.ResourceManager.Dns;
using Azure.ResourceManager.Dns.Models;
using Microsoft.AspNetCore.Http;

namespace DnsForItLearningLabs
{
    internal class TxtRecord : DnsRecord
    {
        public TxtRecord()
        {
            Values = new List<string>();
        }

        public TxtRecord(DnsTxtRecordResource record)
            : base(record.Data)
        {
            var values = new List<string>();
            foreach (var recordInfo in record.Data.DnsTxtRecords)
            {
                // There only ever seems to be one value in each list. Not sure why there are nested lists.
                values.AddRange(recordInfo.Values);
            }
            Values = values;
        }

        public TxtRecord(DnsRecordData record)
            : base(record)
        {
            var values = new List<string>();
            foreach (var recordInfo in record.DnsTxtRecords)
            {
                // There only ever seems to be one value in each list. Not sure why there are nested lists.
                values.AddRange(recordInfo.Values);
            }
            Values = values;
        }

        public IList<string> Values { get; }

        public DnsTxtRecordData ToDnsRecord()
        {
            var data = new DnsTxtRecordData();
            FillInDnsRecord(data);

            var dnsTxtRecords = data.DnsTxtRecords;
            foreach (var value in Values)
            {
                // Again, not sure why this is a list of strings when there only ever seems to be one of them.
                var ri = new DnsTxtRecordInfo();
                ri.Values.Add(value);
                dnsTxtRecords.Add(ri);
            }
            return data;
        }
    }

    class TxtRecordAccessor : IDnsRecordAccessor<TxtRecord>
    {
        // Singleton pattern
        public static readonly TxtRecordAccessor Instance = new TxtRecordAccessor();
        private TxtRecordAccessor() { }

        public async Task<TxtRecord> GetAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return null;
            return new TxtRecord(resource);
        }

        public Task CreateOrUpdateAsync(DnsZoneResource dnsZone, TxtRecord record)
        {
            return dnsZone.GetDnsTxtRecords().CreateOrUpdateAsync(WaitUntil.Completed, record.Name, record.ToDnsRecord());
        }

        public async Task<bool> DeleteAsync(DnsZoneResource dnsZone, string domain)
        {
            var resource = await GetResource(dnsZone, domain);
            if (resource == null) return false;
            await resource.DeleteAsync(WaitUntil.Completed);
            return true;
        }

        static async Task<DnsTxtRecordResource> GetResource(DnsZoneResource dnsZone, string domain)
        {
            try
            {
                return await dnsZone.GetDnsTxtRecordAsync(domain);
            }
            catch (Azure.RequestFailedException err)
            {
                if (err.Status == StatusCodes.Status404NotFound) return null;
                throw;
            }
        }
    }

}
