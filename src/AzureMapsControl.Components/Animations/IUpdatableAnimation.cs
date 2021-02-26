namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    public interface IUpdatableAnimation : IAnimation
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
        Task SetOptionsAsync(IAnimationOptions options);
    }
}
