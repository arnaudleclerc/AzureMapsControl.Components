namespace AzureMapsControl.Components.Data
{
    using System;

    /// <summary>
    /// A vector tile source describes how to access a vector tile layer.
    /// Vector tile sources can be used with; SymbolLayer, LineLayer, PolygonLayer, BubbleLayer, HeatmapLayer and VectorTileLayer.
    /// </summary>
    public sealed class VectorTileSource : Source<VectorTileSourceOptions>
    {
        public VectorTileSource() : this(Guid.NewGuid().ToString()) { }
        public VectorTileSource(string id) : base(id, SourceType.VectorTileSource) { }
    }
}
