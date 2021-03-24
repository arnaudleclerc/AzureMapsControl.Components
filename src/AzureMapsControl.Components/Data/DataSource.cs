namespace AzureMapsControl.Components.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    public sealed class DataSource : Source<DataSourceOptions>
    {
        private List<Geometry> _geometries;
        private List<Feature> _features;

        internal ILogger Logger { get; set; }
        internal IMapJsRuntime JSRuntime { get; set; }

        public IEnumerable<Geometry> Geometries => _geometries;

        public IEnumerable<Feature> Features => _features;

        public DataSource() : this(Guid.NewGuid().ToString()) { }

        public DataSource(string id) : base(id, SourceType.DataSource) { }

        /// <summary>
        /// Add geometries to the data source
        /// </summary>
        /// <param name="geometries">Geometries to add</param>
        /// <returns></returns>
        public async Task AddAsync(IEnumerable<Geometry> geometries)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.DataSource_AddAsync, "Adding geometries to data source");

            if (geometries == null || !geometries.Any())
            {
                return;
            }

            if (_geometries == null)
            {
                _geometries = new List<Geometry>();
            }

            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"Id: {Id}");
            var lineStrings = geometries.OfType<LineString>();
            if (lineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{lineStrings.Count()} linestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), Id, lineStrings);
            }

            var multiLineStrings = geometries.OfType<MultiLineString>();
            if (multiLineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{multiLineStrings.Count()} multilinestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), Id, multiLineStrings);
            }

            var multiPoints = geometries.OfType<MultiPoint>();
            if (multiPoints.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{multiPoints.Count()} multipoints will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), Id, multiPoints);
            }

            var multiPolygons = geometries.OfType<MultiPolygon>();
            if (multiPolygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{multiPolygons.Count()} multipolygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), Id, multiPolygons);
            }

            var points = geometries.OfType<Point>();
            if (points.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{points.Count()} points will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), Id, points);
            }

            var polygons = geometries.OfType<Polygon>();
            if (polygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{polygons.Count()} polygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), Id, polygons);
            }

            _geometries.AddRange(geometries);
        }

        /// <summary>
        /// Add geometries to the data source
        /// </summary>
        /// <param name="geometries">Geometries to add</param>
        /// <returns></returns>
        public async Task AddAsync(params Geometry[] geometries) => await AddAsync(geometries as IEnumerable<Geometry>);

        /// <summary>
        /// Add features to the data source
        /// </summary>
        /// <param name="features">Features to add</param>
        /// <returns></returns>
        public async Task AddAsync(IEnumerable<Feature> features)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.DataSource_AddAsync, "Adding features to data source");

            if (features == null || !features.Any())
            {
                return;
            }

            if (_features == null)
            {
                _features = new List<Feature>();
            }

            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"Id: {Id}");
            var lineStrings = features.OfType<Feature<LineString>>();
            if (lineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{lineStrings.Count()} linestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, lineStrings);
            }

            var multiLineStrings = features.OfType<Feature<MultiLineString>>();
            if (multiLineStrings.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{multiLineStrings.Count()} multilinestrings will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, multiLineStrings);
            }

            var multiPoints = features.OfType<Feature<MultiPoint>>();
            if (multiPoints.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{multiPoints.Count()} multipoints will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, multiPoints);
            }

            var multiPolygons = features.OfType<Feature<MultiPolygon>>();
            if (multiPolygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{multiPolygons.Count()} multipolygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, multiPolygons);
            }

            var points = features.OfType<Feature<Point>>();
            if (points.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{points.Count()} points will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, points);
            }

            var polygons = features.OfType<Feature<Polygon>>();
            if (polygons.Any())
            {
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_AddAsync, $"{polygons.Count()} polygons will be added");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), Id, polygons);
            }

            _features.AddRange(features);
        }

        /// <summary>
        /// Add features to the data source
        /// </summary>
        /// <param name="features">Features to add</param>
        /// <returns></returns>
        public async Task AddAsync(params Feature[] features) => await AddAsync(features as IEnumerable<Feature>);

        /// <summary>
        /// Remove geometriesand features from the data source
        /// </summary>
        /// <param name="ids">IDs of the geometries and features to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<string> ids)
        {
            var geometryIdsToRemove = _geometries?.Where(geometry => ids.Contains(geometry.Id)).Select(geometry => geometry.Id);
            var featureIdsToRemove = _features?.Where(feature => ids.Contains(feature.Id)).Select(feature => feature.Id);
            var idsToRemove = new List<string>();
            if (geometryIdsToRemove != null)
            {
                idsToRemove.AddRange(geometryIdsToRemove);
                _geometries.RemoveAll(geometry => geometryIdsToRemove.Contains(geometry.Id));
            }

            if (featureIdsToRemove != null)
            {
                idsToRemove.AddRange(featureIdsToRemove);
                _features.RemoveAll(feature => featureIdsToRemove.Contains(feature.Id));
            }

            if (idsToRemove.Any())
            {
                Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.DataSource_RemoveAsync, "Removing geometries from data source");
                Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_RemoveAsync, $"Id: {Id} | Ids: {string.Join('|', ids)}");
                await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), Id, ids);
            }
        }

        /// <summary>
        /// Remove geometries and features from the data source
        /// </summary>
        /// <param name="ids">IDs of the geometries and features to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(params string[] ids) => await RemoveAsync(ids as IEnumerable<string>);

        /// <summary>
        /// Remove geometries and features from the datasource
        /// </summary>
        /// <param name="geometries">Geometries to remove</param>
        /// <param name="features">Features to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<Geometry> geometries, IEnumerable<Feature> features)
        {
            var ids = new List<string>();
            if (geometries != null)
            {
                ids.AddRange(geometries.Select(geometry => geometry.Id));
            }

            if (features != null)
            {
                ids.AddRange(features.Select(feature => feature.Id));
            }

            await RemoveAsync(ids);
        }

        /// <summary>
        /// Remove geometries from the datasource
        /// </summary>
        /// <param name="geometries">Geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<Geometry> geometries) => await RemoveAsync(geometries, null);

        /// <summary>
        /// Remove geometries from the data source
        /// </summary>
        /// <param name="geometries">Geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(params Geometry[] geometries) => await RemoveAsync(geometries, null);

        /// <summary>
        /// Remove features from the datasource
        /// </summary>
        /// <param name="features">Features to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<Feature> features) => await RemoveAsync(null, features);

        /// <summary>
        /// Remove features from the data source
        /// </summary>
        /// <param name="features">Features to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(params Feature[] features) => await RemoveAsync(null, features);

        /// <summary>
        /// Imports data from an URL into a data source
        /// </summary>
        /// <param name="dataSourceId">ID of the data source on which the data will be imported</param>
        /// <param name="url">Url to import the data from</param>
        /// <returns></returns>
        public async Task ImportDataFromUrlAsync(string url)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.DataSource_ImportDataFromUrlAsync, "Importing data from url into data source");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_ImportDataFromUrlAsync, $"Id: {Id} | Url: {url}");
            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.ImportDataFromUrl.ToSourceNamespace(), Id, url);
        }

        /// <summary>
        /// Clear the datasource
        /// </summary>
        /// <returns></returns>
        public async Task ClearAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.DataSource_ClearAsync, "Clearing data source");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_ClearAsync, $"Id: {Id}");

            _geometries = null;
            _features = null;
            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToSourceNamespace(), Id);
        }
    }
}
