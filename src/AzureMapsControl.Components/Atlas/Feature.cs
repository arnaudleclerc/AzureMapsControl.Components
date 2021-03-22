namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    public sealed class Feature
    {
        [JsonConverter(typeof(StringConverter))]
        public string Id { get; set; }
        public string Type { get; set; }
        public BoundingBox BBox { get; set; }
    }

    internal sealed class StringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch {
                JsonTokenType.Number => reader.TryGetUInt64(out var value) ? value.ToString() : throw new JsonException($"Invalid number."),
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.None => null,
                JsonTokenType.Null => null,
                _ => throw new JsonException($"Cannot convert from {reader.TokenType}.")
            };
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) => writer.WriteStringValue(value);
    }
}
