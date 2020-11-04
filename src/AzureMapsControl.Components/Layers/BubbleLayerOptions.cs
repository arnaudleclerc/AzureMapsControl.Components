namespace AzureMapsControl.Components.Layers
{
    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options to display a bubble layer
    /// </summary>
    public sealed class BubbleLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// The amount to blur the circles.
        /// A value of 1 blurs the circles such that only the center point if at full opacity.
        /// </summary>
        public double? Blur { get; set; }

        /// <summary>
        /// The color to fill the circle symbol with.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the circles will be drawn.
        /// </summary>
        public double? Opacity { get; set; }

        /// <summary>
        /// The color of the circles' outlines.
        /// </summary>
        public string StrokeColor { get; set; }

        /// <summary>
        /// A number between 0 and 1 that indicates the opacity at which the circles' outlines will be drawn.
        /// </summary>
        public double? StrokeOpacity { get; set; }

        /// <summary>
        /// The width of the circles' outlines in pixels.
        /// </summary>
        public double? StrokeWidth { get; set; }

        internal string PitchAlignment { get => PitchAlignmentType.ToString(); set => PitchAlignmentType = Atlas.PitchAlignment.FromString(value); }

        /// <summary>
        /// Specifies the orientation of circle when map is pitched.
        /// </summary>
        public PitchAlignment PitchAlignmentType { get; set; }

        /// <summary>
        /// The radius of the circle symbols in pixels.
        /// </summary>
        public double? Radius { get; set; }
    }
}
