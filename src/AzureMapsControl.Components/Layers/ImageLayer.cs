
using System;

namespace AzureMapsControl.Components.Layers;
public sealed class ImageLayer : Layer<ImageLayerOptions>
{
    public ImageLayer() : this(Guid.NewGuid().ToString()) { }
    public ImageLayer(string id) : base(id, LayerType.ImageLayer) { }
}
