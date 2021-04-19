namespace AzureMapsControl.Components.Layers
{
    using System;

    /// <summary>
    /// A layer that renders point data from a data source as HTML elements on the map.
    /// </summary>
    public sealed class HtmlMarkerLayer : Layer<HtmlMarkerLayerOptions>
    {
        internal HtmlMarkerLayerCallbackInvokeHelper MarkerCallbackInvokeHelper => new HtmlMarkerLayerCallbackInvokeHelper(Options?.MarkerCallback);
        public HtmlMarkerLayer() : this(Guid.NewGuid().ToString()) { }
        public HtmlMarkerLayer(string id) : base(id, LayerType.HtmlMarkerLayer) { }
    }
}
