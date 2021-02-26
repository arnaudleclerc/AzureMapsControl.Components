namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations.Options;

    public interface IUpdatableAnimation<TOptions> : IPausableAnimation
        where TOptions: IAnimationOptions
    {
        /// <summary>
        /// Advances the animation to specific step. 
        /// </summary>
        /// <param name="progress">The progress of the animation to advance to. A value between 0 and 1.</param>
        /// <returns></returns>
        Task SeekAsync(decimal progress);

        /// <summary>
        /// Sets the options of the animation.
        /// </summary>
        /// <param name="options">Options to update the animation with</param>
        /// <returns></returns>
        Task SetOptionsAsync(TOptions options);
    }
}
