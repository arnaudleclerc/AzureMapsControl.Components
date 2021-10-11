namespace AzureMapsControl.Components.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    public abstract class BaseSource<TOptions> : Source<TOptions> where TOptions : SourceOptions, new()
    {
        private List<Shape> _shapes;
        private List<Feature> _features;

        internal ILogger Logger { get; set; }
        internal IMapJsRuntime JSRuntime { get; set; }

        public bool Disposed { get; private set; }

        public IEnumerable<Shape> Shapes => _shapes;

        public IEnumerable<Feature> Features => _features;

        internal BaseSource(string id, SourceType type) : base(id, type) { }

        /// <summary>
        /// Add a Json containing a Feature Collection to the source
        /// </summary>
        /// <param name="json">JSON containing the feature collection</param>
        /// <returns></returns>
        /// <exception cref="JsonException">The given string is not a valid JSON</exception>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(string json) => await AddAsync(JsonDocument.Parse(json));

        /// <summary>
        /// Add a Json containing a Feature Collection to the source
        /// </summary>
        /// <param name="json">JSON containing the feature collection</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(JsonDocument json)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_AddAsync, "Adding json to source");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, Id);

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatureCollection.ToSourceNamespace(), Id, json);
        }

        /// <summary>
        /// Remove shapes and features from the source
        /// </summary>
        /// <param name="ids">IDs of the shapes and features to remove</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask RemoveAsync(IEnumerable<string> ids)
        {
            var shapeIdsToRemove = _shapes?.Where(shape => ids.Contains(shape.Id)).Select(shape => shape.Id);
            var featureIdsToRemove = _features?.Where(feature => ids.Contains(feature.Id)).Select(feature => feature.Id);
            var idsToRemove = new List<string>();
            if (shapeIdsToRemove != null)
            {
                idsToRemove.AddRange(shapeIdsToRemove);
                _shapes.RemoveAll(geometry => shapeIdsToRemove.Contains(geometry.Id));
            }

            if (featureIdsToRemove != null)
            {
                idsToRemove.AddRange(featureIdsToRemove);
                _features.RemoveAll(feature => featureIdsToRemove.Contains(feature.Id));
            }

            if (idsToRemove.Any())
            {
                EnsureJsRuntimeExists();
                EnsureNotDisposed();

                Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_RemoveAsync, "Removing geometries from data source");
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_RemoveAsync, $"Id: {Id} | Ids: {string.Join('|', ids)}");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), Id, ids);
            }
        }

        /// <summary>
        /// Remove shapes and features from the source
        /// </summary>
        /// <param name="ids">IDs of the shapes and features to remove</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask RemoveAsync(params string[] ids) => await RemoveAsync(ids as IEnumerable<string>);

        /// <summary>
        /// Remove shapes and features from the source
        /// </summary>
        /// <param name="shapes">Shapes to remove</param>
        /// <param name="features">Features to remove</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask RemoveAsync(IEnumerable<Shape> shapes, IEnumerable<Feature> features)
        {
            var ids = new List<string>();
            if (shapes != null)
            {
                ids.AddRange(shapes.Select(shape => shape.Id));
            }

            if (features != null)
            {
                ids.AddRange(features.Select(feature => feature.Id));
            }

            await RemoveAsync(ids);
        }

        /// <summary>
        /// Remove shapes from the source
        /// </summary>
        /// <param name="shapes">Shapes to remove</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask RemoveAsync(IEnumerable<Shape> shapes) => await RemoveAsync(shapes, null);

        /// <summary>
        /// Remove shapes from the source
        /// </summary>
        /// <param name="shapes">Shapes to remove</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask RemoveAsync(params Shape[] shapes) => await RemoveAsync(shapes, null);

        /// <summary>
        /// Remove features from the source
        /// </summary>
        /// <param name="features">Features to remove</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask RemoveAsync(IEnumerable<Feature> features) => await RemoveAsync(null, features);

        /// <summary>
        /// Remove features from the source
        /// </summary>
        /// <param name="features">Features to remove</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask RemoveAsync(params Feature[] features) => await RemoveAsync(null, features);

        /// <summary>
        /// Imports data from an URL into a source
        /// </summary>
        /// <param name="dataSourceId">ID of the source on which the data will be imported</param>
        /// <param name="url">Url to import the data from</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        /// <exception cref="Microsoft.JSInterop.JSException">The call to importDataFromUrl failed</exception>
        public async ValueTask ImportDataFromUrlAsync(string url)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_ImportDataFromUrlAsync, "Importing data from url into source");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_ImportDataFromUrlAsync, $"Id: {Id} | Url: {url}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.ImportDataFromUrl.ToSourceNamespace(), Id, url);
        }

        /// <summary>
        /// Clear the source
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask ClearAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_ClearAsync, "Clearing source");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_ClearAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            _shapes = null;
            _features = null;
            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToSourceNamespace(), Id);
        }

        /// <summary>
        /// Cleans up any resources this object is consuming.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask DisposeAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_DisposeAsync, "BaseSource - DisposeAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_DisposeAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), Id);
            Disposed = true;
        }

        /// <summary>
        /// Gets the options used by the source.
        /// </summary>
        /// <returns>Current options of the datasource</returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask<TOptions> GetOptionsAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_GetOptionsAsync, "BaseSource - GetOptionsAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_GetOptionsAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            Options = await JSRuntime.InvokeAsync<TOptions>(Constants.JsConstants.Methods.Source.GetOptions.ToSourceNamespace(), Id);
            return Options;
        }

        /// <summary>
        /// Sets the source options.
        /// </summary>
        /// <param name="options">Options to be set</param>
        /// <returns></returns>
        public async ValueTask SetOptionsAsync(Action<TOptions> update)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_SetOptionsAsync, "BaseSource - SetOptionsAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_SetOptionsAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            if(Options == null)
            {
                Options = new TOptions();
            }

            update(Options);

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.SetOptions.ToSourceNamespace(), Id, Options);
        }

        /// <summary>
        /// Add shapes to the data source
        /// </summary>
        /// <param name="shapes">Shapes to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        protected async ValueTask AddShapesAsync(IEnumerable<Shape> shapes)
        {
            if (shapes == null || !shapes.Any())
            {
                return;
            }

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            if (_shapes == null)
            {
                _shapes = new List<Shape>();
            }

            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"Id: {Id}");
            var lineStrings = shapes.OfType<Shape<LineString>>();
            if (lineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{lineStrings.Count()} linestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), Id, lineStrings);
            }

            var multiLineStrings = shapes.OfType<Shape<MultiLineString>>();
            if (multiLineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{multiLineStrings.Count()} multilinestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), Id, multiLineStrings);
            }

            var multiPoints = shapes.OfType<Shape<MultiPoint>>();
            if (multiPoints.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{multiPoints.Count()} multipoints will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), Id, multiPoints);
            }

            var multiPolygons = shapes.OfType<Shape<MultiPolygon>>();
            if (multiPolygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{multiPolygons.Count()} multipolygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), Id, multiPolygons);
            }

            var points = shapes.OfType<Shape<Point>>();
            if (points.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{points.Count()} points will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), Id, points);
            }

            var polygons = shapes.OfType<Shape<Polygon>>();
            if (polygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{polygons.Count()} polygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), Id, polygons);
            }

            var routePoints = shapes.OfType<Shape<RoutePoint>>();
            if (routePoints.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{routePoints.Count()} route points will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), Id, routePoints);
            }

            _shapes.AddRange(shapes);
        }

        /// <summary>
        /// Add features to the data source
        /// </summary>
        /// <param name="features">Features to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        protected async ValueTask AddFeaturesAsync(IEnumerable<Feature> features)
        {
            if (features == null || !features.Any())
            {
                return;
            }

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            if (_features == null)
            {
                _features = new List<Feature>();
            }

            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"Id: {Id}");
            var lineStrings = features.OfType<Feature<LineString>>();
            if (lineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{lineStrings.Count()} linestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, lineStrings);
            }

            var multiLineStrings = features.OfType<Feature<MultiLineString>>();
            if (multiLineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{multiLineStrings.Count()} multilinestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, multiLineStrings);
            }

            var multiPoints = features.OfType<Feature<MultiPoint>>();
            if (multiPoints.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{multiPoints.Count()} multipoints will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, multiPoints);
            }

            var multiPolygons = features.OfType<Feature<MultiPolygon>>();
            if (multiPolygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{multiPolygons.Count()} multipolygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, multiPolygons);
            }

            var points = features.OfType<Feature<Point>>();
            if (points.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{points.Count()} points will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, points);
            }

            var polygons = features.OfType<Feature<Polygon>>();
            if (polygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{polygons.Count()} polygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, polygons);
            }

            var routePoints = features.OfType<Feature<RoutePoint>>();
            if (routePoints.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_AddAsync, $"{routePoints.Count()} route points will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, routePoints);
            }

            _features.AddRange(features);
        }

        protected void EnsureJsRuntimeExists()
        {
            if (JSRuntime is null)
            {
                throw new Exceptions.ComponentNotAddedToMapException();
            }
        }

        protected void EnsureNotDisposed()
        {
            if (Disposed)
            {
                throw new Exceptions.ComponentDisposedException();
            }
        }

    }
}
