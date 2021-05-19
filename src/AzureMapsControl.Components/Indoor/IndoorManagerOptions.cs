namespace AzureMapsControl.Components.Indoor
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(IndoorManagerOptionsJsonConverter))]
    public sealed class IndoorManagerOptions
    {
        /// <summary>
        /// A level picker to display as a control for the indoor manager.
        /// </summary>
        public LevelControl LevelControl { get; set; }

        /// <summary>
        /// The state set id to pass with requests.
        /// </summary>
        public string StatesetId { get; set; }

        /// <summary>
        /// The theme for indoor layer styles.
        /// </summary>
        public IndoorLayerTheme Theme { get; set; }

        /// <summary>
        /// The tileset id to pass with requests.
        /// </summary>
        public string TilesetId { get; set; }
    }

    internal class IndoorManagerOptionsJsonConverter : JsonConverter<IndoorManagerOptions>
    {
        public override IndoorManagerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, IndoorManagerOptions value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value.LevelControl is not null)
            {
                writer.WritePropertyName("levelControl");
                JsonSerializer.Serialize(writer, value.LevelControl, options);
            }
            writer.WriteString("statesetId", value.StatesetId);
            if (value.Theme.ToString() != default(IndoorLayerTheme).ToString())
            {
                writer.WriteString("theme", value.Theme.ToString());
            }
            writer.WriteString("tilesetId", value.TilesetId);
            writer.WriteEndObject();
        }
    }
}
