namespace AzureMapsControl.Components.Animations
{
    public interface IDurationAnimationOptions : IAnimationOptions
    {
        /// <summary>
        /// The duration of the animation in ms
        /// </summary>
        int? Duration { get; set; }
    }
}
