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
