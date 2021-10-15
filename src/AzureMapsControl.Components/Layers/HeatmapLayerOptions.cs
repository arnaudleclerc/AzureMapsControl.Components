namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used when rendering Point objects in a HeatMapLayer.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(HeatmapLayerOptionsJsonConverter))]
    public sealed class HeatmapLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// Specifies the color gradient used to colorize the pixels in the heatmap. 
        /// This is defined using an expression that uses `["heatmap-density"]` as input. 
        /// </summary>
        public Expression Color { get; set; }

        /// <summary>
        /// Similar to `heatmap-weight` but specifies the global heatmap intensity. 
        /// The higher this value is, the more ‘weight’ each point will contribute to the appearance.
        /// </summary>
        public ExpressionOrNumber Intensity { get; set; }

        /// <summary>
        /// The opacity at which the heatmap layer will be rendered defined as a number between 0 and 1.
        /// </summary>
        public ExpressionOrNumber Opacity { get; set; }

        /// <summary>
        /// The radius in pixels used to render a data point on the heatmap. 
        /// The radius must be a number greater or equal to 1.
        /// </summary>
        public ExpressionOrNumber Radius { get; set; }

        /// <summary>
        /// Specifies how much an individual data point contributes to the heatmap. 
        /// Must be a number greater than 0. A value of 5 would be equivalent to having 5 points of weight 1 in the same spot. 
        /// This is useful when clustering points to allow heatmap rendering or large datasets. 
        /// </summary>
        public ExpressionOrNumber Weight { get; set; }
    }

    internal sealed class HeatmapLayerOptionsJsonConverter : SourceLayerOptionsJsonConverter<HeatmapLayerOptions>
    {
        public override HeatmapLayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var depth = reader.CurrentDepth;
            var result = new HeatmapLayerOptions();
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
                }
            }
            return result;
        }
        public override void Write(Utf8JsonWriter writer, HeatmapLayerOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            WriteSourceLayerOptionsProperties(writer, value, options);

            if (value.Color is not null)
            {
                writer.WritePropertyName("color");
                JsonSerializer.Serialize(writer, value.Color, options);
            }

            if (value.Intensity is not null)
            {
                writer.WritePropertyName("intensity");
                JsonSerializer.Serialize(writer, value.Intensity, options);
            }

            if (value.Opacity is not null)
            {
                writer.WritePropertyName("opacity");
                JsonSerializer.Serialize(writer, value.Opacity, options);
            }

            if (value.Radius is not null)
            {
                writer.WritePropertyName("radius");
                JsonSerializer.Serialize(writer, value.Radius, options);
            }

            if (value.Weight is not null)
            {
                writer.WritePropertyName("weight");
                JsonSerializer.Serialize(writer, value.Weight, options);
            }

            writer.WriteEndObject();

        }
    }
}
