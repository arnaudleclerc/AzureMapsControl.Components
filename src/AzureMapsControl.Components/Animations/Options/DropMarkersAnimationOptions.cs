namespace AzureMapsControl.Components.Animations.Options
{
    public struct DropMarkersAnimationOptions : IDisposableAnimationOptions, IDurationAnimationOptions, IAnimationOptions
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool? AutoPlay { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Easing Easing { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool? Loop { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool? Reverse { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public decimal? SpeedMultiplier { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int? Duration { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool? DisposeOnComplete { get; set; }
    }
}
