namespace AzureMapsControl.Components.Animations.Options
{
    public interface IDurationAnimationOptions : IAnimationOptions
    {
        /// <summary>
        /// The duration of the animation in ms
        /// </summary>
        int? Duration { get; set; }
    }
}
