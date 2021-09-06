namespace AzureMapsControl.Components.Indoor.Style
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A representation of a style definition.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public struct StyleDefinition
    {
        /// <summary>
        /// The base path of the style.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Style version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// A description of a style with configuration paths.
        /// </summary>
        public IEnumerable<StyleDefinitionStyle> Styles { get; set; }
    }

    internal class StyleDefinitionJsonConverter : JsonConverter<StyleDefinition>
    {
        public override StyleDefinition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.None)
            {
                reader.Read();
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                string domain = null, version = null;
                IList<StyleDefinitionStyle> styles = null;
                while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.GetString() == "domain")
                    {
                        reader.Read();
                        domain = reader.GetString();
                    }
                    else if (reader.GetString() == "version")
                    {
                        reader.Read();
                        version = reader.GetString();
                    }
                    else if (reader.GetString() == "styles")
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.StartArray)
                        {
                            styles = new List<StyleDefinitionStyle>();
                            while (reader.Read() && reader.TokenType == JsonTokenType.StartObject)
                            {
                                styles.Add(JsonSerializer.Deserialize<StyleDefinitionStyle>(ref reader, options));
                            }
                        }
                    }
                }

                return new StyleDefinition {
                    Domain = domain,
                    Version = version,
                    Styles = styles?.ToArray()
                };
            }

            return default;
        }
        public override void Write(Utf8JsonWriter writer, StyleDefinition value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}
