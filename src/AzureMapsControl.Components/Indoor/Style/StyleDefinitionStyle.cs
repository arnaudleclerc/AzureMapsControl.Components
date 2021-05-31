namespace AzureMapsControl.Components.Indoor.Style
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A representation of a style definition's style object.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(StyleDefinitionStyleJsonConverter))]
    public struct StyleDefinitionStyle
    {
        /// <summary>
        /// Style name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Style theme. `light` or `dark`.
        /// </summary>
        public StyleDefinitionStyleTheme Theme { get; set; }

        /// <summary>
        /// Style's sprite path.
        /// </summary>
        public string SpritePath { get; set; }

        /// <summary>
        /// List of layer groups, defining layer names and their layer style paths.
        /// </summary>
        public IEnumerable<StyleDefinitionLayerGroup> LayerGroups { get; set; }
    }

    internal class StyleDefinitionStyleJsonConverter : JsonConverter<StyleDefinitionStyle>
    {
        public override StyleDefinitionStyle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.None)
            {
                reader.Read();
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                string name = null, spritePath = null;
                StyleDefinitionStyleTheme theme = default;
                IList<StyleDefinitionLayerGroup> layerGroups = null;
                while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.GetString() == "name")
                    {
                        reader.Read();
                        name = reader.GetString();
                    }
                    else if (reader.GetString() == "spritePath")
                    {
                        reader.Read();
                        spritePath = reader.GetString();
                    }
                    else if (reader.GetString() == "theme")
                    {
                        reader.Read();
                        if (reader.GetString() == "dark")
                        {
                            theme = StyleDefinitionStyleTheme.Dark;
                        }
                        else if (reader.GetString() == "light")
                        {
                            theme = StyleDefinitionStyleTheme.Light;
                        }
                    }
                    else if (reader.GetString() == "layerGroups")
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.StartArray)
                        {
                            layerGroups = new List<StyleDefinitionLayerGroup>();
                            while (reader.Read() && reader.TokenType == JsonTokenType.StartObject)
                            {
                                layerGroups.Add(JsonSerializer.Deserialize<StyleDefinitionLayerGroup>(ref reader, options));
                            }
                        }
                    }
                }

                return new StyleDefinitionStyle {
                    LayerGroups = layerGroups?.ToArray(),
                    Name = name,
                    SpritePath = spritePath,
                    Theme = theme
                };
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, StyleDefinitionStyle value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}
