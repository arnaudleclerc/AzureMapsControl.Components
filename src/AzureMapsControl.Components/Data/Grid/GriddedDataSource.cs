namespace AzureMapsControl.Components.Data.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Logger;

    public sealed class GriddedDataSource : BaseSource<GriddedDataSourceOptions>
    {
        public GriddedDataSource() : this(Guid.NewGuid().ToString()) { }

        public GriddedDataSource(string id) : base(id, SourceType.GriddedDataSource) { }

        /// <summary>
        /// Add shapes to the gridded data source
        /// </summary>
        /// <param name="shapes">Shapes to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(IEnumerable<Shape<Point>> shapes)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_AddAsync, "Adding shapes to gridded data source");

            await AddShapesAsync(shapes);
        }

        /// <summary>
        /// Add shapes to the grided data source
        /// </summary>
        /// <param name="shapes">Shapes to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(params Shape<Point>[] shapes) => await AddAsync(shapes as IEnumerable<Shape<Point>>);

        /// <summary>
        /// Add features to the gridded data source
        /// </summary>
        /// <param name="features">Features to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(IEnumerable<Feature<Point>> features)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Source_AddAsync, "Adding features to gridded data source");

            await AddFeaturesAsync(features);
        }

        /// <summary>
        /// Add features to the gridded data source
        /// </summary>
        /// <param name="features">Features to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask AddAsync(params Feature<Point>[] features) => await AddAsync(features as IEnumerable<Feature<Point>>);
    }
}
