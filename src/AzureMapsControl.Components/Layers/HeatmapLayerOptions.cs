namespace AzureMapsControl.Components.Layers
{
    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used when rendering Point objects in a HeatMapLayer.
    /// </summary>
    public sealed class HeatmapLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// Specifies the color gradient used to colorize the pixels in the heatmap. 
        /// This is defined using an expression that uses `["heatmap-density"]` as input. 
        /// </summary>
        public Expression Color { get; set; }

        /// <summary>
        /// Similar to `heatmap-weight` but specifies the global heatmap intensity. 
        /// The higher this value is, the more ‘weight’ each point will contribute to the appearance.
        /// </summary>
        public ExpressionOrNumber Intensity { get; set; }

        /// <summary>
        /// The opacity at which the heatmap layer will be rendered defined as a number between 0 and 1.
        /// </summary>
        public ExpressionOrNumber Opacity { get; set; }

        /// <summary>
        /// The radius in pixels used to render a data point on the heatmap. 
        /// The radius must be a number greater or equal to 1.
        /// </summary>
        public ExpressionOrNumber Radius { get; set; }

        /// <summary>
        /// Specifies how much an individual data point contributes to the heatmap. 
        /// Must be a number greater than 0. A value of 5 would be equivalent to having 5 points of weight 1 in the same spot. 
        /// This is useful when clustering points to allow heatmap rendering or large datasets. 
        /// </summary>
        public ExpressionOrNumber Weight { get; set; }
    }
}
