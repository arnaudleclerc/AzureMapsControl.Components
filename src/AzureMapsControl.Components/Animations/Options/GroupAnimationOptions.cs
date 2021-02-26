namespace AzureMapsControl.Components.Animations.Options
{
    public struct GroupAnimationOptions : IAnimationOptions
    {
        /// <summary>
        /// Specifies if the animation should start automatically or wait for the play function to be called.
        /// </summary>
        public bool? AutoPlay { get; set; }

        /// <summary>
        /// If the `playType` is set to `interval`, this option specifies the time interval to start each animation in milliseconds.
        /// </summary>
        public decimal? Interval { get; set; }

        /// <summary>
        /// How to play the animations.
        /// </summary>
        public PlayType PlayType { get; set; }
    }
}
