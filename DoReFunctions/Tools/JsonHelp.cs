using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnsForItLearningLabs;

static class JsonHelp {
    public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static readonly JsonWriterOptions WriterOptions = new JsonWriterOptions() {
        Indented = true
    };

    static JsonHelp() {
        SerializerOptions.Converters.Add(JsonDateTimeConverter.Instance);
    }

    public static void WriteProperty(this Utf8JsonWriter writer, string name, string value) {
        writer.WritePropertyName(name);
        writer.WriteStringValue(value);
    }

    public static void WritePropertyObject<T>(this Utf8JsonWriter writer, string name, T obj) {
        writer.WritePropertyName(name);
        JsonSerializer.Serialize<T>(writer, obj, SerializerOptions);
    }
}

class JsonDateTimeConverter : JsonConverter<DateTime> {
    // Singleton
    private JsonDateTimeConverter() { }
    public static readonly JsonDateTimeConverter Instance = new JsonDateTimeConverter();

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return DateTime.Parse(reader.GetString()!, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.RoundtripKind);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) {
        writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK", CultureInfo.InvariantCulture));
    }
}

