namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Drawing;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Markers;
    using AzureMapsControl.Components.Popups;
    using AzureMapsControl.Components.Traffic;

    public delegate void MapEvent(MapEventArgs eventArgs);
    public delegate void MapMouseEvent(MapMouseEventArgs eventArgs);
    public delegate void MapStyleDataEvent(MapStyleDataEventArgs eventArgs);
    public delegate void MapDataEvent(MapDataEventArgs eventArgs);
    public delegate void MapErrorEvent(MapErrorEventArgs eventArgs);
    public delegate void MapLayerEvent(MapLayerEventArgs eventArgs);
    public delegate void MapTouchEvent(MapTouchEventArgs eventArgs);
    public delegate void MapMessageEvent(MapMessageEventArgs eventArgs);

    public delegate void DrawingToolbarModeEvent(DrawingToolbarModeEventArgs eventArgs);
    public delegate void DrawingToolbarEvent(DrawingToolbarEventArgs eventArgs);

    /// <summary>
    /// Representation of a map
    /// </summary>
    public sealed class Map
    {

        #region Fields

        private readonly Func<IEnumerable<Control>, Task> _addControlsCallback;
        private readonly Func<IEnumerable<HtmlMarker>, Task> _addHtmlMarkersCallback;
        private readonly Func<IEnumerable<HtmlMarkerUpdate>, Task> _updateHtmlMarkersCallback;
        private readonly Func<IEnumerable<HtmlMarker>, Task> _removeHtmlMarkersCallback;
        private readonly Func<DrawingToolbarOptions, Task> _addDrawingToolbarCallback;
        private readonly Func<DrawingToolbarUpdateOptions, Task> _updateDrawingToolbarCallback;
        private readonly Func<Task> _removeDrawingToolbarCallback;
        private readonly Func<Layer, string, Task> _addLayerCallback;
        private readonly Func<IEnumerable<string>, Task> _removeLayersCallback;
        private readonly Func<Source, Task> _addSourceCallback;
        private readonly Func<string, Task> _removeSourceCallback;
        private readonly Action<DataSource> _attachDataSourceEventsCallback;
        private readonly Func<Task> _clearMapCallback;
        private readonly Func<Task> _clearLayersCallback;
        private readonly Func<Task> _clearSourcesCallback;
        private readonly Func<Task> _clearHtmlMarkersCallback;
        private readonly Func<Popup, Task> _addPopupCallback;
        private readonly Func<string, Task> _removePopupCallback;
        private readonly Func<Task> _clearPopupsCallback;
        private readonly Func<CameraOptions, Task> _setCameraCallback;
        private readonly Func<StyleOptions, Task> _setStyleCallback;
        private readonly Func<UserInteractionOptions, Task> _setUserInteractionCallback;
        private readonly Func<TrafficOptions, Task> _setTrafficOptions;

        private List<Layer> _layers;
        private List<Source> _sources;
        private List<Popup> _popups;

        #endregion

        #region Properties

        /// <summary>
        /// ID of the map
        /// </summary>
        public string Id { get; }

        public IEnumerable<Control> Controls { get; internal set; }

        public IEnumerable<HtmlMarker> HtmlMarkers { get; internal set; }

        public DrawingToolbarOptions DrawingToolbarOptions { get; internal set; }

        public IEnumerable<Layer> Layers => _layers;

        public IEnumerable<Source> Sources => _sources;

        public IEnumerable<Popup> Popups => _popups;

        internal CameraOptions CameraOptions { get; set; }
        internal StyleOptions StyleOptions { get; set; }
        internal UserInteractionOptions UserInteractionOptions { get; set; }
        internal TrafficOptions TrafficOptions { get; set; }

        #endregion

        #region Events

        public event MapEvent OnBoxZoomEnd;
        public event MapEvent OnBoxZoomStart;
        public event MapMouseEvent OnClick;
        public event MapMouseEvent OnContextMenu;
        public event MapDataEvent OnData;
        public event MapMouseEvent OnDblClick;
        public event MapEvent OnDrag;
        public event MapEvent OnDragEnd;
        public event MapEvent OnDragStart;
        public event MapErrorEvent OnError;
        public event MapEvent OnIdle;
        public event MapLayerEvent OnLayerAdded;
        public event MapLayerEvent OnLayerRemoved;
        public event MapEvent OnLoad;
        public event MapMouseEvent OnMouseDown;
        public event MapMouseEvent OnMouseMove;
        public event MapMouseEvent OnMouseOut;
        public event MapMouseEvent OnMouseOver;
        public event MapMouseEvent OnMouseUp;
        public event MapEvent OnMove;
        public event MapEvent OnMoveEnd;
        public event MapEvent OnMoveStart;
        public event MapEvent OnPitch;
        public event MapEvent OnPitchEnd;
        public event MapEvent OnPitchStart;
        public event MapEvent OnReady;
        public event MapEvent OnRender;
        public event MapEvent OnResize;
        public event MapEvent OnRotate;
        public event MapEvent OnRotateEnd;
        public event MapEvent OnRotateStart;
        public event MapDataEvent OnSourceAdded;
        public event MapDataEvent OnSourceData;
        public event MapDataEvent OnSourceRemoved;
        public event MapStyleDataEvent OnStyleData;
        public event MapMessageEvent OnStyleImageMissing;
        public event MapEvent OnTokenAcquired;
        public event MapTouchEvent OnTouchCancel;
        public event MapTouchEvent OnTouchEnd;
        public event MapTouchEvent OnTouchMove;
        public event MapTouchEvent OnTouchStart;
        public event MapEvent OnWheel;
        public event MapEvent OnZoom;
        public event MapEvent OnZoomEnd;
        public event MapEvent OnZoomStart;

        public event DrawingToolbarModeEvent OnDrawingModeChanged;
        public event MapEvent OnDrawingStarted;
        public event DrawingToolbarEvent OnDrawingChanging;
        public event DrawingToolbarEvent OnDrawingChanged;
        public event DrawingToolbarEvent OnDrawingComplete;

        #endregion

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
            Func<Source, Task> addSourceCallback = null,
            Func<string, Task> removeSourceCallback = null,
            Action<DataSource> attachDataSourceEventsCallback = null,
            Func<Task> clearMapCallback = null,
            Func<Task> clearLayersCallback = null,
            Func<Task> clearSourcesCallback = null,
            Func<Task> clearHtmlMarkersCallback = null,
            Func<Popup, Task> addPopupCallback = null,
            Func<string, Task> removePopupCallback = null,
            Func<Task> clearPopupsCallback = null,
            Func<CameraOptions, Task> setCameraCallback = null,
            Func<StyleOptions, Task> setStyleCallback = null,
            Func<UserInteractionOptions, Task> setUserInteractionCallback = null,
            Func<TrafficOptions, Task> setTrafficOptions = null)
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
            _addSourceCallback = addSourceCallback;
            _removeSourceCallback = removeSourceCallback;
            _attachDataSourceEventsCallback = attachDataSourceEventsCallback;
            _clearMapCallback = clearMapCallback;
            _clearLayersCallback = clearLayersCallback;
            _clearSourcesCallback = clearSourcesCallback;
            _clearHtmlMarkersCallback = clearHtmlMarkersCallback;
            _addPopupCallback = addPopupCallback;
            _removePopupCallback = removePopupCallback;
            _clearPopupsCallback = clearPopupsCallback;
            _setCameraCallback = setCameraCallback;
            _setStyleCallback = setStyleCallback;
            _setUserInteractionCallback = setUserInteractionCallback;
            _setTrafficOptions = setTrafficOptions;
        }

        # region Controls

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

        #endregion

        #region Data Sources

        /// <summary>
        /// Add a data source to the map
        /// </summary>
        /// <param name="dataSource">Data source to add</param>
        /// <returns></returns>
        public async Task AddSourceAsync<TSource>(TSource source) where TSource : Data.Source
        {
            if (_sources == null)
            {
                _sources = new List<Data.Source>();
            }

            if (_sources.Any(ds => ds.Id == source.Id))
            {
                throw new SourceAlreadyExistingException(source.Id);
            }

            _sources.Add(source);
            await _addSourceCallback(source);

            if (source is DataSource dataSource)
            {
                _attachDataSourceEventsCallback.Invoke(dataSource);
            }
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
            var dataSource = _sources?.SingleOrDefault(ds => ds.Id == id);
            if (dataSource != null)
            {
                await _removeSourceCallback.Invoke(id);
                _sources.Remove(dataSource);
            }
        }

        /// <summary>
        /// Removes all sources from the map.
        /// </summary>
        /// <returns></returns>
        public async Task ClearDataSourcesAsync()
        {
            _sources = null;
            await _clearSourcesCallback.Invoke();
        }

        #endregion

        #region Drawing Toolbar

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

        internal void DispatchDrawingToolbarEvent(DrawingToolbarJsEventArgs eventArgs)
        {
            switch (eventArgs.Type)
            {
                case "drawingchanged":
                    OnDrawingChanged?.Invoke(new DrawingToolbarEventArgs(this, eventArgs));
                    break;

                case "drawingchanging":
                    OnDrawingChanging?.Invoke(new DrawingToolbarEventArgs(this, eventArgs));
                    break;

                case "drawingcomplete":
                    OnDrawingComplete?.Invoke(new DrawingToolbarEventArgs(this, eventArgs));
                    break;

                case "drawingmodechanged":
                    OnDrawingModeChanged?.Invoke(new DrawingToolbarModeEventArgs(this, eventArgs));
                    break;

                case "drawingstarted":
                    OnDrawingStarted?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
            }
        }

        #endregion

        #region HTML Markers

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
            HtmlMarkers = (HtmlMarkers ?? Array.Empty<HtmlMarker>()).Concat(markers);
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
        /// Removes all markers
        /// </summary>
        /// <returns></returns>
        public async Task ClearHtmlMarkersAsync()
        {
            HtmlMarkers = null;
            await _clearHtmlMarkersCallback.Invoke();
        }

        #endregion

        #region Layers

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
        /// Removes all user added layers from the map
        /// </summary>
        /// <returns></returns>
        public async Task ClearLayersAsync()
        {
            _layers = null;
            await _clearLayersCallback.Invoke();
        }

        #endregion

        #region Map

        /// <summary>
        /// Removes all user added sources, layers, markers, and popups from the map. User added images are preserved.
        /// </summary>
        /// <returns></returns>
        public async Task ClearMapAsync()
        {
            _sources = null;
            _layers = null;
            HtmlMarkers = null;
            _popups = null;
            await _clearMapCallback.Invoke();
        }

        /// <summary>
        /// Update the camera options of the map
        /// </summary>
        /// <param name="configure">Action setting the camera options</param>
        /// <returns></returns>
        public async Task SetCameraOptionsAsync(Action<CameraOptions> configure)
        {
            if (CameraOptions == null)
            {
                CameraOptions = new CameraOptions();
            }
            configure.Invoke(CameraOptions);
            await _setCameraCallback.Invoke(CameraOptions);
        }

        /// <summary>
        /// Update the style options of the map
        /// </summary>
        /// <param name="configure">Action setting the style options</param>
        /// <returns></returns>
        public async Task SetStyleOptionsAsync(Action<StyleOptions> configure)
        {
            if (StyleOptions == null)
            {
                StyleOptions = new StyleOptions();
            }
            configure.Invoke(StyleOptions);
            await _setStyleCallback.Invoke(StyleOptions);
        }

        /// <summary>
        /// Update the user interaction options of the map
        /// </summary>
        /// <param name="configure">Action setting the user interaction options</param>
        /// <returns></returns>
        public async Task SetUserInteractionAsync(Action<UserInteractionOptions> configure)
        {
            if (UserInteractionOptions == null)
            {
                UserInteractionOptions = new UserInteractionOptions();
            }
            configure.Invoke(UserInteractionOptions);
            await _setUserInteractionCallback.Invoke(UserInteractionOptions);
        }

        /// <summary>
        /// Update the traffic options on the map
        /// </summary>
        /// <param name="configure">Action setting the traffic options</param>
        /// <returns></returns>
        public async Task SetTrafficOptionsAsync(Action<TrafficOptions> configure)
        {
            if (TrafficOptions == null)
            {
                TrafficOptions = new TrafficOptions();
            }
            configure.Invoke(TrafficOptions);
            await _setTrafficOptions.Invoke(TrafficOptions);
        }

        internal void DispatchEvent(MapJsEventArgs eventArgs)
        {
            switch (eventArgs.Type)
            {
                case "boxzoomend":
                    OnBoxZoomEnd?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "boxzoomstart":
                    OnBoxZoomStart?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "click":
                    OnClick?.Invoke(new MapMouseEventArgs(this, eventArgs));
                    break;
                case "contextmenu":
                    OnContextMenu?.Invoke(new MapMouseEventArgs(this, eventArgs));
                    break;
                case "data":
                    OnData?.Invoke(new MapDataEventArgs(this, eventArgs));
                    break;
                case "dblclick":
                    OnDblClick?.Invoke(new MapMouseEventArgs(this, eventArgs));
                    break;
                case "drag":
                    OnDrag?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "dragend":
                    OnDragEnd?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "dragstart":
                    OnDragStart?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "error":
                    OnError?.Invoke(new MapErrorEventArgs(this, eventArgs));
                    break;
                case "idle":
                    OnIdle?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "layeradded":
                    OnLayerAdded?.Invoke(new MapLayerEventArgs(this, eventArgs));
                    break;
                case "layerremoved":
                    OnLayerRemoved?.Invoke(new MapLayerEventArgs(this, eventArgs));
                    break;
                case "load":
                    OnLoad?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "mousedown":
                    OnMouseDown?.Invoke(new MapMouseEventArgs(this, eventArgs));
                    break;
                case "mousemove":
                    OnMouseMove?.Invoke(new MapMouseEventArgs(this, eventArgs));
                    break;
                case "mouseout":
                    OnMouseOut?.Invoke(new MapMouseEventArgs(this, eventArgs));
                    break;
                case "mouseover":
                    OnMouseOver?.Invoke(new MapMouseEventArgs(this, eventArgs));
                    break;
                case "mouseup":
                    OnMouseUp?.Invoke(new MapMouseEventArgs(this, eventArgs));
                    break;
                case "move":
                    OnMove?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "moveend":
                    OnMoveEnd?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "movestart":
                    OnMoveStart?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "pitch":
                    OnPitch?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "pitchend":
                    OnPitchEnd?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "pitchstart":
                    OnPitchStart?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "ready":
                    OnReady?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "render":
                    OnRender?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "resize":
                    OnResize?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "rotate":
                    OnRotate?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "rotateend":
                    OnRotateEnd?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "rotatestart":
                    OnRotateStart?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "sourceadded":
                    OnSourceAdded?.Invoke(new MapDataEventArgs(this, eventArgs));
                    break;
                case "sourcedata":
                    OnSourceData?.Invoke(new MapDataEventArgs(this, eventArgs));
                    break;
                case "sourceremoved":
                    OnSourceRemoved?.Invoke(new MapDataEventArgs(this, eventArgs));
                    break;
                case "styledata":
                    OnStyleData?.Invoke(new MapStyleDataEventArgs(this, eventArgs));
                    break;
                case "styleimagemissing":
                    OnStyleImageMissing?.Invoke(new MapMessageEventArgs(this, eventArgs));
                    break;
                case "tokenacquired":
                    OnTokenAcquired?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "touchcancel":
                    OnTouchCancel?.Invoke(new MapTouchEventArgs(this, eventArgs));
                    break;
                case "touchend":
                    OnTouchEnd?.Invoke(new MapTouchEventArgs(this, eventArgs));
                    break;
                case "touchmove":
                    OnTouchMove?.Invoke(new MapTouchEventArgs(this, eventArgs));
                    break;
                case "touchstart":
                    OnTouchStart?.Invoke(new MapTouchEventArgs(this, eventArgs));
                    break;
                case "wheel":
                    OnWheel?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "zoom":
                    OnZoom?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "zoomend":
                    OnZoomEnd?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
                case "zoomstart":
                    OnZoomStart?.Invoke(new MapEventArgs(this, eventArgs.Type));
                    break;
            }
        }

        #endregion

        #region Popups

        /// <summary>
        /// Add a new popup to the map
        /// </summary>
        /// <param name="poup">Popup to add to the map</param>
        /// <returns></returns>
        public async Task AddPopupAsync(Popup popup)
        {
            if (_popups == null)
            {
                _popups = new List<Popup>();
            }

            if (_popups.Any(p => p.Id == popup.Id))
            {
                throw new PopupAlreadyExistingException(popup.Id);
            }

            await _addPopupCallback.Invoke(popup);
            _popups.Add(popup);
        }

        /// <summary>
        /// Remove a popup from the map
        /// </summary>
        /// <param name="id">ID of the popup to remove</param>
        /// <returns></returns>
        public async Task RemovePopupAsync(string id)
        {
            var popup = _popups?.SingleOrDefault(p => p.Id == id);
            if (popup != null)
            {
                await _removePopupCallback.Invoke(id);
                RemovePopup(id);
            }
        }

        /// <summary>
        /// Remove a popup from the map
        /// </summary>
        /// <param name="popup">Popup to remove</param>
        /// <returns></returns>
        public async Task RemovePopupAsync(Popup popup) => await RemovePopupAsync(popup.Id);

        /// <summary>
        /// Remove all the popups from the map
        /// </summary>
        /// <returns></returns>
        public async Task ClearPopupsAsync()
        {
            await _clearPopupsCallback.Invoke();
            _popups = null;
        }

        internal void RemovePopup(string id)
        {
            if (_popups != null)
            {
                var popupIndex = _popups.FindIndex(popup => popup.Id == id);
                _popups.RemoveAt(popupIndex);
            }
        }

        #endregion
    }
}
