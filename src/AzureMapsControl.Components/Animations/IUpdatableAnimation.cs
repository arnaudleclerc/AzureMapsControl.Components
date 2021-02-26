namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations.Options;

    public interface IUpdatableAnimation<TOptions>
        where TOptions: IAnimationOptions
    {
        /// <summary>
        /// Sets the options of the animation.
        /// </summary>
        /// <param name="options">Options to update the animation with</param>
        /// <returns></returns>
        Task SetOptionsAsync(TOptions options);
    }
}
