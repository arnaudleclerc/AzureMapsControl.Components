namespace AzureMapsControl.Components.Animations.Options
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Options for animating the map along a path.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public struct RoutePathAnimationOptions : IAnimationOptions
    {
        /// <summary>
        /// Interpolation calculations to perform on property values between points during the animation. Requires `captureMetadata` to be enabled.
        /// </summary>
        public IEnumerable<PointPairValueInterpolation> ValueInterpolations { get; set; }

        /// <summary>
        /// Specifies if metadata should be captured as properties of the shape. Potential metadata properties that may be captured: heading, speed, timestamp
        /// </summary>
        public bool? CaptureMetadata { get; set; }

        /// <summary>
        /// A fixed zoom level to snap the map to on each animation frame. By default the maps current zoom level is used.
        /// </summary>
        public int? Zoom { get; set; }

        /// <summary>
        /// A pitch value to set on the map. By default this is not set.
        /// </summary>
        public int? Pitch { get; set; }

        /// <summary>
        /// Specifies if the map should rotate such that the bearing of the map faces the direction the map is moving.
        /// </summary>
        public bool? Rotate { get; set; }

        /// <summary>
        /// When rotate is set to true, the animation will follow the animation. An offset of 180 will cause the camera to lead the animation and look back.
        /// </summary>
        public int? RotationOffset { get; set; }

        /// <summary>
        /// Specifies if the animation should start automatically or wait for the play function to be called.
        /// </summary>
        public bool? AutoPlay { get; set; }

        /// <summary>
        /// Specifies if the animation should loop infinitely.
        /// </summary>
        public bool? Loop { get; set; }

        /// <summary>
        /// Specifies if the animation should play backwards.
        /// </summary>
        public bool? Reverse { get; set; }

        /// <summary>
        /// A multiplier of the duration to speed up or down the animation.
        /// </summary>
        public decimal? SpeedMultiplier { get; set; }
    }
}
