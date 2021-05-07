namespace AzureMapsControl.Components.Controls
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A control for changing the style of the map.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class StyleControl : Control<StyleControlOptions>
    {
        internal override string Type => "style";
        internal override int Order => 0;

        public StyleControl(StyleControlOptions options = null, ControlPosition position = default) : base(options, position) { }
    }

    internal sealed class StyleControlJsonConverter : JsonConverter<StyleControl>
    {
        public override StyleControl Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, StyleControl value, JsonSerializerOptions options) => Write(writer, value);

        internal static void Write(Utf8JsonWriter writer, StyleControl value)
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
                if (value.Options.Style.ToString() != default(ControlStyle).ToString())
                {
                    writer.WriteString("style", value.Options.Style.ToString());
                }
                if (value.Options.StyleControlLayout.ToString() != default(StyleControlLayout).ToString())
                {
                    writer.WriteString("styleControlLayout", value.Options.StyleControlLayout.ToString());
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
