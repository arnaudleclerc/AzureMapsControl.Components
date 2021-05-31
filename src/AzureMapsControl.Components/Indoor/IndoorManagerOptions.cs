namespace AzureMapsControl.Components.Indoor
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(IndoorManagerOptionsJsonConverter))]
    public struct IndoorManagerOptions
    {
        /// <summary>
        /// A level picker to display as a control for the indoor manager.
        /// </summary>
        public LevelControl? LevelControl { get; set; }

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
        public override IndoorManagerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string statesetId = null, tilesetId = null;
            IndoorLayerTheme theme = default;

            if (reader.TokenType == JsonTokenType.None)
            {
                reader.Read();
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        if (reader.GetString() == "statesetId")
                        {
                            reader.Read();
                            statesetId = reader.GetString();
                        }
                        else if (reader.GetString() == "theme")
                        {
                            reader.Read();
                            theme = IndoorLayerTheme.FromString(reader.GetString());
                        }
                        else if (reader.GetString() == "tilesetId")
                        {
                            reader.Read();
                            tilesetId = reader.GetString();
                        }
                    }
                }

                return new IndoorManagerOptions {
                    StatesetId = statesetId,
                    Theme = theme,
                    TilesetId = tilesetId
                };
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, IndoorManagerOptions value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value.LevelControl.HasValue)
            {
                writer.WritePropertyName("levelControl");
                JsonSerializer.Serialize(writer, value.LevelControl.Value, options);
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
