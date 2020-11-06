namespace AzureMapsControl.Components.Layers
{
    using System;

    /// <summary>
    /// Renders filled Polygon and MultiPolygon objects on the map.
    /// </summary>
    public sealed class PolygonLayer : Layer<PolygonLayerOptions>
    {
        public PolygonLayer() : this(Guid.NewGuid().ToString()) { }
        public PolygonLayer(string id) : base(id, LayerType.PolygonLayer) { }
    }
}
