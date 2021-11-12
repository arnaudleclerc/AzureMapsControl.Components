namespace AzureMapsControl.Components.Data.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Logger;

    /// <summary>
    /// A data source for aggregating point features into cells of a grid system. 
    /// </summary>
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

        /// <summary>
        /// Gets all points that are within the specified grid cell.
        /// </summary>
        /// <param name="cellId">The grid cell id.</param>
        /// <returns>Points that are within the specified grid cell</returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask<IEnumerable<Feature<Point>>> GetCellChildrenAsync(string cellId)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GriddedDataSource_GetCellChildren, "Get cell children of gridded data source");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GriddedDataSource_GetCellChildren, $"ID: { Id }");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GriddedDataSource_GetCellChildren, $"CellId: {cellId}");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            return await JSRuntime.InvokeAsync<IEnumerable<Feature<Point>>>(Constants.JsConstants.Methods.GriddedDatasource.GetCellChildren.ToGriddedDatasourceNamespace(), Id, cellId);
        }

        /// <summary>
        /// Gets all grid cell polygons
        /// </summary>
        /// <returns>All grid cell of the griddeddatasource</returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask<IEnumerable<Feature<Polygon>>> GetGridCellsAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GriddedDataSource_GetGridCells, "Get grid cells of gridded data source");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GriddedDataSource_GetGridCells, $"ID: { Id }");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            return await JSRuntime.InvokeAsync<IEnumerable<Feature<Polygon>>>(Constants.JsConstants.Methods.GriddedDatasource.GetGridCells.ToGriddedDatasourceNamespace(), Id);
        }

        /// <summary>
        /// Gets all points
        /// </summary>
        /// <returns>All points of the griddeddatasource</returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask<IEnumerable<Feature<Point>>> GetPointsAsync()
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GriddedDataSource_GetPoints, "Get points of gridded data source");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GriddedDataSource_GetPoints, $"ID: { Id }");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            return await JSRuntime.InvokeAsync<IEnumerable<Feature<Point>>>(Constants.JsConstants.Methods.GriddedDatasource.GetPoints.ToGriddedDatasourceNamespace(), Id);
        }

        /// <summary>
        /// Overwrites all points in the data source with the points of the given feature collection
        /// </summary>
        /// <param name="featureCollection">The feature collection containing the new points to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask SetPointsAsync(JsonDocument featureCollection)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GriddedDataSource_SetPoints, "Set points of gridded data source with feature collection");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GriddedDataSource_SetPoints, $"ID: { Id }");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.GriddedDatasource.SetFeatureCollectionPoints.ToGriddedDatasourceNamespace(), Id, featureCollection);
        }

        /// <summary>
        /// Overwrites all points in the data source with the given features
        /// </summary>
        /// <param name="features"The new features to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask SetPointsAsync(IEnumerable<Feature<Point>> features)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GriddedDataSource_SetPoints, "Set points of gridded data source with feature points");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GriddedDataSource_SetPoints, $"ID: { Id }");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.GriddedDatasource.SetFeaturePoints.ToGriddedDatasourceNamespace(), Id, features);
        }

        /// <summary>
        /// Overwrites all points in the data source with the given points
        /// </summary>
        /// <param name="points"The new points to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask SetPointsAsync(IEnumerable<Point> points)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GriddedDataSource_SetPoints, "Set points of gridded data source with points");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GriddedDataSource_SetPoints, $"ID: { Id }");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.GriddedDatasource.SetPoints.ToGriddedDatasourceNamespace(), Id, points);
        }

        /// <summary>
        /// Overwrites all points in the data source with the given shapes
        /// </summary>
        /// <param name="shapes"The new shapes to add</param>
        /// <returns></returns>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <exception cref="Exceptions.ComponentDisposedException">The control has already been disposed</exception>
        public async ValueTask SetPointsAsync(IEnumerable<Shape<Point>> shapes)
        {
            Logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GriddedDataSource_SetPoints, "Set points of gridded data source with shapes");
            Logger?.LogAzureMapsControlDebug(AzureMapLogEvent.GriddedDataSource_SetPoints, $"ID: { Id }");

            EnsureJsRuntimeExists();
            EnsureNotDisposed();

            await JSRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.GriddedDatasource.SetShapePoints.ToGriddedDatasourceNamespace(), Id, shapes);
        }
    }
}
