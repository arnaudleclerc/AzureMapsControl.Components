namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
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

    internal class LayerOptionsJsonConverter : JsonConverter<LayerOptions>
    {
        public override LayerOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, LayerOptions value, JsonSerializerOptions options)
        {
            if (value is BubbleLayerOptions bubbleLayerOptions)
            {
                JsonSerializer.Serialize(writer, bubbleLayerOptions, options);
            }
            else if (value is HeatmapLayerOptions heatmapLayerOptions)
            {
                JsonSerializer.Serialize(writer, heatmapLayerOptions, options);
            }
            else if (value is ImageLayerOptions imageLayerOptions)
            {
                JsonSerializer.Serialize(writer, imageLayerOptions, options);
            }
            else if (value is LineLayerOptions lineLayerOptions)
            {
                JsonSerializer.Serialize(writer, lineLayerOptions, options);
            }
            else if (value is PolygonExtrusionLayerOptions polygonExtrusionLayerOptions)
            {
                JsonSerializer.Serialize(writer, polygonExtrusionLayerOptions, options);
            }
            else if (value is PolygonLayerOptions polygonLayerOptions)
            {
                JsonSerializer.Serialize(writer, polygonLayerOptions, options);
            }
            else if (value is SymbolLayerOptions symbolLayerOptions)
            {
                JsonSerializer.Serialize(writer, symbolLayerOptions, options);
            }
            else if (value is TileLayerOptions tileLayerOptions)
            {
                JsonSerializer.Serialize(writer, tileLayerOptions, options);
            }
        }
    }
}
