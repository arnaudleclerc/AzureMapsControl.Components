namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// A base class which all other layer options inherit from.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(LayerOptionsJsonConverter))]
    public abstract class LayerOptions
    {
        /// <summary>
        /// An expression specifying conditions on source features.
        /// Only features that match the filter are displayed.
        /// </summary>
        public Expression Filter { get; set; }

        /// <summary>
        /// An integer specifying the maximum zoom level to render the layer at.
        /// This value is inclusive, i.e. the layer will be visible at `maxZoom > zoom >= minZoom`.
        /// </summary>
        public int? MaxZoom { get; set; }

        /// <summary>
        /// An integer specifying the minimum zoom level to render the layer at.
        /// This value is inclusive, i.e. the layer will be visible at `maxZoom > zoom >= minZoom`.
        /// </summary>
        public int? MinZoom { get; set; }

        /// <summary>
        /// Specifies if the layer is visible or not.
        /// </summary>
        public bool? Visible { get; set; }
    }

    internal abstract class LayerOptionsJsonConverter<TOptions> : JsonConverter<TOptions>
        where TOptions : LayerOptions
    {
        private static readonly string[] s_layerOptionsProperties = new[] { "filter", "maxZoom", "minZoom", "visible" };

        protected static bool IsLayerOptionsProperty(string propertyName) => s_layerOptionsProperties.Contains(propertyName);

        protected static void ReadLayerOptionsProperty(string propertyName, Utf8JsonReader reader, TOptions value)
        {
            reader.Read();
            switch (propertyName)
            {
                case "maxZoom":
                    value.MaxZoom = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                    break;
                case "minZoom":
                    value.MinZoom = reader.TokenType == JsonTokenType.Null ? null : reader.GetInt32();
                    break;
                case "visible":
                    value.Visible = reader.TokenType == JsonTokenType.Null ? null : reader.GetBoolean();
                    break;
            }
        }

        protected static void WriteLayerOptionsProperties(Utf8JsonWriter writer, TOptions value, JsonSerializerOptions options)
        {
            if (value.Filter is not null)
            {
                writer.WritePropertyName("filter");
                JsonSerializer.Serialize(writer, value.Filter, options);
            }

            if (value.MaxZoom.HasValue)
            {
                writer.WriteNumber("maxZoom", value.MaxZoom.Value);
            }

            if (value.MinZoom.HasValue)
            {
                writer.WriteNumber("minZoom", value.MaxZoom.Value);
            }

            if (value.Visible.HasValue)
            {
                writer.WriteBoolean("visible", value.Visible.Value);
            }
        }
    }

    internal sealed class LayerOptionsJsonConverter : LayerOptionsJsonConverter<LayerOptions>
    {
        public override LayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();
        public override void Write(Utf8JsonWriter writer, LayerOptions value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            if (value is SourceLayerOptions sourceLayerOptions)
            {
                JsonSerializer.Serialize(writer, sourceLayerOptions, options);
            }
            else if (value is MediaLayerOptions mediaLayerOptions)
            {
                JsonSerializer.Serialize(writer, mediaLayerOptions, options);
            }
        }
    }
}
