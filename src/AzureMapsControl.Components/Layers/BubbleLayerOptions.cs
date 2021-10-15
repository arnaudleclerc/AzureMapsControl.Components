namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options to display a bubble layer
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(BubbleLayerOptionsJsonConverter))]
    public sealed class BubbleLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// The amount to blur the circles.
        /// A value of 1 blurs the circles such that only the center point if at full opacity.
        /// </summary>
        public ExpressionOrNumber Blur { get; set; }

        /// <summary>
        /// The color to fill the circle symbol with.
        /// </summary>
        public ExpressionOrString Color { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the circles will be drawn.
        /// </summary>
        public ExpressionOrNumber Opacity { get; set; }

        /// <summary>
        /// The color of the circles' outlines.
        /// </summary>
        public ExpressionOrString StrokeColor { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the circles' outlines will be drawn.
        /// </summary>
        public ExpressionOrNumber StrokeOpacity { get; set; }

        /// <summary>
        /// The width of the circles' outlines in pixels.
        /// </summary>
        public ExpressionOrNumber StrokeWidth { get; set; }

        /// <summary>
        /// Specifies the orientation of circle when map is pitched.
        /// </summary>
        public PitchAlignment PitchAlignment { get; set; }

        /// <summary>
        /// The radius of the circle symbols in pixels.
        /// </summary>
        public ExpressionOrNumber Radius { get; set; }
    }

    internal class BubbleLayerOptionsJsonConverter : SourceLayerOptionsJsonConverter<BubbleLayerOptions>
    {
        public override BubbleLayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var depth = reader.CurrentDepth;
            var result = new BubbleLayerOptions();
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
                            case "pitchAlignment":
                                result.PitchAlignment = reader.TokenType == JsonTokenType.Null ? default : PitchAlignment.FromString(reader.GetString());
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, BubbleLayerOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            WriteSourceLayerOptionsProperties(writer, value, options);

            if (value.Blur is not null)
            {
                writer.WritePropertyName("blur");
                JsonSerializer.Serialize(writer, value.Blur, options);
            }

            if (value.Color is not null)
            {
                writer.WritePropertyName("color");
                JsonSerializer.Serialize(writer, value.Color, options);
            }

            if (value.Opacity is not null)
            {
                writer.WritePropertyName("opacity");
                JsonSerializer.Serialize(writer, value.Opacity, options);
            }

            if (value.PitchAlignment.ToString() != default(PitchAlignment).ToString())
            {
                writer.WritePropertyName("pitchAlignment");
                JsonSerializer.Serialize(writer, value.PitchAlignment, options);
            }

            if (value.Radius is not null)
            {
                writer.WritePropertyName("radius");
                JsonSerializer.Serialize(writer, value.Radius, options);
            }

            if (value.StrokeColor is not null)
            {
                writer.WritePropertyName("strokeColor");
                JsonSerializer.Serialize(writer, value.StrokeColor, options);
            }

            if (value.StrokeOpacity is not null)
            {
                writer.WritePropertyName("strokeOpacity");
                JsonSerializer.Serialize(writer, value.StrokeOpacity, options);
            }

            if (value.StrokeWidth is not null)
            {
                writer.WritePropertyName("strokeWidth");
                JsonSerializer.Serialize(writer, value.StrokeWidth, options);
            }

            writer.WriteEndObject();
        }

    }
}
