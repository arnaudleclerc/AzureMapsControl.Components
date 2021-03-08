namespace AzureMapsControl.Components.Animations.Options
{
    public struct DropMarkersAnimationOptions : IAnimationOptions
    {
        /// <summary>
        /// Specifies if the animation should start automatically or wait for the play function to be called.
        /// </summary>
        public bool? AutoPlay { get; set; }

        /// <summary>
        /// The easing of the animaiton
        /// </summary>
        public Easing Easing { get; set; }

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

        /// <summary>
        /// The duration of the animation in ms.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Specifies if the animation should dispose itself once it has completed.
        /// </summary>
        public bool? DisposeOnComplete { get; set; }
    }
}
