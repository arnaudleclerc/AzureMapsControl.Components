namespace AzureMapsControl.Components.Layers
{
    using System;

    /// <summary>
    /// Renders line data on the map. Can be used with SimpleLine, SimplePolygon, CirclePolygon, LineString, MultiLineString, Polygon, and MultiPolygon objects.
    /// </summary>
    public sealed class LineLayer : Layer<LineLayerOptions>
    {
        public LineLayer(): this(Guid.NewGuid().ToString()) { }
        public LineLayer(string id): base(id, LayerType.LineLayer) { }
    }
}
