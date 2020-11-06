namespace AzureMapsControl.Components.Traffic
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Contains the configuration to display traffic on the map
    /// </summary>

    [ExcludeFromCodeCoverage]
    public sealed class TrafficOptions
    {
        /// <summary>
        /// The type of traffic flow to display
        /// </summary>
        public TrafficFlow Flow { get; set; }

        /// <summary>
        /// Whether to display incidents on the map.
        /// </summary>
        public bool? Incidents { get; set; }
    }
}
