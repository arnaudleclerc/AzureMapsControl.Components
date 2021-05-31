namespace AzureMapsControl.Components.Indoor.Style
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A representation of layer name and its style path.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(StyleDefinitionLayerGroupJsonConverter))]
    public struct StyleDefinitionLayerGroup
    {
        /// <summary>
        /// A representation of layer name and its style path.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Layer style path.
        /// </summary>
        public string LayerPath { get; set; }
    }

    internal class StyleDefinitionLayerGroupJsonConverter : JsonConverter<StyleDefinitionLayerGroup>
    {
        public override StyleDefinitionLayerGroup Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.None)
            {
                reader.Read();
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                string layerPath = null;
                string name = null;
                while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.GetString() == "name")
                    {
                        reader.Read();
                        name = reader.GetString();
                    }

                    else if (reader.GetString() == "layerPath")
                    {
                        reader.Read();
                        layerPath = reader.GetString();
                    }
                }
                return new StyleDefinitionLayerGroup { LayerPath = layerPath, Name = name };

            }
            return default;
        }

        public override void Write(Utf8JsonWriter writer, StyleDefinitionLayerGroup value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}
