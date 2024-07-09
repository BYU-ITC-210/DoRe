using System.Globalization;
using System.Text.Json;
using Bredd.CodeBit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DnsForItLearningLabs;

internal static class Helpers {
    public static string GetString(this IDictionary<string, string> dictionary, string key) {
        if (dictionary.TryGetValue(key, out string? value))
            return value;
        return string.Empty;
    }

    public static DateTime GetDateTime(this IDictionary<string, string> dictionary, string key) {
        if (dictionary.TryGetValue(key, out string? value)) {
            if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind | DateTimeStyles.AllowWhiteSpaces,
                out DateTimeOffset dt)) {
                return dt.UtcDateTime;
            }
        }
        return DateTime.MinValue;
    }

    public static string ToJson(this DateTime value) {
        return value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK", CultureInfo.InvariantCulture);
    }

    public static IActionResult ToMessageResult(this JsonException ex) {
        return new MessageResult(StatusCodes.Status400BadRequest, $"Invalid JSON: {ex.Message}");
    }
}
