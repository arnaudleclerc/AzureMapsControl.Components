namespace AzureMapsControl.Components.Animations.Options
{
    public struct AnimationOptions : IAnimationOptions
    {
        public bool? AutoPlay { get; set; }
        public bool? DisposeOnComplete { get; set; }
        public int? Duration { get; set; }
        public Easing Easing { get; set; }
        public bool? Loop { get; set; }
        public bool? Reverse { get; set; }
        public decimal? SpeedMultiplier { get; set; }
    }
}
