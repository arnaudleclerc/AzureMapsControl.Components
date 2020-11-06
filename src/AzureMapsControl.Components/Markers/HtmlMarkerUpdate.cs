namespace AzureMapsControl.Components.Markers
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Contains the update information of an HtmlMarker
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class HtmlMarkerUpdate
    {
        internal HtmlMarker Marker { get; }
        internal HtmlMarkerOptions Options { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="marker">HtmlMarker to update.</param>
        /// <param name="options">Options to update the HtmlMarker with</param>
        public HtmlMarkerUpdate(HtmlMarker marker, HtmlMarkerOptions options)
        {
            if(!Guid.TryParse(marker?.Id, out _))
            {
                throw new ArgumentException("Please provide a marker which has already been added to the map");
            }
            Marker = marker;
            Options = options;
        }
    }
}
