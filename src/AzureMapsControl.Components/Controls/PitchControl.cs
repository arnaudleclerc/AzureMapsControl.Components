namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    public sealed class PitchControl : Control<PitchControlOptions>
    {
        internal override string Type => "pitch";
        internal override int Order => 0;

        public PitchControl(PitchControlOptions options = null, ControlPosition position = default) : base(options, position) { }
    }

    internal class PitchControlJsonConverter : JsonConverter<PitchControl>
    {
        public override PitchControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, PitchControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, PitchControl value)
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
                if (value.Options.PitchDegreesDelta.HasValue)
                {
                    writer.WriteNumber("pitchDegreesDelta", value.Options.PitchDegreesDelta.Value);
                }
                if (value.Options.Style.ToString() != default(ControlStyle).ToString())
                {
                    writer.WriteString("style", value.Options.Style.ToString());
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
