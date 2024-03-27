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

    public delegate void DataSourceEvent();
    public delegate void DataSourceDataEvent(IEnumerable<Shape<Geometry>> shapes);

    public sealed class DataSource : BaseSource<DataSourceOptions>
    {
        public event DataSourceEvent OnDataSourceUpdated;
        public event DataSourceDataEvent OnDataAdded;
        public event DataSourceDataEvent OnDataRemoved;
        public event DataSourceEvent OnSourceAdded;
        public event DataSourceEvent OnSourceRemoved;

        public DataSourceEventActivationFlags EventActivationFlags { get; set; }

        public DataSource() : this(Guid.NewGuid().ToString()) { }

        public DataSource(string id) : base(id, SourceType.DataSource) { }

        /// <summary>
        /// Add shapes to the data source
        /// </summary>
        /// <param name="shapes">Shapes to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(IEnumerable<Shape> shapes)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_AddAsync, "Adding shapes to data source");

            await AddShapesAsync(shapes);
        }

        /// <summary>
        /// Add shapes to the data source
        /// </summary>
        /// <param name="shapes">Shapes to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(params Shape[] shapes) => await AddAsync(shapes as IEnumerable<Shape>);

        /// <summary>
        /// Add features to the data source
        /// </summary>
        /// <param name="features">Features to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(IEnumerable<Feature> features)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_AddAsync, "Adding features to data source");

            await AddFeaturesAsync(features);
        }

        /// <summary>
        /// Add features to the data source
        /// </summary>
        /// <param name="features">Features to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(params Feature[] features) => await AddAsync(features as IEnumerable<Feature>);

        /// <summary>
        /// Returns all shapes that are in the DataSource.
        /// </summary>
        /// <returns>Shapes existing in the datasource</returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask<IEnumerable<Shape<Geometry>>> GetShapesAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_GetOptionsAsync, "DataSource - GetShapesAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Source_GetOptionsAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            return await JSRuntime.InvokeAsync<IEnumerable<Shape<Geometry>>>(Constants.JsConstants.Methods.Datasource.GetShapes.ToDatasourceNamespace(), Id);
        }

        /// <summary>
        /// Retrieves shapes that are within the cluster.
        /// </summary>
        /// <param name="clusterId">The ID of the cluster</param>
        /// <param name="limit">The maximum number of features to return</param>
        /// <param name="offset">The number of shapes to skip. Allows you to page through the shapes in the cluster</param>
        /// <returns></returns>
        public async ValueTask<IEnumerable<Feature<Geometry>>> GetClusterLeavesAsync(int clusterId, int limit, int offset)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.DataSource_GetClusterLeavesAsync, "DataSource - GetClusterLeavesAsync");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_GetClusterLeavesAsync, $"Id: {Id}");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_GetClusterLeavesAsync, $"ClusterId: {clusterId}");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_GetClusterLeavesAsync, $"Limit: {limit}");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.DataSource_GetClusterLeavesAsync, $"Offset: {offset}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            return await JSRuntime.InvokeAsync<IEnumerable<Feature<Geometry>>>(Constants.JsConstants.Methods.Datasource.GetClusterLeaves.ToDatasourceNamespace(), Id, clusterId, limit, offset);
        }

        internal void DispatchEvent(DataSourceEventArgs eventArgs)
        {
            switch (eventArgs.Type)
            {
                case "dataadded":
                    OnDataAdded?.Invoke(eventArgs.Shapes);
                    break;

                case "dataremoved":
                    OnDataRemoved?.Invoke(eventArgs.Shapes);
                    break;

                case "datasourceupdated":
                    OnDataSourceUpdated?.Invoke();
                    break;

                case "sourceadded":
                    OnSourceAdded?.Invoke();
                    break;

                case "sourceremoved":
                    OnSourceRemoved?.Invoke();
                    break;
            }
        }
    }
}
