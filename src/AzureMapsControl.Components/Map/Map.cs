namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Drawing;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Markers;

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
        private readonly Func<Layer, string, Task> _addLayerCallback;
        private readonly Func<IEnumerable<string>, Task> _removeLayersCallback;
        private readonly Func<DataSource, Task> _addDataSourceCallback;

        private readonly List<Layer> _layers;
        private readonly List<DataSource> _dataSources;

        /// <summary>
        /// ID of the map
        /// </summary>
        public string Id { get; }

        public IReadOnlyCollection<Control> Controls { get; internal set; }

        public IReadOnlyCollection<HtmlMarker> HtmlMarkers { get; internal set; }

        public DrawingToolbarOptions DrawingToolbarOptions { get; internal set; }

        public IReadOnlyCollection<Layer> Layers => _layers;

        public IReadOnlyCollection<DataSource> DataSources => _dataSources;

        internal Map() { }

        internal Map(string id,
            Func<IEnumerable<Control>, Task> addControlsCallback,
            Func<IEnumerable<HtmlMarker>, Task> addHtmlMarkersCallback,
            Func<IEnumerable<HtmlMarkerUpdate>, Task> updateHtmlMarkersCallback,
            Func<IEnumerable<HtmlMarker>, Task> removeHtmlMarkersCallback,
            Func<DrawingToolbarOptions, Task> addDrawingToolbarCallback,
            Func<DrawingToolbarUpdateOptions, Task> updateDrawingToolbarCallback,
            Func<Layer, string, Task> addLayerCallback,
            Func<IEnumerable<string>, Task> removeLayersCallback,
            Func<DataSource, Task> addDataSourceCallback)
        {
            Id = id;
            _addControlsCallback = addControlsCallback;
            _addHtmlMarkersCallback = addHtmlMarkersCallback;
            _updateHtmlMarkersCallback = updateHtmlMarkersCallback;
            _removeHtmlMarkersCallback = removeHtmlMarkersCallback;
            _addDrawingToolbarCallback = addDrawingToolbarCallback;
            _updateDrawingToolbarCallback = updateDrawingToolbarCallback;
            _addLayerCallback = addLayerCallback;
            _removeLayersCallback = removeLayersCallback;
            _addDataSourceCallback = addDataSourceCallback;
            _layers = new List<Layer>();
            _dataSources = new List<DataSource>();
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(params Control[] controls)
        {
            Controls = controls?.Where(control => control != null).ToArray();
            await _addControlsCallback.Invoke(Controls);
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(IEnumerable<Control> controls)
        {
            Controls = controls?.Where(control => control != null).ToArray();
            await _addControlsCallback.Invoke(Controls);
        }

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(params HtmlMarker[] markers)
        {
            HtmlMarkers = (HtmlMarkers ?? new HtmlMarker[0]).Concat(markers?.Where(marker => marker != null)).ToArray();
            await _addHtmlMarkersCallback.Invoke(HtmlMarkers);
        }

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(IEnumerable<HtmlMarker> markers)
        {
            HtmlMarkers = (HtmlMarkers ?? new HtmlMarker[0]).Concat(markers?.Where(marker => marker != null)).ToArray();
            await _addHtmlMarkersCallback.Invoke(HtmlMarkers);
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
        public async Task RemoveHtmlMarkersAsync(params HtmlMarker[] markers)
        {
            if (HtmlMarkers != null && markers != null)
            {
                HtmlMarkers = HtmlMarkers.Where(marker => markers.Any(m => m != null && m.Id != marker.Id)).ToArray();
                await _removeHtmlMarkersCallback.Invoke(markers);
            }
        }

        /// <summary>
        /// Remove HtmlMarkers from the map
        /// </summary>
        /// <param name="markers">HtmlMarkers to remove</param>
        /// <returns></returns>
        public async Task RemoveHtmlMarkersAsync(IEnumerable<HtmlMarker> markers)
        {
            if (HtmlMarkers != null && markers != null)
            {
                HtmlMarkers = HtmlMarkers.Where(marker => markers.Any(m => m != null && m.Id != marker.Id)).ToArray();
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
            if (_layers.Any(l => l.Id == layer.Id))
            {
                throw new ArgumentException("A layer with the given ID has already been added");
            }

            _layers.Add(layer);
            await _addLayerCallback.Invoke(layer, before);
        }

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layers">Layers to remove</param>
        /// <returns>Layers removed from the map</returns>
        public async Task<IEnumerable<Layer>> RemoveLayersAsync(params Layer[] layers) => await RemoveLayersAsync(layers?.Select(l => l.Id).ToArray());

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layerIds">ID of the layers to remove</param>
        /// <returns>Layers removed from the map</returns>
        public async Task<IEnumerable<Layer>> RemoveLayersAsync(params string[] layerIds)
        {
            var layers = _layers.Where(l => layerIds.Contains(l.Id));
            if (layers.Any())
            {
                await _removeLayersCallback.Invoke(layers.Select(l => l.Id));
                _layers.RemoveAll(l => layerIds.Contains(l.Id));
            }
            return layers;
        }

        /// <summary>
        /// Add a data source to the map
        /// </summary>
        /// <param name="dataSource">Data source to add</param>
        /// <returns></returns>
        public async Task AddDataSourceAsync(DataSource dataSource)
        {
            if (_dataSources.Any(ds => ds.Id == dataSource.Id))
            {
                throw new ArgumentException("A data source with the given ID has already been added");
            }

            _dataSources.Add(dataSource);
            await _addDataSourceCallback(dataSource);
        }

    }
}
