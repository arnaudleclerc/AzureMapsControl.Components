namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Drawing;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Markers;
    using AzureMapsControl.Components.Popups;

    /// <summary>
    /// Representation of a map
    /// </summary>
    public sealed class Map
    {
        private readonly Func<IEnumerable<Control>, Task> _addControlsCallback;
        private readonly Func<IEnumerable<HtmlMarker>, Task> _addHtmlMarkersCallback;
        private readonly Func<IEnumerable<HtmlMarkerUpdate>, Task> _updateHtmlMarkersCallback;
        private readonly Func<IEnumerable<HtmlMarker>, Task> _removeHtmlMarkersCallback;
        private readonly Func<DrawingToolbarOptions, Task> _addDrawingToolbarCallback;
        private readonly Func<DrawingToolbarUpdateOptions, Task> _updateDrawingToolbarCallback;
        private readonly Func<Task> _removeDrawingToolbarCallback;
        private readonly Func<Layer, string, Task> _addLayerCallback;
        private readonly Func<IEnumerable<string>, Task> _removeLayersCallback;
        private readonly Func<DataSource, Task> _addDataSourceCallback;
        private readonly Func<string, Task> _removeDataSourceCallback;
        private readonly Func<Task> _clearMapCallback;
        private readonly Func<Task> _clearLayersCallback;
        private readonly Func<Task> _clearDataSourcesCallback;
        private readonly Func<Task> _clearHtmlMarkersCallback;
        private readonly Func<Popup, Task> _addPopupCallback;

        private List<Layer> _layers;
        private List<DataSource> _dataSources;
        private List<Popup> _popups;

        /// <summary>
        /// ID of the map
        /// </summary>
        public string Id { get; }

        public IEnumerable<Control> Controls { get; internal set; }

        public IEnumerable<HtmlMarker> HtmlMarkers { get; internal set; }

        public DrawingToolbarOptions DrawingToolbarOptions { get; internal set; }

        public IEnumerable<Layer> Layers => _layers;

        public IEnumerable<DataSource> DataSources => _dataSources;

        public IEnumerable<Popup> Popups => _popups;

        internal Map(string id,
            Func<IEnumerable<Control>, Task> addControlsCallback = null,
            Func<IEnumerable<HtmlMarker>, Task> addHtmlMarkersCallback = null,
            Func<IEnumerable<HtmlMarkerUpdate>, Task> updateHtmlMarkersCallback = null,
            Func<IEnumerable<HtmlMarker>, Task> removeHtmlMarkersCallback = null,
            Func<DrawingToolbarOptions, Task> addDrawingToolbarCallback = null,
            Func<DrawingToolbarUpdateOptions, Task> updateDrawingToolbarCallback = null,
            Func<Task> removeDrawingToolbarCallback = null,
            Func<Layer, string, Task> addLayerCallback = null,
            Func<IEnumerable<string>, Task> removeLayersCallback = null,
            Func<DataSource, Task> addDataSourceCallback = null,
            Func<string, Task> removeDataSourceCallback = null,
            Func<Task> clearMapCallback = null,
            Func<Task> clearLayersCallback = null,
            Func<Task> clearDataSourcesCallback = null,
            Func<Task> clearHtmlMarkersCallback = null,
            Func<Popup, Task> addPopupCallback = null)
        {
            Id = id;
            _addControlsCallback = addControlsCallback;
            _addHtmlMarkersCallback = addHtmlMarkersCallback;
            _updateHtmlMarkersCallback = updateHtmlMarkersCallback;
            _removeHtmlMarkersCallback = removeHtmlMarkersCallback;
            _addDrawingToolbarCallback = addDrawingToolbarCallback;
            _updateDrawingToolbarCallback = updateDrawingToolbarCallback;
            _removeDrawingToolbarCallback = removeDrawingToolbarCallback;
            _addLayerCallback = addLayerCallback;
            _removeLayersCallback = removeLayersCallback;
            _addDataSourceCallback = addDataSourceCallback;
            _removeDataSourceCallback = removeDataSourceCallback;
            _clearMapCallback = clearMapCallback;
            _clearLayersCallback = clearLayersCallback;
            _clearDataSourcesCallback = clearDataSourcesCallback;
            _clearHtmlMarkersCallback = clearHtmlMarkersCallback;
            _addPopupCallback = addPopupCallback;
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(params Control[] controls) => await AddControlsAsync(controls as IEnumerable<Control>);

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(IEnumerable<Control> controls)
        {
            Controls = controls;
            await _addControlsCallback.Invoke(Controls);
        }

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(params HtmlMarker[] markers) => await AddHtmlMarkersAsync(markers as IEnumerable<HtmlMarker>);

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(IEnumerable<HtmlMarker> markers)
        {
            HtmlMarkers = (HtmlMarkers ?? new HtmlMarker[0]).Concat(markers);
            await _addHtmlMarkersCallback.Invoke(markers);
        }

        /// <summary>
        /// Update HtmlMarkers on the map
        /// </summary>
        /// <param name="updates">HtmlMarkers to update</param>
        /// <returns></returns>
        public async Task UpdateHtmlMarkersAsync(params HtmlMarkerUpdate[] updates) => await _updateHtmlMarkersCallback.Invoke(updates);

        /// <summary>
        /// Update HtmlMarkers on the map
        /// </summary>
        /// <param name="updates">HtmlMarkers to update</param>
        /// <returns></returns>
        public async Task UpdateHtmlMarkersAsync(IEnumerable<HtmlMarkerUpdate> updates) => await _updateHtmlMarkersCallback.Invoke(updates);

        /// <summary>
        /// Remove HtmlMarkers from the map
        /// </summary>
        /// <param name="markers">HtmlMarkers to remove</param>
        /// <returns></returns>
        public async Task RemoveHtmlMarkersAsync(params HtmlMarker[] markers) => await RemoveHtmlMarkersAsync(markers as IEnumerable<HtmlMarker>);

        /// <summary>
        /// Remove HtmlMarkers from the map
        /// </summary>
        /// <param name="markers">HtmlMarkers to remove</param>
        /// <returns></returns>
        public async Task RemoveHtmlMarkersAsync(IEnumerable<HtmlMarker> markers)
        {
            if (HtmlMarkers != null && markers != null)
            {
                HtmlMarkers = HtmlMarkers.Where(marker => markers.Any(m => m != null && m.Id != marker.Id));
                await _removeHtmlMarkersCallback.Invoke(markers);
            }
        }

        /// <summary>
        /// Add a drawing toolbar to the map
        /// </summary>
        /// <param name="drawingToolbarOptions">Options of the toolbar to create</param>
        /// <returns></returns>
        public async Task AddDrawingToolbarAsync(DrawingToolbarOptions drawingToolbarOptions)
        {
            DrawingToolbarOptions = drawingToolbarOptions;
            await _addDrawingToolbarCallback.Invoke(drawingToolbarOptions);
        }

        /// <summary>
        /// Update the drawing toolbar with the given options
        /// </summary>
        /// <param name="drawingToolbarUpdateOptions">Options to update the drawing toolbar</param>
        /// <returns></returns>
        public async Task UpdateDrawingToolbarAsync(DrawingToolbarUpdateOptions drawingToolbarUpdateOptions)
        {
            DrawingToolbarOptions.Buttons = drawingToolbarUpdateOptions.Buttons;
            DrawingToolbarOptions.ContainerId = drawingToolbarUpdateOptions.ContainerId;
            DrawingToolbarOptions.NumColumns = drawingToolbarUpdateOptions.NumColumns;
            DrawingToolbarOptions.Position = drawingToolbarUpdateOptions.Position;
            DrawingToolbarOptions.Style = drawingToolbarUpdateOptions.Style;
            DrawingToolbarOptions.Visible = drawingToolbarUpdateOptions.Visible;
            await _updateDrawingToolbarCallback.Invoke(drawingToolbarUpdateOptions);
        }

        /// <summary>
        /// Removes the drawing toolbar from the map
        /// </summary>
        /// <returns></returns>
        public async Task RemoveDrawingToolbarAsync()
        {
            if (DrawingToolbarOptions != null)
            {
                await _removeDrawingToolbarCallback.Invoke();
                DrawingToolbarOptions = null;
            }
        }

        /// <summary>
        /// Add a layer to the map
        /// </summary>
        /// <typeparam name="T">Type of layer to add</typeparam>
        /// <param name="layer">Layer to add to the map</param>
        /// <returns></returns>
        public async Task AddLayerAsync<T>(T layer) where T : Layer => await AddLayerAsync(layer, null);

        /// <summary>
        /// Add a layer to the map
        /// </summary>
        /// <typeparam name="T">Type of layer to add</typeparam>
        /// <param name="layer">Layer to add</param>
        /// <param name="before">ID of the layer before which the new layer will be added</param>
        /// <returns></returns>
        public async Task AddLayerAsync<T>(T layer, string before) where T : Layer
        {
            if (_layers == null)
            {
                _layers = new List<Layer>();
            }

            if (_layers.Any(l => l.Id == layer.Id))
            {
                throw new LayerAlreadyAddedException(layer.Id);
            }

            _layers.Add(layer);
            await _addLayerCallback.Invoke(layer, before);
        }

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layers">Layers to remove</param>
        /// <returns></returns>
        public async Task RemoveLayersAsync(IEnumerable<Layer> layers) => await RemoveLayersAsync(layers?.Select(l => l.Id));

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layers">Layers to remove</param>
        /// <returns></returns>
        public async Task RemoveLayersAsync(params Layer[] layers) => await RemoveLayersAsync(layers?.Select(l => l.Id));

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layerIds">ID of the layers to remove</param>
        /// <returns></returns>
        public async Task RemoveLayersAsync(params string[] layerIds) => await RemoveLayersAsync(layerIds as IEnumerable<string>);

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layerIds">ID of the layers to remove</param>
        /// <returns></returns>
        public async Task RemoveLayersAsync(IEnumerable<string> layerIds)
        {
            var layers = _layers?.Where(l => layerIds.Contains(l.Id));
            if (layers.Any())
            {
                await _removeLayersCallback.Invoke(layers.Select(l => l.Id));
                _layers.RemoveAll(l => layerIds.Contains(l.Id));
            }
        }

        /// <summary>
        /// Add a data source to the map
        /// </summary>
        /// <param name="dataSource">Data source to add</param>
        /// <returns></returns>
        public async Task AddDataSourceAsync(DataSource dataSource)
        {
            if (_dataSources == null)
            {
                _dataSources = new List<DataSource>();
            }

            if (_dataSources.Any(ds => ds.Id == dataSource.Id))
            {
                throw new DataSourceAlreadyExistingException(dataSource.Id);
            }

            _dataSources.Add(dataSource);
            await _addDataSourceCallback(dataSource);
        }

        /// <summary>
        /// Removes a data source from the map
        /// </summary>
        /// <param name="dataSource">Data source to remove</param>
        /// <returns></returns>
        public async Task RemoveDataSourceAsync(DataSource dataSource) => await RemoveDataSourceAsync(dataSource.Id);

        /// <summary>
        /// Removes a data source from the map
        /// </summary>
        /// <param name="id">ID of the data source to remove</param>
        /// <returns></returns>
        public async Task RemoveDataSourceAsync(string id)
        {
            var dataSource = _dataSources?.SingleOrDefault(ds => ds.Id == id);
            if (dataSource != null)
            {
                await _removeDataSourceCallback.Invoke(id);
                _dataSources.Remove(dataSource);
            }
        }

        /// <summary>
        /// Removes all user added sources, layers, markers, and popups from the map. User added images are preserved.
        /// </summary>
        /// <returns></returns>
        public async Task ClearMapAsync()
        {
            _dataSources = null;
            _layers = null;
            HtmlMarkers = null;
            await _clearMapCallback.Invoke();
        }

        /// <summary>
        /// Removes all user added layers from the map
        /// </summary>
        /// <returns></returns>
        public async Task ClearLayersAsync()
        {
            _layers = null;
            await _clearLayersCallback.Invoke();
        }

        /// <summary>
        /// Removes all sources from the map.
        /// </summary>
        /// <returns></returns>
        public async Task ClearDataSourcesAsync()
        {
            _dataSources = null;
            await _clearDataSourcesCallback.Invoke();
        }

        /// <summary>
        /// Removes all markers
        /// </summary>
        /// <returns></returns>
        public async Task ClearHtmlMarkersAsync()
        {
            HtmlMarkers = null;
            await _clearHtmlMarkersCallback.Invoke();
        }

        /// <summary>
        /// Add a new popup to the map
        /// </summary>
        /// <param name="poup">Popup to add to the map</param>
        /// <returns></returns>
        public async Task AddPopupAsync(Popup popup)
        {
            if(_popups == null)
            {
                _popups = new List<Popup>();
            }

            if(_popups.Any(p => p.Id == popup.Id))
            {
                throw new PopupAlreadyExistingException(popup.Id);
            }

            await _addPopupCallback.Invoke(popup);
            _popups.Add(popup);
        }


    }
}
