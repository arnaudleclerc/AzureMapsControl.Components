
using System;

namespace AzureMapsControl.Components.Layers;
/// <summary>
/// Renders raster tiled images on top of the map tiles.
/// </summary>
public sealed class TileLayer : Layer<TileLayerOptions>
{
    public TileLayer() : this(Guid.NewGuid().ToString()) { }
    public TileLayer(string id) : base(id, LayerType.TileLayer) { }
}
