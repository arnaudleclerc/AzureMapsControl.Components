namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A control for changing the style of the map.
    /// </summary>
    [JsonConverter(typeof(StyleControlJsonConverter))]
    public sealed class StyleControl : Control<StyleControlOptions>
    {
        internal override string Type => "style";

        public ControlPosition Position { get; set; }

        public StyleControl(StyleControlOptions options = null, ControlPosition position = null) : base(options) => Position = position;
    }

    internal sealed class StyleControlJsonConverter : JsonConverter<StyleControl>
    {
        public override StyleControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, StyleControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, StyleControl value)
        {
            writer.WriteStartObject();
            writer.WriteString("type", value.Type);
            if (value.Position is not null)
            {
                writer.WriteString("position", value.Position.ToString());
            }
            if (value.Options is not null)
            {
                writer.WritePropertyName("options");
                writer.WriteStartObject();
                if (value.Options.MapStyles is not null)
                {
                    writer.WritePropertyName("mapStyles");
                    writer.WriteStartArray();
                    foreach (var mapStyle in value.Options.MapStyles)
                    {
                        writer.WriteStringValue(mapStyle.ToString());
                    }
                    writer.WriteEndArray();
                }
                if (value.Options.Style is not null)
                {
                    writer.WriteString("style", value.Options.Style.ToString());
                }
                if (value.Options.StyleControlLayout is not null)
                {
                    writer.WriteString("styleControlLayout", value.Options.StyleControlLayout.ToString());
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
