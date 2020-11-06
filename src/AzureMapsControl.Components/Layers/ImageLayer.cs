namespace AzureMapsControl.Components.Layers
{
    using System;

    public sealed class ImageLayer : Layer<ImageLayerOptions>
    {
        public ImageLayer() : this(Guid.NewGuid().ToString()) { }
        public ImageLayer(string id) : base(id, LayerType.ImageLayer) { }
    }
}
