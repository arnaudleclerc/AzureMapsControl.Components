namespace AzureMapsControl.Map
{
    /// <summary>
    /// Global properties used in all atlas service requests.
    /// </summary>
    internal class ServiceOptions
    {
        /// <summary>
        /// Disable telemetry collection
        /// This option may only be set when initializing the map.
        /// </summary>
        public bool DisableTelemetry { get; set; }

        /// <summary>
        /// Enable accessibility
        /// </summary>
        public bool EnableAccessibility { get; set; }

        /// <summary>
        /// A boolean that specifies if vector and raster tiles should be reloaded when they expire (based on expires header).
        /// This is useful for data sets that update frequently. When set to false, each tile will be loaded once, when needed, and not reloaded when they expire.
        /// <summary>
        public bool RefreshExpiredTiles { get; set; }
    }
}
