namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Drawing;
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

        private Control[] _controls;
        private HtmlMarker[] _htmlMarkers;
        private DrawingToolbarOptions _drawingToolbarOptions;

        /// <summary>
        /// ID of the map
        /// </summary>
        public string Id { get; }

        public IReadOnlyCollection<Control> Controls
        {
            get => _controls;
            internal set => _controls = value?.ToArray();
        }

        public IReadOnlyCollection<HtmlMarker> HtmlMarkers
        {
            get => _htmlMarkers;
            internal set => _htmlMarkers = value?.ToArray();
        }

        public DrawingToolbarOptions DrawingToolbarOptions
        {
            get => _drawingToolbarOptions;
            internal set => _drawingToolbarOptions = value;
        }

        internal Map(string id,
            Func<IEnumerable<Control>, Task> addControlsCallback,
            Func<IEnumerable<HtmlMarker>, Task> addHtmlMarkersCallback,
            Func<IEnumerable<HtmlMarkerUpdate>, Task> updateHtmlMarkersCallback,
            Func<IEnumerable<HtmlMarker>, Task> removeHtmlMarkersCallback,
            Func<DrawingToolbarOptions, Task> addDrawingToolbarAsync,
            Func<DrawingToolbarUpdateOptions, Task> updateDrawingToolbarAsync)
        {
            Id = id;
            _addControlsCallback = addControlsCallback;
            _addHtmlMarkersCallback = addHtmlMarkersCallback;
            _updateHtmlMarkersCallback = updateHtmlMarkersCallback;
            _removeHtmlMarkersCallback = removeHtmlMarkersCallback;
            _addDrawingToolbarCallback = addDrawingToolbarAsync;
            _updateDrawingToolbarCallback = updateDrawingToolbarAsync;
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(params Control[] controls)
        {
            _controls = controls?.Where(control => control != null).ToArray();
            await _addControlsCallback.Invoke(_controls);
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(IEnumerable<Control> controls)
        {
            _controls = controls?.Where(control => control != null).ToArray();
            await _addControlsCallback.Invoke(_controls);
        }

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(params HtmlMarker[] markers)
        {
            _htmlMarkers = (_htmlMarkers ?? new HtmlMarker[0]).Concat(markers?.Where(marker => marker != null)).ToArray();
            await _addHtmlMarkersCallback.Invoke(_htmlMarkers);
        }

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(IEnumerable<HtmlMarker> markers)
        {
            _htmlMarkers = (_htmlMarkers ?? new HtmlMarker[0]).Concat(markers?.Where(marker => marker != null)).ToArray();
            await _addHtmlMarkersCallback.Invoke(_htmlMarkers);
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
            if(_htmlMarkers != null && markers != null)
            {
                _htmlMarkers = _htmlMarkers.Where(marker => markers.Any(m => m != null && m.Id != marker.Id)).ToArray();
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
            if (_htmlMarkers != null && markers != null)
            {
                _htmlMarkers = _htmlMarkers.Where(marker => markers.Any(m => m != null && m.Id != marker.Id)).ToArray();
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
            _drawingToolbarOptions = drawingToolbarOptions;
            await _addDrawingToolbarCallback.Invoke(drawingToolbarOptions);
        }

        /// <summary>
        /// Update the drawing toolbar with the given options
        /// </summary>
        /// <param name="drawingToolbarUpdateOptions">Options to update the drawing toolbar</param>
        /// <returns></returns>
        public async Task UpdateDrawingToolbarAsync(DrawingToolbarUpdateOptions drawingToolbarUpdateOptions)
        {
            _drawingToolbarOptions.Buttons = drawingToolbarUpdateOptions.Buttons;
            _drawingToolbarOptions.ContainerId = drawingToolbarUpdateOptions.ContainerId;
            _drawingToolbarOptions.NumColumns = drawingToolbarUpdateOptions.NumColumns;
            _drawingToolbarOptions.Position = drawingToolbarUpdateOptions.Position;
            _drawingToolbarOptions.Style = drawingToolbarUpdateOptions.Style;
            _drawingToolbarOptions.Visible = drawingToolbarUpdateOptions.Visible;
            await _updateDrawingToolbarCallback.Invoke(drawingToolbarUpdateOptions);
        }
    }
}
