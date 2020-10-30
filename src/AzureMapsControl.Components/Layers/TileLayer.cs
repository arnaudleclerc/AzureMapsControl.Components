namespace AzureMapsControl.Components.Layers
{
    using System;

    /// <summary>
    /// Renders raster tiled images on top of the map tiles.
    /// </summary>
    public sealed class TileLayer : Layer<TileLayerOptions, TileLayerEventActivationFlags>
    {
        public TileLayer() : this(Guid.NewGuid().ToString()) { }
        public TileLayer(string id) : base(id, LayerType.TileLayer) => EventActivationFlags = TileLayerEventActivationFlags.None;
    }
}
