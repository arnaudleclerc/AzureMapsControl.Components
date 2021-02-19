namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(ScaleBarControlJsonConverter))]
    public sealed class ScaleBarControl : Control<ScaleBarControlOptions>
    {
        internal override string Type => "scalebar";
        internal override int Order => 0;
        public ScaleBarControl(ScaleBarControlOptions options = null, ControlPosition position = default) : base(options, position) { }
    }

    internal class ScaleBarControlJsonConverter : JsonConverter<ScaleBarControl>
    {
        public override ScaleBarControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, ScaleBarControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, ScaleBarControl value)
        {
            writer.WriteStartObject();
            writer.WriteString("id", value.Id);
            writer.WriteString("type", value.Type);
            if (value.Position.ToString() != default(ControlPosition).ToString())
            {
                writer.WriteString("position", value.Position.ToString());
            }
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
