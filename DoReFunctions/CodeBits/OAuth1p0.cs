using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace Bredd.CodeBit
{

    /// <summary>
    /// Functions for decoding and validating OAuth 1.0 payloads - particularly for use with LTI 1.0 and 1.1
    /// </summary>
    static class OAuth1p0
    {
        const string c_oauthSignatureKey = "oauth_signature";
        const string c_oauthTimestampKey = "oauth_timestamp";
        static readonly TimeSpan s_timestampTolerance = TimeSpan.FromMinutes(10);

        public static bool ValidateOAuthSignature(string method, string url, IEnumerable<KeyValuePair<string, StringValues>> values, string secret)
        {
            var calcSig = InternalGetOAuthSignature(method, url, values, secret, out string foundSignature, out string foundTimestamp);
            if (!string.Equals(calcSig, foundSignature, StringComparison.OrdinalIgnoreCase)) return false;
            if (foundTimestamp is null) return false;
            if (!long.TryParse(foundTimestamp, out var timestampLong)) return false;
            var timestamp = DateTime.UnixEpoch.AddSeconds((double)timestampLong);
            if (DateTime.UtcNow - timestamp > s_timestampTolerance) return false;
            
            return true;
        }

        public static string GetOAuthSignature(string method, string url, IEnumerable<KeyValuePair<string, StringValues>> values, string secret)
        {
            return InternalGetOAuthSignature(method, url, values, secret, out string _, out string _);
        }

        public static string InternalGetOAuthSignature(string method, string url, IEnumerable<KeyValuePair<string, StringValues>> values, string secret,
            out string foundSignature, out string foundTimestamp)
        {
            foundSignature = string.Empty;
            foundTimestamp = string.Empty;

            var pairs = new List<KeyValuePair<string, string>>();
            foreach(var pair in values)
            {
                pairs.Add(new KeyValuePair<string, string>(pair.Key, pair.Value!));
            }
            pairs.Sort((a, b) => string.CompareOrdinal(a.Key, b.Key));
            var payload = new StringBuilder();
            foreach (var pair in pairs)
            {
                if (pair.Key == c_oauthSignatureKey)
                {
                    foundSignature = pair.Value;
                    continue; // Don't include in signature validation
                }
                if (pair.Key == c_oauthTimestampKey)
                {
                    foundTimestamp = pair.Value;
                }

                if (payload.Length > 0) payload.Append('&');
                OAuthEncode(pair.Key, payload);
                payload.Append('=');
                OAuthEncode(pair.Value, payload);
            }

            var sigString = new StringBuilder();
            sigString.Append(method.ToUpperInvariant());
            sigString.Append('&');
            OAuthEncode(url, sigString);
            sigString.Append('&');
            OAuthEncode(payload.ToString(), sigString);

            using (var hasher = new HMACSHA1(Encoding.ASCII.GetBytes(string.Concat(Uri.EscapeDataString(secret), "&"))))
            {
                return Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(sigString.ToString())));
            }
        }

        static void OAuthEncode(string value, StringBuilder sb)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            foreach (var b in bytes)
            {
                if ((b >= 'A' && b <= 'Z')
                    || (b >= 'a' && b <= 'z')
                    || (b >= '0' && b <= '9')
                    || (b == '-' || b == '.' || b == '_' || b == '~'))
                {
                    sb.Append((char)b);
                }
                else
                {
                    sb.Append($"%{b:X2}");
                }
            }
        }
    }
}
