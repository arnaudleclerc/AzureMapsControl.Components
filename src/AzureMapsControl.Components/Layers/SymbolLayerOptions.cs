namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;


    /// <summary>
    /// Options used when rendering geometries in a SymbolLayer.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(SymbolLayerOptionsJsonConverter))]
    public sealed class SymbolLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// Options used to customize the icons of the symbols.
        /// </summary>
        public IconOptions IconOptions { get; set; }

        /// <summary>
        /// Distance in pixels between two symbol anchors along a line. Must be greater or equal to 1.
        /// </summary>
        public ExpressionOrNumber LineSpacing { get; set; }

        /// <summary>
        /// Options used to customize the text of the symbols.
        /// </summary>
        public TextOptions TextOptions { get; set; }

        /// <summary>
        /// Specifies the label placement relative to its geometry.
        /// </summary>
        public SymbolLayerPlacement Placement { get; set; }
    }

    internal sealed class SymbolLayerOptionsJsonConverter : SourceLayerOptionsJsonConverter<SymbolLayerOptions>
    {
        public override SymbolLayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var depth = reader.CurrentDepth;
            var result = new SymbolLayerOptions();
            while (reader.TokenType != JsonTokenType.EndObject || depth != reader.CurrentDepth)
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    if (IsSourceLayerOptionsProperty(propertyName))
                    {
                        ReadSourceLayerOptionsProperty(propertyName, reader, result);
                    }
                    else
                    {
                        reader.Read();
                        switch (propertyName)
                        {
                            case "iconOptions":
                                result.IconOptions = reader.TokenType == JsonTokenType.Null ? null : JsonSerializer.Deserialize<IconOptions>(ref reader, options);
                                break;

                            case "textOptions":
                                result.TextOptions = reader.TokenType == JsonTokenType.Null ? null : JsonSerializer.Deserialize<TextOptions>(ref reader, options);
                                break;

                            case "placement":
                                result.Placement = reader.TokenType == JsonTokenType.Null ? default : SymbolLayerPlacement.FromString(reader.GetString());
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, SymbolLayerOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            WriteSourceLayerOptionsProperties(writer, value, options);

            if (value.IconOptions is not null)
            {
                writer.WritePropertyName("iconOptions");
                JsonSerializer.Serialize(writer, value.IconOptions);
            }

            if (value.LineSpacing is not null)
            {
                writer.WritePropertyName("lineSpacing");
                JsonSerializer.Serialize(writer, value.LineSpacing);
            }

            if (value.Placement.ToString() != default(SymbolLayerPlacement).ToString())
            {
                writer.WriteString("placement", value.Placement.ToString());
            }

            if (value.TextOptions is not null)
            {
                writer.WritePropertyName("textOptions");
                JsonSerializer.Serialize(writer, value.TextOptions);
            }

            writer.WriteEndObject();
        }
    }
}
