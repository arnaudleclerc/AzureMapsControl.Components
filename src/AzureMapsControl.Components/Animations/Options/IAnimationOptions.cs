namespace AzureMapsControl.Components.Animations.Options
{
    /// <summary>
    /// Base animation options
    /// </summary>
    public interface IAnimationOptions
    {
        /// <summary>
        /// Specifies if the animation should start automatically or wait for the play function to be called
        /// </summary>
        bool? AutoPlay { get; set; }
    }
}
