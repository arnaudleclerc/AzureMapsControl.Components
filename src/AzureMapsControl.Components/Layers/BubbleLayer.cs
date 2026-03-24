
using System;

namespace AzureMapsControl.Components.Layers;
public sealed class BubbleLayer : Layer<BubbleLayerOptions>
{
    public BubbleLayer() : this(Guid.NewGuid().ToString()) { }
    public BubbleLayer(string id) : base(id, LayerType.BubbleLayer) { }
}
