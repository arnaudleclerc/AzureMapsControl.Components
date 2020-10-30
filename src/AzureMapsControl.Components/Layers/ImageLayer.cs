namespace AzureMapsControl.Components.Layers
{
    using System;

    using AzureMapsControl.Components.Events;

    public sealed class ImageLayer : Layer<ImageLayerOptions>
    {
        public ImageLayer() : this(Guid.NewGuid().ToString()) { }
        public ImageLayer(string id) : base(id, LayerType.ImageLayer) { }
    }
}
