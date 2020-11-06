namespace AzureMapsControl.Components.Layers
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A base class which all source layer options inherit from
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class SourceLayerOptions : LayerOptions
    {
        /// <summary>
        /// ID of the datasource which the layer will render.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Required when the source of the layer is a VectorTileSource.
        /// A vector source can have multiple layers within it, this identifies which one to render in this layer.
        /// Prohibited for all other types of sources.
        /// </summary>
        public string SourceLayer { get; set; }
    }
}
