namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A base class which all source layer options inherit from
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(SourceLayerOptionsJsonConverter))]
    public abstract class SourceLayerOptions : LayerOptions
    {
        /// <summary>
        /// ID of the datasource which the layer will render.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Required when the source of the layer is a VectorTileSource.
        /// A vector source can have multiple layers within it, this identifies which one to render in this layer.
        /// Prohibited for all other types of sources.
        /// </summary>
        public string SourceLayer { get; set; }
    }

    internal abstract class SourceLayerOptionsJsonConverter<TOptions> : LayerOptionsJsonConverter<TOptions>
        where TOptions : SourceLayerOptions
    {
        private static readonly string[] s_sourceLayerOptionsProperties = new[] { "source", "sourceLayer" };

        protected static bool IsSourceLayerOptionsProperty(string propertyName) => s_sourceLayerOptionsProperties.Contains(propertyName) || IsLayerOptionsProperty(propertyName);

        protected static void ReadSourceLayerOptionsProperty(string propertyName, Utf8JsonReader reader, TOptions value)
        {
            if (IsLayerOptionsProperty(propertyName))
            {
                ReadLayerOptionsProperty(propertyName, reader, value);
            }
            else
            {
                reader.Read();
                switch (propertyName)
                {
                    case "source":
                        value.Source = reader.TokenType == JsonTokenType.Null ? null : reader.GetString();
                        break;

                    case "sourceLayer":
                        value.SourceLayer = reader.TokenType == JsonTokenType.Null ? null : reader.GetString();
                        break;
                }
            }
        }

        protected static void WriteSourceLayerOptionsProperties(Utf8JsonWriter writer, TOptions value, JsonSerializerOptions options)
        {
            WriteLayerOptionsProperties(writer, value, options);

            if (value.Source is not null)
            {
                writer.WriteString("source", value.Source);
            }

            if (value.SourceLayer is not null)
            {
                writer.WriteString("sourceLayer", value.SourceLayer);
            }
        }
    }

    internal sealed class SourceLayerOptionsJsonConverter : SourceLayerOptionsJsonConverter<SourceLayerOptions>
    {
        public override SourceLayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();
        public override void Write(Utf8JsonWriter writer, SourceLayerOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            if (value is BubbleLayerOptions bubbleLayerOptions)
            {
                JsonSerializer.Serialize(writer, bubbleLayerOptions, options);
            }
            else if (value is HeatmapLayerOptions heatmapLayerOptions)
            {
                JsonSerializer.Serialize(writer, heatmapLayerOptions, options);
            }
            else if (value is PolygonExtrusionLayerOptions polygonExtrusionLayerOptions)
            {
                JsonSerializer.Serialize(writer, polygonExtrusionLayerOptions, options);
            }
            else if (value is SymbolLayerOptions symbolLayerOptions)
            {
                JsonSerializer.Serialize(writer, symbolLayerOptions, options);
            }
        }
    }
}
