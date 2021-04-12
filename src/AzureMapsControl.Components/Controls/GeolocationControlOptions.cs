namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Geolocation;

    [ExcludeFromCodeCoverage]
    public sealed class GeolocationControlOptions : IControlOptions
    {
        /// <summary>
        /// Specifies that if the `speed` or `heading` values are missing in the geolocation position, it will calculate these values based on the last known position.
        /// </summary>
        public bool? CalculateMissingValues { get; set; }

        /// <summary>
        /// The color of the user location marker.
        /// </summary>
        public string MarkerColor { get; set; }

        /// <summary>
        /// The maximum zoom level the map can be zoomed out. 
        /// If zoomed out more than this when location updates, the map will zoom into this level. 
        /// If zoomed in more than this level, the map will maintain its current zoom level.
        /// </summary>
        public int? MaxZoom { get; set; }

        /// <summary>
        /// A Geolocation API PositionOptions object.
        /// </summary>
        public PositionOptions PositionOptions { get; set; }

        /// <summary>
        /// Shows the users location on the map using a marker.
        /// </summary>
        public bool? ShowUserLocation { get; set; }

        /// <summary>
        /// The style of the control.
        /// </summary>
        public ControlStyle Style { get; set; }

        /// <summary>
        /// If `true` the geolocation control becomes a toggle button and when active the map will receive updates to the user's location as it changes.
        /// </summary>
        public bool? TrackUserLocation { get; set; }

        /// <summary>
        /// Specifies if the map camera should update as the position moves. When set to `true`, the map camera will update to the new position, unless the user has interacted with the map.
        /// </summary>
        public bool? UpdateMapCamera { get; set; }
    }
}
