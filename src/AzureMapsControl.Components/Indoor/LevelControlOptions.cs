namespace AzureMapsControl.Components.Indoor
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Controls;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(LevelControlOptionsJsonConverter))]
    public sealed class LevelControlOptions
    {
        /// <summary>
        /// The position of the control.
        /// </summary>
        public ControlPosition Position { get; set; }

        /// <summary>
        /// The style of the control
        /// </summary>
        public ControlStyle Style { get; set; }
    }

    internal class LevelControlOptionsJsonConverter : JsonConverter<LevelControlOptions>
    {
        public override LevelControlOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();
        public override void Write(Utf8JsonWriter writer, LevelControlOptions value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value.Position.ToString() != default(ControlPosition).ToString())
            {
                writer.WriteString("position", value.Position.ToString());
            }
            if (value.Style.ToString() != default(ControlStyle).ToString())
            {
                writer.WriteString("style", value.Style.ToString());
            }
            writer.WriteEndObject();
        }
    }
}
