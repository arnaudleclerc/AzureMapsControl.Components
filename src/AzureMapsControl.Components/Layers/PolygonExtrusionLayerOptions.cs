namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used when rendering `Polygon` and `MultiPolygon` objects in a `PolygonExtrusionLayer`.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(PolygonExtrusionLayerOptionsJsonConverter))]
    public sealed class PolygonExtrusionLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// The height in meters to extrude the base of this layer.
        /// This height is relative to the ground.
        /// Must be greater or equal to 0 and less than or equal to `height`.
        /// </summary>
        public ExpressionOrNumber Base { get; set; }

        /// <summary>
        /// The color to fill the polygons with.
        /// Ignored if `fillPattern` is set.
        /// </summary>
        public ExpressionOrString FillColor { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the fill will be drawn.
        /// </summary>
        public double? FillOpacity { get; set; }

        /// <summary>
        /// Name of image in sprite to use for drawing image fills.
        /// For seamless patterns, image width must be a factor of two (2, 4, 8, ..., 512).
        /// </summary>
        public string FillPattern { get; set; }

        /// <summary>
        /// The height in meters to extrude this layer.
        /// This height is relative to the ground.
        /// Must be a number greater or equal to 0.
        /// </summary>
        public ExpressionOrNumber Height { get; set; }

        /// <summary>
        /// The polygons' pixel offset.
        /// Values are [x, y] where negatives indicate left and up, respectively.
        /// </summary>
        public Pixel Translate { get; set; }

        /// <summary>
        /// Specifies the frame of reference for `translate`.
        /// </summary>
        public PitchAlignment TranslateAnchor { get; set; }

        /// <summary>
        /// Specifies if the polygon should have a vertical gradient on the sides of the extrusion.
        /// </summary>
        public bool? VerticalGradient { get; set; }
    }

    internal sealed class PolygonExtrusionLayerOptionsJsonConverter : SourceLayerOptionsJsonConverter<PolygonExtrusionLayerOptions>
    {
        public override PolygonExtrusionLayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var depth = reader.CurrentDepth;
            var result = new PolygonExtrusionLayerOptions();
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
                            case "fillOpacity":
                                result.FillOpacity = reader.TokenType == JsonTokenType.Null ? null : reader.GetDouble();
                                break;

                            case "fillPattern":
                                result.FillPattern = reader.TokenType == JsonTokenType.Null ? null : reader.GetString();
                                break;

                            case "translate":
                                result.Translate = reader.TokenType == JsonTokenType.Null ? null : JsonSerializer.Deserialize<Pixel>(ref reader, options);
                                break;

                            case "translateAnchor":
                                result.TranslateAnchor = reader.TokenType == JsonTokenType.Null ? default : PitchAlignment.FromString(reader.GetString());
                                break;

                            case "verticalGradient":
                                result.VerticalGradient = reader.TokenType == JsonTokenType.Null ? null : reader.GetBoolean();
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, PolygonExtrusionLayerOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            WriteSourceLayerOptionsProperties(writer, value, options);

            if (value.Base is not null)
            {
                writer.WritePropertyName("base");
                JsonSerializer.Serialize(writer, value.Base, options);
            }

            if (value.FillColor is not null)
            {
                writer.WritePropertyName("fillColor");
                JsonSerializer.Serialize(writer, value.FillColor, options);
            }

            if (value.FillOpacity.HasValue)
            {
                writer.WriteNumber("fillOpacity", value.FillOpacity.Value);
            }

            if (value.FillPattern is not null)
            {
                writer.WriteString("fillPattern", value.FillPattern);
            }

            if (value.Height is not null)
            {
                writer.WritePropertyName("height");
                JsonSerializer.Serialize(writer, value.Height, options);
            }

            if (value.Translate is not null)
            {
                writer.WritePropertyName("translate");
                JsonSerializer.Serialize(writer, value.Translate, options);
            }

            if (value.TranslateAnchor.ToString() != default(PitchAlignment).ToString())
            {
                writer.WriteString("translateAnchor", value.TranslateAnchor.ToString());
            }

            if (value.VerticalGradient.HasValue)
            {
                writer.WriteBoolean("verticalGradient", value.VerticalGradient.Value);
            }

            writer.WriteEndObject();
        }
    }
}
