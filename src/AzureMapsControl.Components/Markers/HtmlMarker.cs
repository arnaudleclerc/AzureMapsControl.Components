namespace AzureMapsControl.Components.Markers
{
    /// <summary>
    /// HTML Marker which can be added directly to the map
    /// </summary>
    public sealed class HtmlMarker
    {
        internal HtmlMarkerOptions Options { get; }

        /// <summary>
        /// Events to activate on the marker
        /// </summary>
        public HtmlMarkerEventActivationFlags EventActivationFlags { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options of the HtmlMarker</param>
        public HtmlMarker(HtmlMarkerOptions options) : this(options, HtmlMarkerEventActivationFlags.All) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options of the HtmlMarker</param>
        /// <param name="eventActivationFlags">Events to activate on the marker</param>
        public HtmlMarker(HtmlMarkerOptions options, HtmlMarkerEventActivationFlags eventActivationFlags)
        {
            Options = options;
            EventActivationFlags = eventActivationFlags;
        }

    }
}
