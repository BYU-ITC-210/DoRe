using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Bredd.CodeBit;
using Azure;
using Azure.ResourceManager.Dns;
using System.Text.Json;
using Bredd.Json;

namespace DnsForItLearningLabs
{
    public static class ZoneFunction
    {
        const string resTypePrefix = "dnszones/";
        const int c_maxUserRecords = 50;
        const int c_maxDomainRecords = 200;
        const int c_defaultTtl = 3600; // One hour in seconds
        const int c_maxSubdomainsPerUser = 6;

        [Function("ZoneFunction")]
        public static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", "options", Route = "api/zone/{domain?}/{recType?}")] HttpRequest req,
            string domain, string recType)
        {
            // Require authentication
            {
                var response = AccessControl.AuthenticateRequest(req);
                if (response is not null) return Task.FromResult(response);
            }

            //log.LogInformation(new EventId(5, "Zone"), "un={un} verb={verb} domain={domain} recType={recType}", AccessControl.GetSessionToken(req.HttpContext)["un"], req.Method, domain, recType);

            if (domain is null)
            {
                // Only GET is supported when no domain given.
                if (!string.Equals(req.Method, "GET", StringComparison.OrdinalIgnoreCase))
                    return Task.FromResult<IActionResult>(new MessageResult(StatusCodes.Status400BadRequest, "Must use 'GET' when no domain specified."));
                return GetAllOwnedDomains(req);
            }

            if (domain[0] == '_') return Task.FromResult<IActionResult>(new MessageResult(StatusCodes.Status400BadRequest, "Domain name must not start with underscore."));
            if (domain[0] == '.' || domain[domain.Length - 1] == '.') return Task.FromResult<IActionResult>(new MessageResult(StatusCodes.Status400BadRequest, "Domain name must not start or end with dot."));

            if (recType is null)
            {
                switch (req.Method.ToUpperInvariant())
                {
                    case "GET":
                        return GetSubDomain(req, domain);
                    case "POST":
                        return CreateOrUpdateSubDomain(true, req, domain);
                    case "PUT":
                        return CreateOrUpdateSubDomain(false, req, domain);
                    case "DELETE":
                        return DeleteDomain(req, domain);
                    default:
                        throw new ApplicationException("Unexpected HTTP verb.");
                }
            }

            switch (recType)
            {
                case "A":
                    return HandleRecordRequest(req, ARecordAccessor.Instance, domain);
                case "AAAA":
                    return HandleRecordRequest(req, AaaaRecordAccessor.Instance, domain);
                case "CNAME":
                    return HandleRecordRequest(req, CnameRecordAccessor.Instance, domain);
                case "MX":
                    return HandleRecordRequest(req, MxRecordAccessor.Instance, domain);
                case "TXT":
                    return HandleRecordRequest(req, TxtRecordAccessor.Instance, domain);
                default:
                    return Task.FromResult<IActionResult>(new MessageResult(StatusCodes.Status400BadRequest, $"Record type '{recType}' not supported."));
            }
        }

        static async Task<IActionResult> GetAllOwnedDomains(HttpRequest req)
        {
            var dnsZone = AzureDns.GetDnsZone();

            // Collect the records
            var user = AccessControl.GetSessionToken(req.HttpContext)?["un"];
            if (user is null) throw new ApplicationException("Failed to retrieve session token or user.");
            var domainRecords = await dnsZone.GetOwnedDomainsAsync(user);
            var aRecords = new List<ARecord>();
            var aaaaRecords = new List<AaaaRecord>();
            var cnameRecords = new List<CnameRecord>();
            var mxRecords = new List<MxRecord>();
            var txtRecords = new List<TxtRecord>();

            foreach (var domain in domainRecords)
            {
                await foreach (var recordData in dnsZone.GetAllRecordDataAsync(c_maxDomainRecords, domain.Name))
                {
                    var resType = recordData.ResourceType.Type;
                    if (resType.StartsWith(resTypePrefix)) resType = resType.Substring(resTypePrefix.Length);
                    switch (resType)
                    {
                        case "A":
                            aRecords.Add(new ARecord(recordData));
                            break;
                        case "AAAA":
                            aaaaRecords.Add(new AaaaRecord(recordData));
                            break;
                        case "CNAME":
                            cnameRecords.Add(new CnameRecord(recordData));
                            break;
                        case "MX":
                            mxRecords.Add(new MxRecord(recordData));
                            break;
                        case "TXT":
                            txtRecords.Add(new TxtRecord(recordData));
                            break;
                            // Ignore other record types
                    }
                }
            }

            // Pepare the result
            var result = new JsonWriterResult();
            using (var writer = result.OpenWriter()) {
                writer.WriteStartObject();

                writer.WriteProperty("user", user);
                writer.WritePropertyObject("domains", domainRecords);
                writer.WritePropertyObject("aRecords", aRecords);
                writer.WritePropertyObject("aaaaRecords", aaaaRecords);
                writer.WritePropertyObject("cnameRecords", cnameRecords);
                writer.WritePropertyObject("mxRecords", mxRecords);
                writer.WritePropertyObject("txtRecords", txtRecords);

                writer.WriteEndObject();
            }

            return result;
        }

        static async Task<IActionResult> GetSubDomain(HttpRequest req, string domain)
        {
            var dnsZone = AzureDns.GetDnsZone();
            var domainRec = await SubDomainRecordAccessor.Instance.GetAsync(dnsZone, domain);
            if (domainRec == null)
                return MessageResult.NotFoundResult;

            var user = AccessControl.GetSessionToken(req.HttpContext)?["un"];
            if (user is null)
                throw new ApplicationException("Failed to retrieve session token or user.");

            if (!string.Equals(domainRec.Owner, user, StringComparison.Ordinal))
                return MessageResult.ForbiddenResult;

            return new JsonResult(domainRec);
        }

        static async Task<IActionResult> CreateOrUpdateSubDomain(bool create, HttpRequest req, string domain)
        {
            try
            {
                if (domain.IndexOf('.') >= 0)
                    return new MessageResult(StatusCodes.Status400BadRequest, "Must be a plain name with no dot.");

                var newRecord = JsonSerializer.Deserialize<SubDomainRecord>(req.Body)!;

                if (!string.IsNullOrWhiteSpace(newRecord.Name) && !string.Equals(domain, newRecord.Name, StringComparison.Ordinal))
                    return new MessageResult(StatusCodes.Status400BadRequest, "Name doesn't match path.");

                var sessionToken = AccessControl.GetSessionToken(req.HttpContext);
                if (sessionToken is null) throw new ApplicationException("Failed to retrieve session token.");
                var user = sessionToken["un"];

                var dnsZone = AzureDns.GetDnsZone();
                var existing = await SubDomainRecordAccessor.Instance.GetAsync(dnsZone, domain);

                if (existing == null && !create) return MessageResult.NotFoundResult;
                if (existing != null && create) return string.Equals(user, existing.Owner, StringComparison.Ordinal)
                        ? MessageResult.ExistsConflictResult
                        : MessageResult.ForbiddenResult;

                if (!string.IsNullOrWhiteSpace(newRecord.Owner) && !string.Equals(user, newRecord.Owner, StringComparison.Ordinal))
                    return new MessageResult(StatusCodes.Status400BadRequest, "Cannot claim a domain for another account.");

                if (create)
                {
                    var ownedCount = (await dnsZone.GetOwnedDomainsAsync(user)).Count;
                    if (ownedCount >= c_maxSubdomainsPerUser)
                        return new MessageResult(StatusCodes.Status400BadRequest, $"You can only claim {c_maxSubdomainsPerUser} subdomains. You already have {ownedCount}.");
                }

                if (newRecord.Ttl <= 0 && existing != null) newRecord.Ttl = existing.Ttl;
                if (newRecord.Ttl <= 0) newRecord.Ttl = c_defaultTtl;

                newRecord.Name = domain;
                newRecord.Owner = user;
                newRecord.OwnerName = sessionToken.GetValueOrDefault("n") ?? string.Empty;
                newRecord.Created = (create) ? DateTime.UtcNow : existing!.Created;
                newRecord.Updated = DateTime.UtcNow;

                await SubDomainRecordAccessor.Instance.CreateOrUpdateAsync(dnsZone, newRecord);

                return new JsonResult(newRecord) {
                    StatusCode = create ? StatusCodes.Status201Created : StatusCodes.Status200OK
                };
            }
            catch (RequestFailedException ex)
            {
                return ex.ToMessageResult();
            }
            catch (JsonException ex)
            {
                return ex.ToMessageResult();
            }
        }

        static async Task<IActionResult> DeleteDomain(HttpRequest req, string domain)
        {
            var user = AccessControl.GetSessionToken(req.HttpContext)?["un"];

            var dnsZone = AzureDns.GetDnsZone();

            var srvRecordResource = await SubDomainRecordAccessor.Instance.GetResource(dnsZone, domain);
            if (srvRecordResource == null) return MessageResult.NotFoundResult;
            
            var owner = srvRecordResource.Data.Metadata.GetString("owner");
            if (!string.Equals(user, owner, StringComparison.Ordinal)) return MessageResult.ForbiddenResult;

            await DeleteDomainRecords(dnsZone, domain);

            await srvRecordResource.DeleteAsync(Azure.WaitUntil.Completed);

            return MessageResult.DeletedResult;
        }

        static async Task DeleteDomainRecords(DnsZoneResource dnsZone, string domain)
        {
            // Collect the names first, then delete. Otherwise the deletions can interfere with enumeration
            // This looks like it could be templated by the Azure class hierarchy isn't amenable to that.

            // A Records
            var names = new List<string>();
            {
                var recset = dnsZone.GetDnsARecords();
                await foreach (var record in recset.GetAllAsync(c_maxUserRecords, domain))
                {
                    names.Add(record.Data.Name);
                }
                foreach (var name in names)
                {
                    // Continue even with errors
                    try
                    {
                        var record = await recset.GetAsync(name);
                        await record.Value.DeleteAsync(WaitUntil.Completed);
                    }
                    catch { }
                }
            }

            // AAAA Records
            names.Clear();
            {
                var recset = dnsZone.GetDnsAaaaRecords();
                await foreach (var record in recset.GetAllAsync(c_maxUserRecords, domain))
                {
                    names.Add(record.Data.Name);
                }
                foreach (var name in names)
                {
                    // Continue even with errors
                    try
                    {
                        var record = await recset.GetAsync(name);
                        await record.Value.DeleteAsync(WaitUntil.Completed);
                    }
                    catch { }
                }
            }

            // CName Records
            names.Clear();
            {
                var recset = dnsZone.GetDnsCnameRecords();
                await foreach (var record in recset.GetAllAsync(c_maxUserRecords, domain))
                {
                    names.Add(record.Data.Name);
                }
                foreach (var name in names)
                {
                    // Continue even with errors
                    try
                    {
                        var record = await recset.GetAsync(name);
                        await record.Value.DeleteAsync(WaitUntil.Completed);
                    }
                    catch { }
                }
            }

            // MX Records
            names.Clear();
            {
                var recset = dnsZone.GetDnsMXRecords();
                await foreach (var record in recset.GetAllAsync(c_maxUserRecords, domain))
                {
                    names.Add(record.Data.Name);
                }
                foreach (var name in names)
                {
                    // Continue even with errors
                    try
                    {
                        var record = await recset.GetAsync(name);
                        await record.Value.DeleteAsync(WaitUntil.Completed);
                    }
                    catch { }
                }
            }

            // TXT Records
            names.Clear();
            {
                var recset = dnsZone.GetDnsTxtRecords();
                await foreach (var record in recset.GetAllAsync(c_maxUserRecords, domain))
                {
                    names.Add(record.Data.Name);
                }
                foreach (var name in names)
                {
                    // Continue even with errors
                    try
                    {
                        var record = await recset.GetAsync(name);
                        await record.Value.DeleteAsync(WaitUntil.Completed);
                    }
                    catch { }
                }
            }
        }

        static Task<IActionResult> HandleRecordRequest<T>(HttpRequest req, IDnsRecordAccessor<T> accessor, string domain) where T : DnsRecord
        {
            switch (req.Method.ToUpperInvariant())
            {
                case "GET":
                    return GetRecord(req, accessor, domain);
                case "POST":
                    return CreateOrUpdateRecord(true, req, accessor, domain);
                case "PUT":
                    return CreateOrUpdateRecord(false, req, accessor, domain);
                case "DELETE":
                    return DeleteRecord(req, accessor, domain);
                default:
                    throw new ApplicationException("Unexpected HTTP verb.");
            }
        }

        static async Task<bool> VerifyAccess(HttpRequest req, DnsZoneResource dnsZone, string domain)
        {
            var dot = domain.LastIndexOf('.');
            var suffix = (dot >= 0) ? domain.Substring(dot + 1) : domain;
            var srvRecordResource = await SubDomainRecordAccessor.Instance.GetResource(dnsZone, suffix);
            if (srvRecordResource == null) return false;

            var owner = srvRecordResource.Data.Metadata.GetString("owner");
            var user = AccessControl.GetSessionToken(req.HttpContext)?["un"];
            return string.Equals(owner, user, StringComparison.Ordinal);
        }

        static async Task<IActionResult> GetRecord<T>(HttpRequest req, IDnsRecordAccessor<T> accessor, string domain)
        {
            // Check Access
            var dnsZone = AzureDns.GetDnsZone();
            if (! await VerifyAccess(req, dnsZone, domain)) return MessageResult.ForbiddenResult;

            var record = await accessor.GetAsync(dnsZone, domain);
            if (record == null) return MessageResult.NotFoundResult;
            return new JsonResult(record);
        }

        static async Task<IActionResult> CreateOrUpdateRecord<T>(bool create, HttpRequest req, IDnsRecordAccessor<T> accessor, string domain) where T : DnsRecord
        {
            try
            {
                var newRecord = JsonSerializer.Deserialize<T>(req.Body)!;

                var dnsZone = AzureDns.GetDnsZone();
                if (!await VerifyAccess(req, dnsZone, domain)) return MessageResult.ForbiddenResult;

                var existing = await accessor.GetAsync(dnsZone, domain);
                if (existing == null && !create) return MessageResult.NotFoundResult;
                if (existing != null && create) return MessageResult.ExistsConflictResult;

                if (newRecord.Ttl <= 0 && existing != null) newRecord.Ttl = existing.Ttl;
                if (newRecord.Ttl <= 0) newRecord.Ttl = c_defaultTtl;

                newRecord.Name = domain;
                newRecord.Created = create ? DateTime.UtcNow : existing!.Created;
                newRecord.Updated = DateTime.UtcNow;

                await accessor.CreateOrUpdateAsync(dnsZone, newRecord);
                return new JsonResult(newRecord) {
                    StatusCode = create ? StatusCodes.Status201Created : StatusCodes.Status200OK
                };
            }
            catch (RequestFailedException ex)
            {
                return ex.ToMessageResult();
            }
            catch (JsonException ex)
            {
                return ex.ToMessageResult();
            }
        }

        static async Task<IActionResult> DeleteRecord<T>(HttpRequest req, IDnsRecordAccessor<T> accessor, string domain)
        {
            var dnsZone = AzureDns.GetDnsZone();
            if (!await VerifyAccess(req, dnsZone, domain)) return MessageResult.ForbiddenResult;

            return await accessor.DeleteAsync(dnsZone, domain)
                ? MessageResult.DeletedResult
                : MessageResult.NotFoundResult;
        }
    }
}
