namespace AzureMapsControl.Components.Layers
{
    /// <summary>
    /// Renders raster tiled images on top of the map tiles.
    /// </summary>
    public sealed class TileLayer : Layer
    {
        /// <summary>
        /// Options used when rendering raster tiled images in a TileLayer.
        /// </summary>
        public TileLayerOptions Options { get; internal set; }

        internal TileLayer(string id) : base(id, LayerType.TileLayer) { }
    }
}
