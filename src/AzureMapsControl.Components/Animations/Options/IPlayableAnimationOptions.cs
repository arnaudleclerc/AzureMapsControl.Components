namespace AzureMapsControl.Components.Animations.Options
{
    public interface IPlayableAnimationOptions
    {
        /// <summary>
        /// The easing of the animaiton.
        /// </summary>
        Easing Easing { get; set; }

        /// <summary>
        /// Specifies if the animation should loop infinitely.
        /// </summary>
        bool? Loop { get; set; }

        /// <summary>
        /// Specifies if the animation should play backwards.
        /// </summary>
        bool? Reverse { get; set; }

        /// <summary>
        /// A multiplier of the duration to speed up or down the animation.
        /// </summary>
        decimal? SpeedMultiplier { get; set; }
    }
}
