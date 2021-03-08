namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations.Options;

    public interface IGroupAnimation : IAnimation
    {
        /// <summary>
        /// Disposes the animation
        /// </summary>
        /// <returns></returns>
        Task DisposeAsync();

        /// <summary>
        /// Plays the animation.
        /// </summary>
        /// <returns></returns>
        Task PlayAsync();

        /// <summary>
        /// Stops the animation and jumps back to the beginning of the animation. 
        /// </summary>
        /// <returns></returns>
        Task ResetAsync();

        /// <summary>
        /// Stops the animation and jumps back to the end of the animation. 
        /// </summary>
        /// <returns></returns>
        Task StopAsync();

        /// <summary>
        /// Sets the options of the animation.
        /// </summary>
        /// <param name="options">Options to update the animation with</param>
        /// <returns></returns>
        Task SetOptionsAsync(GroupAnimationOptions options);
    }
}
