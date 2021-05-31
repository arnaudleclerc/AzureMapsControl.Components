namespace AzureMapsControl.Components.Atlas.FormatOptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(NumberFormatOptionsJsonConverter))]
    public sealed class NumberFormatOptions
    {
        public string Currency { get; set; }
        public string CurrencyDisplay { get; set; }
        public string CurrencySign { get; set; }
        public string LocaleMatcher { get; set; }
        public int? MaximumFractionDigits { get; set; }
        public int? MaximumSignificantDigits { get; set; }
        public int? MinimumIntegerDigits { get; set; }
        public int? MinimumFractionDigits { get; set; }
        public int? MinimumSignificantDigits { get; set; }
        public string Style { get; set; }
        public bool? UseGrouping { get; set; }
    }

    internal class NumberFormatOptionsJsonConverter : JsonConverter<NumberFormatOptions>
    {
        public override NumberFormatOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();
        public override void Write(Utf8JsonWriter writer, NumberFormatOptions value, JsonSerializerOptions options)
        {
            if (value is not null)
            {
                writer.WriteStartObject();

                if (value.Currency is not null)
                {
                    writer.WriteString("currency", value.Currency);
                }

                if (value.CurrencyDisplay is not null)
                {
                    writer.WriteString("currencyDisplay", value.CurrencyDisplay);
                }

                if (value.CurrencySign is not null)
                {
                    writer.WriteString("currencySign", value.CurrencySign);
                }

                if (value.LocaleMatcher is not null)
                {
                    writer.WriteString("localeMatcher", value.LocaleMatcher);
                }

                if (value.MaximumFractionDigits.HasValue)
                {
                    writer.WriteNumber("maximumFractionDigits", value.MaximumFractionDigits.Value);
                }

                if (value.MaximumSignificantDigits.HasValue)
                {
                    writer.WriteNumber("maximumSignificantDigits", value.MaximumSignificantDigits.Value);
                }

                if (value.MinimumFractionDigits.HasValue)
                {
                    writer.WriteNumber("minimumFractionDigits", value.MinimumFractionDigits.Value);
                }

                if (value.MinimumIntegerDigits.HasValue)
                {
                    writer.WriteNumber("minimumIntegerDigits", value.MinimumIntegerDigits.Value);
                }

                if (value.MinimumSignificantDigits.HasValue)
                {
                    writer.WriteNumber("minimumSignificantDigits", value.MinimumSignificantDigits.Value);
                }

                if (value.Style is not null)
                {
                    writer.WriteString("style", value.Style);
                }

                if (value.UseGrouping.HasValue)
                {
                    writer.WriteBoolean("useGrouping", value.UseGrouping.Value);
                }

                writer.WriteEndObject();
            }
        }
    }
}
