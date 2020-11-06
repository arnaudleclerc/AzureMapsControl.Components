namespace AzureMapsControl.Components.Layers
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used when rendering `Polygon` and `MultiPolygon` objects in a `PolygonExtrusionLayer`.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class PolygonExtrusionLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// The height in meters to extrude the base of this layer.
        /// This height is relative to the ground.
        /// Must be greater or equal to 0 and less than or equal to `height`.
        /// </summary>
        public ExpressionOrNumber Base { get; set; }

        /// <summary>
        /// The color to fill the polygons with.
        /// Ignored if `fillPattern` is set.
        /// </summary>
        public ExpressionOrString FillColor { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the fill will be drawn.
        /// </summary>
        public double? FillOpacity { get; set; }

        /// <summary>
        /// Name of image in sprite to use for drawing image fills.
        /// For seamless patterns, image width must be a factor of two (2, 4, 8, ..., 512).
        /// </summary>
        public string FillPattern { get; set; }

        /// <summary>
        /// The height in meters to extrude this layer.
        /// This height is relative to the ground.
        /// Must be a number greater or equal to 0.
        /// </summary>
        public ExpressionOrNumber Height { get; set; }

        /// <summary>
        /// The polygons' pixel offset.
        /// Values are [x, y] where negatives indicate left and up, respectively.
        /// </summary>
        public Pixel Translate { get; set; }

        /// <summary>
        /// Specifies the frame of reference for `translate`.
        /// </summary>
        public PitchAlignment TranslateAnchor { get; set; }

        /// <summary>
        /// Specifies if the polygon should have a vertical gradient on the sides of the extrusion.
        /// </summary>
        public bool? VerticalGradient { get; set; }
    }
}
