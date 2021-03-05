namespace AzureMapsControl.Components.Animations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations.Options;
    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Markers;

    public interface IAnimationService
    {
        /// <summary>
        /// Animates the path of a LineString. 
        /// </summary>
        /// <param name="line">A LineString shape to animate.</param>
        /// <param name="source">DataSource the given line string is attached to</param>
        /// <param name="options">Options for the animation.</param>
        /// <returns>Animation</returns>
        Task<ISnakeLineAnimation> SnakelineAsync(LineString line, DataSource source, SnakeLineAnimationOptions options = default);

        /// <summary>
        /// Animates a map and/or a Point shape, or marker along a path.
        /// </summary>
        /// <param name="path">The path to animate the point along.</param>
        /// <param name="pathSource">The data source the given line is attached to</param>
        /// <param name="pin">A point to animate along the path</param>
        /// <param name="pinSource">The data source the given point is attached to</param>
        /// <param name="options">Options for the animation</param>
        /// <returns>Animation</returns>
        Task<IMoveAlongPathAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default);

        /// <summary>
        /// Animates a map and/or a Point shape, or marker along a path.
        /// </summary>
        /// <param name="path">The path to animate the point along.</param>
        /// <param name="lineSource">The data source the given line is attached to</param>
        /// <param name="pin">An HTML Marker to animate along the path</param>
        /// <param name="options">Options for the animation</param>
        /// <returns>Animation</returns>
        Task<IMoveAlongPathAnimation> MoveAlongPathAsync(LineString path, DataSource pathSource, HtmlMarker pin, MoveAlongPathAnimationOptions options = default);

        /// <summary>
        /// Animates a map and/or a Point shape, or marker along a path.
        /// </summary>
        /// <param name="path">The path to animate the point along.</param>
        /// <param name="pin">A point to animate along the path</param>
        /// <param name="pinSource">The data source the given point is attached to</param>
        /// <param name="options">Options for the animation</param>
        /// <returns>Animation</returns>
        Task<IMoveAlongPathAnimation> MoveAlongPathAsync(IEnumerable<Position> path, Point pin, DataSource pinSource, MoveAlongPathAnimationOptions options = default);

        /// <summary>
        /// Animates a map and/or a Point shape, or marker along a path.
        /// </summary>
        /// <param name="path">The path to animate the point along.</param>
        /// <param name="pin">A point to animate along the path</param>
        /// <param name="options">Options for the animation</param>
        /// <returns>Animation</returns>
        Task<IMoveAlongPathAnimation> MoveAlongPathAsync(IEnumerable<Position> path, HtmlMarker pin, MoveAlongPathAnimationOptions options = default);

        /// <summary>
        /// Animates the dash-array of a line layer to make it appear to flow. 
        /// </summary>
        /// <param name="layer">LineLayer to animate</param>
        /// <param name="options">Options of the animations</param>
        /// <returns>Animation</returns>
        Task<IFlowingDashedLineAnimation> FlowingDashedLineAsync(LineLayer layer, MovingDashLineOptions options = default);

        /// <summary>
        /// Adds an offset to HtmlMarkers to animate its y value to simulate dropping. Animation modifies `pixelOffset` value of HtmlMarkers.
        /// </summary>
        /// <param name="markers">HtmlMarkers to drop in.</param>
        /// <param name="height">The height at which to drop the shape from.</param>
        /// <param name="options">Options for the animation.</param>
        /// <returns></returns>
        Task<IDropMarkersAnimation> DropMarkersAsync(IEnumerable<HtmlMarker> markers, decimal? height = null, DropMarkersAnimationOptions options = default);

        /// <summary>
        /// Adds an offset to an HtmlMarker to animate its y value to simulate dropping. Animation modifies `pixelOffset` value of HtmlMarker.
        /// </summary>
        /// <param name="markers">HtmlMarkers to drop in.</param>
        /// <param name="height">The height at which to drop the shape from.</param>
        /// <param name="options">Options for the animation.</param>
        /// <returns></returns>
        Task<IDropMarkersAnimation> DropMarkerAsync(HtmlMarker marker, decimal? height = null, DropMarkersAnimationOptions options = default);

        /// <summary>
        /// Group animations
        /// </summary>
        /// <param name="animations">Animations to group</param>
        /// <param name="options">Options for the animation</param>
        /// <returns>Animation</returns>
        Task<IGroupAnimation> GroupAnimationAsync(IEnumerable<IAnimation> animations, GroupAnimationOptions options = default);

        /// <summary>
        /// Adds an offset array property to point shapes and animates its y value to simulate dropping.
        /// </summary>
        /// <param name="points">A one or more point geometries to drop in</param>
        /// <param name="source">The data source to drop the point shapes into.</param>
        /// <param name="height">The height at which to drop the shape from</param>
        /// <param name="options">Options for the animation</param>
        /// <returns>Animation</returns>
        Task<IDropAnimation> DropAsync(IEnumerable<Point> points, DataSource source, decimal? height = null, DropAnimationOptions options = default);

        /// <summary>
        /// Adds an offset array property to a point shape and animates its y value to simulate dropping.
        /// </summary>
        /// <param name="point">A point to drop in</param>
        /// <param name="source">The data source to drop the point shapes into.</param>
        /// <param name="height">The height at which to drop the shape from</param>
        /// <param name="options">Options for the animation</param>
        /// <returns>Animation</returns>
        Task<IDropAnimation> DropAsync(Point point, DataSource source, decimal? height = null, DropAnimationOptions options = default);

        /// <summary>
        /// Animates the update of coordinates on a shape. Shapes will stay the same type. Only base animation options supported for geometries other than Point. 
        /// </summary>
        /// <typeparam name="TPosition">Dimension of the shape</typeparam>
        /// <param name="geometry">The shape to animate.</param>
        /// <param name="source">Datasource the geometry is attached to</param>
        /// <param name="newCoordinates">The new coordinates of the shape.</param>
        /// <param name="options">Options for the animation</param>
        /// <returns>Animation</returns>
        Task<ISetCoordinatesAnimation> SetCoordinatesAsync<TPosition>(Geometry<TPosition> geometry, DataSource source, TPosition newCoordinates, SetCoordinatesAnimationOptions options = default);

        /// <summary>
        /// Animates the update of coordinates on a HtmlMarker.
        /// </summary>
        /// <param name="marker">The marker to animate.</param>
        /// <param name="newCoordinates">The new coordinates of the marker.</param>
        /// <param name="options">Options for the animation.</param>
        /// <returns>Animation</returns>
        Task<ISetCoordinatesAnimation> SetCoordinatesAsync(HtmlMarker marker, Position newCoordinates, SetCoordinatesAnimationOptions options = default);

        /// <summary>
        /// Animates the morphing of a shape from one geometry type or set of coordinates to another.
        /// </summary>
        /// <typeparam name="T">Type of geometry the shape will be turned into</typeparam>
        /// <param name="geometry">The shape to animate.</param>
        /// <param name="source">Datasource the shape is attached to</param>
        /// <param name="newGeometry">The new geometry to turn the shape into.</param>
        /// <param name="options">Options for the animation</param>
        /// <returns>Animation</returns>
        Task<IMorphAnimation> MorphAsync<T>(Geometry geometry, DataSource source, T newGeometry, MorphAnimationOptions options = default) where T : Geometry;
    }
}
