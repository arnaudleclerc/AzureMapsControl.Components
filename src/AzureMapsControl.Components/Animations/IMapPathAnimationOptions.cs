namespace AzureMapsControl.Components.Animations
{
    public interface IMapPathAnimationOptions : IAnimationOptions, IDisposableAnimationOptions, IDurationAnimationOptions
    {
        /// <summary>
        /// A pitch value to set on the map. By default this is not set.
        /// </summary>
        int? Pitch { get; set; }

        /// <summary>
        /// Specifies if the map should rotate such that the bearing of the map faces the direction the map is moving.
        /// </summary>
        bool? Rotate { get; set; }

        /// <summary>
        /// When rotate is set to true, the animation will follow the animation. An offset of 180 will cause the camera to lead the animation and look back.
        /// </summary>
        int? RotationOffset { get; set; }

        /// <summary>
        /// A fixed zoom level to snap the map to on each animation frame. By default the maps current zoom level is used.
        /// </summary>
        int? Zoom { get; set; }
    }
}
