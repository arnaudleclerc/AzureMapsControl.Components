namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    public interface IAnimation
    {
        /// <summary>
        /// Id of the animation
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Indicates if the animation is disposed.
        /// </summary>
        public bool Disposed { get; }

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
    }
}
