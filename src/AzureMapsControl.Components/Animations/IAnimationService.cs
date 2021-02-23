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
        Task<PlayableAnimation> SnakelineAsync(LineString line, DataSource source, SnakeLineAnimationOptions options = null);

        /// <summary>
        /// Animates a map and/or a Point shape, or marker along a path.
        /// </summary>
        /// <param name="line">The path to animate the point along.</param>
        /// <param name="lineSource">The data source the given line is attached to</param>
        /// <param name="pin">A point to animate along the path</param>
        /// <param name="pinSource">The data source the given point is attached to</param>
        /// <param name="options">Options for the animation</param>
        /// <returns></returns>
        Task<PlayableAnimation> MoveAlongPathAsync(LineString line, DataSource lineSource, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = null);
    }
}
