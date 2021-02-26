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
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Markers;
    using AzureMapsControl.Components.Popups;
    using AzureMapsControl.Components.Runtime;
    using AzureMapsControl.Components.Traffic;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

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
        private readonly IMapJsRuntime _jsRuntime;
        private readonly ILogger _logger;
        private readonly DrawingToolbarEventInvokeHelper _drawingToolbarEventInvokeHelper;
        private readonly HtmlMarkerInvokeHelper _htmlMarkerInvokeHelper;
        private readonly LayerEventInvokeHelper _layerEventInvokeHelper;
        private readonly PopupInvokeHelper _popupInvokeHelper;

        #region Fields

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
            IMapJsRuntime jsRuntime = null,
            ILogger logger = null,
            DrawingToolbarEventInvokeHelper drawingToolbarEventInvokeHelper = null,
            HtmlMarkerInvokeHelper htmlMarkerInvokeHelper = null,
            LayerEventInvokeHelper layerEventInvokeHelper = null,
            PopupInvokeHelper popupInvokeHelper = null)
        {
            Id = id;
            _jsRuntime = jsRuntime;
            _logger = logger;
            _drawingToolbarEventInvokeHelper = drawingToolbarEventInvokeHelper;
            _htmlMarkerInvokeHelper = htmlMarkerInvokeHelper;
            _layerEventInvokeHelper = layerEventInvokeHelper;
            _popupInvokeHelper = popupInvokeHelper;
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
            if (controls != null && controls.Any())
            {
                _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddControlsAsync, "Map - AddControlsAsync");
                Controls = controls;
                var overviewMapControl = controls.OfType<OverviewMapControl>().FirstOrDefault();
                if (overviewMapControl is not null)
                {
                    overviewMapControl.Logger = _logger;
                    overviewMapControl.JsRuntime = _jsRuntime;
                }
                _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddControlsAsync, $"Adding controls", Controls);
                _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddControlsAsync, $"{Controls.Count()} controls will be added: {string.Join('|', Controls.Select(co => co.Type))}");
                //Ordering the controls is necessary if the controls contain an OverviewMapControl. This one needs to be added last, otherwise the controls added after it will be added to the overlay.
                //Following : https://github.com/Azure-Samples/azure-maps-overview-map/issues/1
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddControls.ToCoreNamespace(), Controls?.OrderBy(control => control.Order));
            }
        }

        #endregion

        #region Data Sources

        /// <summary>
        /// Add a data source to the map
        /// </summary>
        /// <param name="dataSource">Data source to add</param>
        /// <returns></returns>
        public async Task AddSourceAsync<TSource>(TSource source) where TSource : Source
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddSourceAsync, "Adding source");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddSourceAsync, $"Id: {source.Id} | Type: {source.SourceType.ToString()}");

            if (_sources == null)
            {
                _sources = new List<Source>();
            }

            if (_sources.Any(ds => ds.Id == source.Id))
            {
                throw new SourceAlreadyExistingException(source.Id);
            }

            _sources.Add(source);

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), source.Id, source.GetSourceOptions()?.GenerateJsOptions(), source.SourceType.ToString());

            if (source is DataSource dataSource)
            {
                dataSource.Logger = _logger;
                dataSource.JSRuntime = _jsRuntime;
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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_RemoveSourceAsync, "Removing data source");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_RemoveSourceAsync, $"Id: {id}");
            var dataSource = _sources?.SingleOrDefault(ds => ds.Id == id);
            if (dataSource != null)
            {
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveSource.ToCoreNamespace(), id);
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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_ClearSourcesAsync, $"Clearing sources");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearSources.ToCoreNamespace());
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
            if (drawingToolbarOptions != null)
            {
                _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddDrawingToolbarAsync, "Adding drawing toolbar");
                DrawingToolbarOptions = drawingToolbarOptions;
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Drawing.AddDrawingToolbar.ToDrawingNamespace(),
                new DrawingToolbarCreationOptions {
                    Buttons = drawingToolbarOptions.Buttons?.Select(button => button.ToString()).ToArray(),
                    ContainerId = drawingToolbarOptions.ContainerId,
                    DragHandleStyle = drawingToolbarOptions.DragHandleStyle,
                    FreehandInterval = drawingToolbarOptions.FreehandInterval,
                    InteractionType = drawingToolbarOptions.InteractionType.ToString(),
                    Mode = drawingToolbarOptions.Mode.ToString(),
                    NumColumns = drawingToolbarOptions.NumColumns,
                    Position = drawingToolbarOptions.Position.ToString(),
                    SecondaryDragHandleStyle = drawingToolbarOptions.SecondaryDragHandleStyle,
                    ShapeDraggingEnabled = drawingToolbarOptions.ShapesDraggingEnabled,
                    Style = drawingToolbarOptions.Style.ToString(),
                    Visible = drawingToolbarOptions.Visible,
                    Events = drawingToolbarOptions.Events?.EnabledEvents
                },
                DotNetObjectReference.Create(_drawingToolbarEventInvokeHelper));
            }
        }

        /// <summary>
        /// Update the drawing toolbar with the given options
        /// </summary>
        /// <param name="drawingToolbarUpdateOptions">Options to update the drawing toolbar</param>
        /// <returns></returns>
        public async Task UpdateDrawingToolbarAsync(DrawingToolbarUpdateOptions drawingToolbarUpdateOptions)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_UpdateDrawingToolbarAsync, "Updating drawing toolbar");
            DrawingToolbarOptions.Buttons = drawingToolbarUpdateOptions.Buttons;
            DrawingToolbarOptions.ContainerId = drawingToolbarUpdateOptions.ContainerId;
            DrawingToolbarOptions.NumColumns = drawingToolbarUpdateOptions.NumColumns;
            DrawingToolbarOptions.Position = drawingToolbarUpdateOptions.Position;
            DrawingToolbarOptions.Style = drawingToolbarUpdateOptions.Style;
            DrawingToolbarOptions.Visible = drawingToolbarUpdateOptions.Visible;
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Drawing.UpdateDrawingToolbar.ToDrawingNamespace(),
                new DrawingToolbarCreationOptions {
                    Buttons = DrawingToolbarOptions.Buttons?.Select(button => button.ToString()).ToArray(),
                    ContainerId = DrawingToolbarOptions.ContainerId,
                    NumColumns = DrawingToolbarOptions.NumColumns,
                    Position = DrawingToolbarOptions.Position.ToString(),
                    Style = DrawingToolbarOptions.Style.ToString(),
                    Visible = DrawingToolbarOptions.Visible
                });
        }

        /// <summary>
        /// Removes the drawing toolbar from the map
        /// </summary>
        /// <returns></returns>
        public async Task RemoveDrawingToolbarAsync()
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_RemoveDrawingToolbarAsync, "Removing drawing toolbar");
            if (DrawingToolbarOptions != null)
            {
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Drawing.RemoveDrawingToolbar.ToDrawingNamespace());
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
            if (markers != null)
            {
                _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddHtmlMarkersAsync, "Adding html markers");
                HtmlMarkers = (HtmlMarkers ?? Array.Empty<HtmlMarker>()).Concat(markers);
                _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddHtmlMarkersAsync, $"{markers.Count()} new html markers will be added");
                var parameters = GetHtmlMarkersCreationParameters(markers);
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddHtmlMarkers.ToCoreNamespace(),
                parameters.MarkerOptions,
                parameters.InvokeHelper);
            }
        }

        internal (IEnumerable<HtmlMarkerCreationOptions> MarkerOptions, DotNetObjectReference<HtmlMarkerInvokeHelper> InvokeHelper) GetHtmlMarkersCreationParameters(IEnumerable<HtmlMarker> markers)
        {
            return (markers.Select(marker => new HtmlMarkerCreationOptions {
                Id = marker.Id,
                Events = marker.EventActivationFlags?.EnabledEvents,
                Options = marker.Options
            }), DotNetObjectReference.Create(_htmlMarkerInvokeHelper));
        }

        /// <summary>
        /// Update HtmlMarkers on the map
        /// </summary>
        /// <param name="updates">HtmlMarkers to update</param>
        /// <returns></returns>
        public async Task UpdateHtmlMarkersAsync(params HtmlMarkerUpdate[] updates) => await UpdateHtmlMarkersAsync(updates as IEnumerable<HtmlMarkerUpdate>);

        /// <summary>
        /// Update HtmlMarkers on the map
        /// </summary>
        /// <param name="updates">HtmlMarkers to update</param>
        /// <returns></returns>
        public async Task UpdateHtmlMarkersAsync(IEnumerable<HtmlMarkerUpdate> updates)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_UpdateHtmlMarkersAsync, "Updating html markers");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_UpdateHtmlMarkersAsync, $"{updates.Count()} html markers will be updated");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_UpdateHtmlMarkersAsync, $"Ids: {string.Join('|', updates.Select(h => h.Marker.Id))}");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.UpdateHtmlMarkers.ToCoreNamespace(),
            updates.Select(update => new HtmlMarkerCreationOptions {
                Id = update.Marker.Id,
                Options = update.Options
            }));
        }

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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_RemoveHtmlMarkersAsync, "Removing html markers");
            if (HtmlMarkers != null && markers != null)
            {
                HtmlMarkers = HtmlMarkers.Where(marker => markers.Any(m => m != null && m.Id != marker.Id));
                _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_RemoveHtmlMarkersAsync, $"{markers.Count()} html markers will be removed");
                var ids = markers.Select(marker => marker.Id);
                _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_RemoveHtmlMarkersAsync, $"Ids: {string.Join('|', ids)}");
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveHtmlMarkers.ToCoreNamespace(), ids);
            }
        }

        /// <summary>
        /// Removes all markers
        /// </summary>
        /// <returns></returns>
        public async Task ClearHtmlMarkersAsync()
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_ClearHtmlMarkersAsync, "Clearing html markers");
            HtmlMarkers = null;
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearHtmlMarkers.ToCoreNamespace());
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

            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddLayerAsync, "Adding layer");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddLayerAsync, $"Id: {layer.Id} | Type: {layer.Type} | Events: {string.Join('|', layer.EventActivationFlags.EnabledEvents)} | Before: {before}");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(),
                layer.Id,
                before,
                layer.Type.ToString(),
                layer.GetLayerOptions()?.GenerateJsOptions(),
                layer.EventActivationFlags.EnabledEvents,
                DotNetObjectReference.Create(_layerEventInvokeHelper));
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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_RemoveLayersAsync, "Removing layers");
            var layers = _layers?.Where(l => layerIds.Contains(l.Id));
            if (layers.Any())
            {
                var layerIdsToRemove = layers.Select(l => l.Id);
                _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_RemoveLayersAsync, $"Ids: {string.Join('|', layerIds)}");
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveLayers.ToCoreNamespace(), layerIdsToRemove);
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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_ClearLayersAsync, "Clearing layers");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearLayers.ToCoreNamespace());
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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.AzureMap_ClearMapAsync, "Clearing map");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearMap.ToCoreNamespace());
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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_SetCameraOptionsAsync, "Setting camera options");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCameraOptions.ToCoreNamespace(), CameraOptions);
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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_SetStyleOptionsAsync, "Setting style options");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetStyleOptions.ToCoreNamespace(), StyleOptions);
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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_SetUserInteractionAsync, "Setting user interaction options");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetUserInteraction.ToCoreNamespace(), UserInteractionOptions);
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
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_SetTrafficAsync, "Setting traffic options");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetTraffic.ToCoreNamespace(), TrafficOptions);
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

            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddPopupAsync, "Adding popup");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddPopupAsync, $"Id: {popup.Id} | Events: {string.Join('|', popup.EventActivationFlags.EnabledEvents)}");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopup.ToCoreNamespace(), popup.Id, popup.Options, popup.EventActivationFlags.EnabledEvents, DotNetObjectReference.Create(_popupInvokeHelper));

            popup.JSRuntime = _jsRuntime;
            popup.OnRemoved += () => RemovePopup(popup.Id);

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
                await popup.RemoveAsync();
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
            _popups = null;
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_ClearPopupsAsync, "Clearing popups");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearPopups.ToCoreNamespace());
        }

        private void RemovePopup(string id)
        {
            if (_popups != null)
            {
                var popupIndex = _popups.FindIndex(popup => popup.Id == id);
                _popups.RemoveAt(popupIndex);
            }
        }

        #endregion

        /// <summary>
        /// Creates and adds an image to the maps image sprite. Provide the name of the built-in template to use, and a color to apply.
        /// Optionally, specifiy a secondary color if the template supports one. A scale can also be specified.
        /// This will allow the SVG to be scaled before it is converted into an image and thus look much better when scaled up.
        /// </summary>
        /// <param name="id">The image's id. If the specified id matches the id of a previously added image the new image will be ignored.</param>
        /// <param name="templateName">The name of the template to use.</param>
        /// <param name="color">The primary color value</param>
        /// <param name="secondaryColor">A secondary color value</param>
        /// <param name="scale">Specifies how much to scale the template. For best results, scale the icon to the maximum size you want to display it on the map, then use the symbol layers icon size option to scale down if needed. This will reduce blurriness due to scaling.</param>
        /// <returns></returns>
        public async Task CreateImageFromTemplateAsync(string id, string templateName, string color = null, string secondaryColor = null, decimal? scale = null)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_CreateImageFromTemplate, "Creating image from template");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_CreateImageFromTemplate, "Id", id);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_CreateImageFromTemplate, "Template name", templateName);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_CreateImageFromTemplate, "Color", color);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_CreateImageFromTemplate, "Secondary color", secondaryColor);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_CreateImageFromTemplate, "Scale", scale);

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.CreateImageFromTemplate.ToCoreNamespace(), new MapImageTemplate {
                Id = id,
                TemplateName = templateName,
                Color = color,
                SecondaryColor = secondaryColor,
                Scale = scale
            });
        }
    }
}
