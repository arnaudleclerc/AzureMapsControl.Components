namespace AzureMapsControl.Components.Layers
{
    using System;

    /// <summary>
    /// Represent the density of data using different colors (HeatMap).
    /// </summary>
    public class HeatmapLayer : Layer<HeatmapLayerOptions>
    {
        public HeatmapLayer() : this(Guid.NewGuid().ToString()) { }
        public HeatmapLayer(string id) : base(id, LayerType.HeatmapLayer)
        {
        }
    }
}
