namespace AzureMapsControl.Components.Layers
{
    using System;

    public sealed class BubbleLayer : Layer<BubbleLayerOptions>
    {
        public BubbleLayer() : this(Guid.NewGuid().ToString()) { }
        public BubbleLayer(string id) : base(id, LayerType.BubbleLayer) { }
    }
}
