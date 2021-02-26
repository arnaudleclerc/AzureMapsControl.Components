namespace AzureMapsControl.Components.Animations
{
    public interface IPathAnimationOptions : IAnimationOptions
    {
        /// <summary>
        /// Specifies if metadata should be captured as properties of the shape. Potential metadata properties that may be captured: _heading
        /// </summary>
        bool? CaptureMetadata { get; set; }
        /// <summary>
        /// Specifies if a curved geodesic path should be used between points rather than a straight pixel path
        /// </summary>
        bool? Geodesic { get; set; }
    }
}
