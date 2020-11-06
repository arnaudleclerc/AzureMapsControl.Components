namespace AzureMapsControl.Components.Data
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class DataSourceOptions
    {
        /// <summary>
        /// The size of the buffer around each tile.
        /// A buffer value of 0 will provide better performance but will be more likely to generate artifacts when rendering.
        /// Larger buffers will produce left artifacts but will result in slower performance.
        /// </summary>
        public int? Buffer { get; set; }

        /// <summary>
        /// A boolean indicating if Point features in the source should be clustered or not.
        /// If set to true, points will be clustered together into groups by radius.
        /// </summary>
        public bool? Cluster { get; set; }

        /// <summary>
        /// The maximum zoom level in which to cluster points.
        /// </summary>
        public int? ClusterMaxZoom { get; set; }

        /// <summary>
        /// The radius of each cluster in pixels.
        /// </summary>
        public int? ClusterRadius { get; set; }

        /// <summary>
        /// Specifies whether to calculate line distance metrics.
        /// This is required for line layers that specify `lineGradient` values.
        /// </summary>
        public bool? LineMetrics { get; set; }

        /// <summary>
        /// Maximum zoom level at which to create vector tiles (higher means greater detail at high zoom levels).
        /// </summary>
        public int? MaxZoom { get; set; }

        /// <summary>
        /// The Douglas-Peucker simplification tolerance that is applied to the data when rendering (higher means simpler geometries and faster performance).
        /// </summary>
        public double? Tolerance { get; set; }
    }
}
