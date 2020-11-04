namespace AzureMapsControl.Components.Layers
{
    /// <summary>
    /// Options used when rendering Point objects in a HeatMapLayer.
    /// </summary>
    public sealed class HeatmapLayerOptions
    {


        /// <summary>
        /// Required when the source of the layer is a VectorTileSource.
        /// A vector source can have multiple layers within it, this identifies which one to render in this layer.
        /// Prohibited for all other types of sources.
        /// </summary>
        public string SourceLayer { get; set; }
    }
}
