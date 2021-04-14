namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations.Options;

    public interface IMorphAnimation : IAnimation
    {
        /// <summary>
        /// Disposes the animation
        /// </summary>
        /// <returns></returns>
        ValueTask DisposeAsync();

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask PlayAsync();

        /// <summary>
        /// Stops the animation and jumps back to the beginning of the animation. 
        /// </summary>
        /// <returns></returns>
        ValueTask ResetAsync();

        /// <summary>
        /// Stops the animation and jumps back to the end of the animation. 
        /// </summary>
        /// <returns></returns>
        ValueTask StopAsync();

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        /// <returns></returns>
        ValueTask PauseAsync();

        /// <summary>
        /// Advances the animation to specific step. 
        /// </summary>
        /// <param name="progress">The progress of the animation to advance to. A value between 0 and 1.</param>
        /// <returns></returns>
        ValueTask SeekAsync(decimal progress);

        /// <summary>
        /// Sets the options of the animation.
        /// </summary>
        /// <param name="options">Options to update the animation with</param>
        /// <returns></returns>
        ValueTask SetOptionsAsync(MorphAnimationOptions options);
    }
}
