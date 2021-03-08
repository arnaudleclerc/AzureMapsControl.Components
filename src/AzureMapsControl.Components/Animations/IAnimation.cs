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
    }
}
