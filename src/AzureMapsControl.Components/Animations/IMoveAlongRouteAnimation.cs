namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    public interface IMoveAlongRouteAnimation : IAnimation
    {
        /// <summary>
        /// Disposes the animation
        /// </summary>
        /// <returns></returns>
        Task DisposeAsync();
    }
}
