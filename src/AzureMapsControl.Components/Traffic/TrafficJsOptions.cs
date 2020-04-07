namespace AzureMapsControl.Components.Traffic
{
    internal class TrafficJsOptions
    {
        /// <summary>
        /// The type of traffic flow to display
        /// </summary>
        public string Flow { get; set; }

        /// <summary>
        /// Whether to display incidents on the map.
        /// </summary>
        public bool? Incidents { get; set; }
    }
}
