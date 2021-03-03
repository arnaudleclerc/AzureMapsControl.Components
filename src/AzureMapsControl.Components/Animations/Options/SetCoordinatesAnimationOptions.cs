namespace AzureMapsControl.Components.Animations.Options
{
    public struct SetCoordinatesAnimationOptions : IPathAnimationOptions, IMapPathAnimationOptions, IPlayableAnimationOptions, IDisposableAnimationOptions, IDurationAnimationOptions, IAnimationOptions
    {
        public bool? CaptureMetadata { get; set; }
        public bool? Geodesic { get; set; }
        public int? Pitch { get; set; }
        public bool? Rotate { get; set; }
        public int? RotationOffset { get; set; }
        public int? Zoom { get; set; }
        public Easing Easing { get; set; }
        public bool? Loop { get; set; }
        public bool? Reverse { get; set; }
        public decimal? SpeedMultiplier { get; set; }
        public bool? DisposeOnComplete { get; set; }
        public int? Duration { get; set; }
        public bool? AutoPlay { get; set; }
    }
}
