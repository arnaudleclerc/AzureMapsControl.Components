namespace AzureMapsControl.Components.Layers
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// A base class which all other layer options inherit from.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class LayerOptions
    {
        /// <summary>
        /// An expression specifying conditions on source features.
        /// Only features that match the filter are displayed.
        /// </summary>
        public Expression Filter { get; set; }

        /// <summary>
        /// An integer specifying the maximum zoom level to render the layer at.
        /// This value is inclusive, i.e. the layer will be visible at `maxZoom > zoom >= minZoom`.
        /// </summary>
        public int? MaxZoom { get; set; }

        /// <summary>
        /// An integer specifying the minimum zoom level to render the layer at.
        /// This value is inclusive, i.e. the layer will be visible at `maxZoom > zoom >= minZoom`.
        /// </summary>
        public int? MinZoom { get; set; }

        /// <summary>
        /// Specifies if the layer is visible or not.
        /// </summary>
        public bool? Visible { get; set; }

        internal virtual object GenerateJsOptions() => this;
    }
}
