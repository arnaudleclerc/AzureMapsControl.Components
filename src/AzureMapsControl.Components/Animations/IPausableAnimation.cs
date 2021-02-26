namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    public interface IPausableAnimation : IAnimation
    {
        /// <summary>
        /// Pauses the animation.
        /// </summary>
        /// <returns></returns>
        Task PauseAsync();
    }
}
