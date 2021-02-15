namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(ScalebarControlJsonConverter))]
    public sealed class ScalebarControl : Control<ScaleBarControlOptions>
    {
        internal override string Type => "scalebar";

        public ScalebarControl(ScaleBarControlOptions options = null, ControlPosition position = null): base(options, position) { }
    }

    internal class ScalebarControlJsonConverter : JsonConverter<ScalebarControl>
    {
        public override ScalebarControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, ScalebarControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, ScalebarControl value)
        {
            writer.WriteStartObject();
            writer.WriteString("type", value.Type);
            if (value.Options is not null)
            {
                writer.WritePropertyName("options");
                writer.WriteStartObject();
                if (value.Options.MaxBarLength.HasValue)
                {
                    writer.WriteNumber("maxBarLength", value.Options.MaxBarLength.Value);
                }
                if (value.Options.Units is not null)
                {
                    writer.WriteString("units", value.Options.Units.ToString());
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
