namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    public sealed class CameraOptions
    {
        /// <summary>
        /// The bearing of the map (rotation).
        /// default: North
        /// </summary>
        public Bearing Bearing { get; set; } = Bearing.North;

        /// <summary>
        /// The bounds of the map control's camera
        /// </summary>
        public BoundingBox Bounds { get; set; }

        /// <summary>
        /// The position to align the center of the map view with
        /// </summary>
        public Position Center { get; set; }

        /// <summary>
        /// A pixel offset to apply to the center of the map.
        /// This is useful if you want to programmatically pan the map to another location or if you want to center the map over a shape, then offset the maps view to make room for a popup.
        /// </summary>
        public Pixel CenterOffset { get; set; }

        /// <summary>
        /// The duration of the animation in milliseconds.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// A bounding box in which to constrain the viewable map area to. Users won't be able to pan the center of the map outside of this bounding box. Set maxBounds to null or undefined to remove maxBounds
        /// </summary>
        public BoundingBox MaxBounds { get; set; }

        /// <summary>
        /// The maximum zoom level that the map can be zoomed into during the animation. Must be between 0 and 24, and greater than or equal to `minZoom`.
        /// </summary>
        public int? MaxZoom { get; set; }

        /// <summary>
        /// The minimum zoom level that the map can be zoomed out to during the animation. Must be between 0 and 24, and less than or equal to `maxZoom`.
        /// </summary>
        public int? MinZoom { get; set; }

        /// <summary>
        /// An offset of the center of the given bounds relative to the map's center, measured in pixels
        /// </summary>
        public Pixel Offset { get; set; }

        /// <summary>
        /// The amount of padding in pixels to add to the given bounds
        /// </summary>
        public Padding Padding { get; set; }

        /// <summary>
        /// The pitch (tilt) of the map in degrees between 0 and 60, where 0 is looking straight down on the map.
        /// </summary>
        public int? Pitch { get; set; }

        /// <summary>
        /// The type of animation.
        /// "jump" is an immediate change.
        /// "ease" is a gradual change of the camera's settings.
        /// "fly" is a gradual change of the camera's settings following an arc resembling flight.
        /// </summary>
        public CameraType Type { get; set; }

        /// <summary>
        /// The zoom level of the map view
        /// </summary>
        public int? Zoom { get; set; }
    }
}
