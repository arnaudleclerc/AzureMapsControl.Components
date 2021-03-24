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
        /// Remove geometries from the data source
        /// </summary>
        /// <param name="geometryIds">IDs of the geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<string> geometryIds)
        {
            if (_geometries != null)
            {
                var ids = _geometries.Where(geometry => geometryIds.Contains(geometry.Id)).Select(geometry => geometry.Id);
                if (ids.Any())
                {
                    Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.DataSource_RemoveAsync, "Removing geometries from data source");
                    Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_RemoveAsync, $"Id: {Id} | GeometryIds: {string.Join('|', geometryIds)}");
                    await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), Id, geometryIds);
                    _geometries.RemoveAll(geometry => geometryIds.Contains(geometry.Id));
                }
            }
        }

        /// <summary>
        /// Remove geometries from the data source
        /// </summary>
        /// <param name="geometryIds">IDs of the geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(params string[] geometryIds) => await RemoveAsync(geometryIds as IEnumerable<string>);

        /// <summary>
        /// Remove geometries from the datasource
        /// </summary>
        /// <param name="geometries">Geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<Geometry> geometries) => await RemoveAsync(geometries.Select(g => g.Id));

        /// <summary>
        /// Remove geometries from the data source
        /// </summary>
        /// <param name="geometries">Geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(params Geometry[] geometries) => await RemoveAsync(geometries.Select(g => g.Id));

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
            _geometries = null;
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.DataSource_ClearAsync, "Clearing data source");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_ClearAsync, $"Id: {Id}");
            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToSourceNamespace(), Id);
        }
    }
}
