namespace AzureMapsControl.Components.Layers
{
    using System;

    /// <summary>
    /// Renders extruded filled `Polygon` and `MultiPolygon` objects on the map.
    /// </summary>
    public sealed class PolygonExtrusionLayer : Layer<PolygonExtrusionLayerOptions>
    {
        public PolygonExtrusionLayer(): this(Guid.NewGuid().ToString()) { }
        public PolygonExtrusionLayer(string id): base(id, LayerType.PolygonExtrusionLayer) { }
    }
}
