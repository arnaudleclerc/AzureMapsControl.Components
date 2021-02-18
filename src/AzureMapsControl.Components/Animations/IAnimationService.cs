namespace AzureMapsControl.Components.Animations
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;

    public interface IAnimationService
    {
        /// <summary>
        /// Animates the path of a LineString. 
        /// </summary>
        /// <param name="line">A LineString shape to animate.</param>
        /// <param name="source">DataSource the given line string is attached to</param>
        /// <param name="options">Options for the animation.</param>
        /// <returns>Animation</returns>
        Task<PlayableAnimation> SnakelineAsync(LineString line, DataSource source, SnakeLineAnimationOptions options);
    }
}
