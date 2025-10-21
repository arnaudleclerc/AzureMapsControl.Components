namespace AzureMapsControl.Components.Drawing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;


    /// <summary>
    /// DrawingManager for the DrawingToolbar
    /// </summary>
    public sealed class DrawingManager
    {
        internal IMapJsRuntime JSRuntime { get; set; }
        internal ILogger Logger { get; set; }
        public bool Disposed { get; private set; }

        /// <summary>
        /// List of shapes added to the data source
        /// </summary>
        private List<Shape> _sourceShapes;

        /// <summary>
        /// Add shapes to the drawing manager data source
        /// </summary>
        /// <param name="shapes">Shapes to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddShapesAsync(IEnumerable<Shape> shapes)
        {
            if (shapes == null || !shapes.Any())
            {
                return;
            }

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            if (_sourceShapes == null)
            {
                _sourceShapes = new List<Shape>();
            }

            var lineStrings = shapes.OfType<Shape<LineString>>();
            if (lineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{lineStrings.Count()} linestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), lineStrings);
            }

            var multiLineStrings = shapes.OfType<Shape<MultiLineString>>();
            if (multiLineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{multiLineStrings.Count()} multilinestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), multiLineStrings);
            }

            var multiPoints = shapes.OfType<Shape<MultiPoint>>();
            if (multiPoints.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{multiPoints.Count()} multipoints will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), multiPoints);
            }

            var multiPolygons = shapes.OfType<Shape<MultiPolygon>>();
            if (multiPolygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{multiPolygons.Count()} multipolygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), multiPolygons);
            }

            var points = shapes.OfType<Shape<Point>>();
            if (points.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{points.Count()} points will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), points);
            }

            var polygons = shapes.OfType<Shape<Polygon>>();
            if (polygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{polygons.Count()} polygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), polygons);
            }

            var routePoints = shapes.OfType<Shape<RoutePoint>>();
            if (routePoints.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{routePoints.Count()} route points will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), routePoints);
            }

            _sourceShapes.AddRange(shapes);
        }

        /// <summary>
        /// Clear the drawing manager source
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask ClearAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_ClearAsync, "Clearing drawing manager source");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            _sourceShapes = null;
            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToDrawingNamespace());
        }

        /// <summary>
        /// Mark the control as disposed
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        internal void Dispose()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_DisposeAsync, "DrawingManager - Dispose");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            Disposed = true;
        }

        private void EnsureJsRuntimeExists()
        {
            if (JSRuntime is null)
            {
                throw new Exceptions.ComponentNotAddedToMapException();
            }
        }

        private void EnsureNotDisposed()
        {
            if (Disposed)
            {
                throw new Exceptions.ComponentDisposedException();
            }
        }
    }
}
