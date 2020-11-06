namespace AzureMapsControl.Components.Layers
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used when rendering Polygon and MultiPolygon objects in a PolygonLayer.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class PolygonLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// The color to fill the polygons with.
        /// </summary>
        public ExpressionOrString FillColor { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the fill will be drawn.
        /// </summary>
        public ExpressionOrNumber FillOpacity { get; set; }

        /// <summary>
        /// Name of image in sprite to use for drawing image fills. For seamless patterns, image width must be a factor of two (2, 4, 8, ..., 512).
        /// </summary>
        public ExpressionOrString FillPattern { get; set; }
    }
}
