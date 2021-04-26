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
    using AzureMapsControl.Components.Guards;
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
        private readonly DataSourceEventInvokeHelper _dataSourceEventInvokeHelper;

        #region Fields

        private List<Layer> _layers;
        private List<Source> _sources;
        private List<Popup> _popups;
        private List<Control> _controls;

        #endregion

        #region Properties

        /// <summary>
        /// ID of the map
        /// </summary>
        public string Id { get; }

        public IEnumerable<HtmlMarker> HtmlMarkers { get; internal set; }

        public DrawingToolbarOptions DrawingToolbarOptions { get; internal set; }

        public IEnumerable<Control> Controls => _controls;

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
            PopupInvokeHelper popupInvokeHelper = null,
            DataSourceEventInvokeHelper dataSourceEventInvokeHelper = null)
        {
            Id = id;
            _jsRuntime = jsRuntime;
            _logger = logger;
            _drawingToolbarEventInvokeHelper = drawingToolbarEventInvokeHelper;
            _htmlMarkerInvokeHelper = htmlMarkerInvokeHelper;
            _layerEventInvokeHelper = layerEventInvokeHelper;
            _popupInvokeHelper = popupInvokeHelper;
            _dataSourceEventInvokeHelper = dataSourceEventInvokeHelper;
        }

        # region Controls

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async ValueTask AddControlsAsync(params Control[] controls) => await AddControlsAsync(controls as IEnumerable<Control>);

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async ValueTask AddControlsAsync(IEnumerable<Control> controls)
        {
            if (controls == null || !controls.Any())
            {
                return;
            }

            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddControlsAsync, "Map - AddControlsAsync");

            if (_controls is null)
            {
                _controls = new List<Control>();
            }

            _controls.AddRange(controls);

            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddControlsAsync, $"Adding controls", Controls);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddControlsAsync, $"{Controls.Count()} controls will be added: {string.Join('|', Controls.Select(co => co.Type))}");
            //Ordering the controls is necessary if the controls contain an OverviewMapControl. This one needs to be added last, otherwise the controls added after it will be added to the overlay.
            //Following : https://github.com/Azure-Samples/azure-maps-overview-map/issues/1
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddControls.ToCoreNamespace(), Controls?.OrderBy(control => control.Order));

            var overviewMapControl = controls.OfType<OverviewMapControl>().FirstOrDefault();
            if (overviewMapControl is not null)
            {
                overviewMapControl.Logger = _logger;
                overviewMapControl.JsRuntime = _jsRuntime;
            }

            var geolocationControl = controls.OfType<GeolocationControl>().FirstOrDefault();
            if (geolocationControl is not null)
            {
                geolocationControl.Logger = _logger;
                geolocationControl.JsRuntime = _jsRuntime;

                geolocationControl.OnDisposed += () => _controls.Remove(geolocationControl);
                await geolocationControl.AddEventsAsync();
            }
        }

        #endregion

        #region Data Sources

        /// <summary>
        /// Add a data source to the map
        /// </summary>
        /// <param name="dataSource">Data source to add</param>
        /// <returns></returns>
        public async ValueTask AddSourceAsync<TSource>(TSource source) where TSource : Source
        {
            if (source == null)
            {
                return;
            }

            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddSourceAsync, "Adding source");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddSourceAsync, $"Id: {source.Id} | Type: {source.SourceType}");

            if (_sources == null)
            {
                _sources = new List<Source>();
            }

            if (_sources.Any(ds => ds.Id == source.Id))
            {
                throw new SourceAlreadyExistingException(source.Id);
            }

            _sources.Add(source);

            if (source is DataSource dataSource)
            {
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(),
                    source.Id,
                    source.GetSourceOptions(),
                    source.SourceType.ToString(),
                    dataSource.EventActivationFlags?.EnabledEvents,
                    DotNetObjectReference.Create(_dataSourceEventInvokeHelper));
                dataSource.Logger = _logger;
                dataSource.JSRuntime = _jsRuntime;
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(),
                    source.Id,
                    source.GetSourceOptions(),
                    source.SourceType.ToString());
            }
        }

        /// <summary>
        /// Removes a data source from the map
        /// </summary>
        /// <param name="dataSource">Data source to remove</param>
        /// <returns></returns>
        public async ValueTask RemoveDataSourceAsync(DataSource dataSource) => await RemoveDataSourceAsync(dataSource?.Id);

        /// <summary>
        /// Removes a data source from the map
        /// </summary>
        /// <param name="id">ID of the data source to remove</param>
        /// <returns></returns>
        public async ValueTask RemoveDataSourceAsync(string id)
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
        public async ValueTask ClearDataSourcesAsync()
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
        public async ValueTask AddDrawingToolbarAsync(DrawingToolbarOptions drawingToolbarOptions)
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
        public async ValueTask UpdateDrawingToolbarAsync(DrawingToolbarUpdateOptions drawingToolbarUpdateOptions)
        {
            if (drawingToolbarUpdateOptions != null)
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
        }

        /// <summary>
        /// Removes the drawing toolbar from the map
        /// </summary>
        /// <returns></returns>
        public async ValueTask RemoveDrawingToolbarAsync()
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
        public async ValueTask AddHtmlMarkersAsync(params HtmlMarker[] markers) => await AddHtmlMarkersAsync(markers as IEnumerable<HtmlMarker>);

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async ValueTask AddHtmlMarkersAsync(IEnumerable<HtmlMarker> markers)
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

                foreach (var marker in markers)
                {
                    marker.JSRuntime = _jsRuntime;
                    marker.PopupInvokeHelper = _popupInvokeHelper;
                    marker.Logger = _logger;

                    marker.OnPopupToggled += () => {
                        if (_popups == null)
                        {
                            _popups = new List<Popup>();
                        }

                        if (!_popups.Contains(marker.Options.Popup))
                        {
                            _popups.Add(marker.Options.Popup);
                        }
                    };

                    if (marker.Options?.Popup != null)
                    {
                        marker.Options.Popup.JSRuntime = _jsRuntime;
                        marker.Options.Popup.Logger = _logger;
                        marker.Options.Popup.OnRemoved += () => RemovePopup(marker.Options.Popup.Id);

                        if (marker.Options.Popup.Options.OpenOnAdd.GetValueOrDefault())
                        {
                            await marker.TogglePopupAsync();
                        }
                    }
                }
            }
        }

        internal (IEnumerable<HtmlMarkerCreationOptions> MarkerOptions, DotNetObjectReference<HtmlMarkerInvokeHelper> InvokeHelper) GetHtmlMarkersCreationParameters(IEnumerable<HtmlMarker> markers)
        {
            return (markers.Select(marker => new HtmlMarkerCreationOptions {
                Id = marker.Id,
                Events = marker.EventActivationFlags?.EnabledEvents,
                Options = marker.Options,
                PopupOptions = marker.Options?.Popup != null ? new HtmlMarkerPopupCreationOptions {
                    Events = marker.Options.Popup.EventActivationFlags.EnabledEvents,
                    Id = marker.Options.Popup.Id,
                    Options = marker.Options.Popup.Options
                } : null
            }),
            DotNetObjectReference.Create(_htmlMarkerInvokeHelper));
        }

        /// <summary>
        /// Update HtmlMarkers on the map
        /// </summary>
        /// <param name="updates">HtmlMarkers to update</param>
        /// <returns></returns>
        public async ValueTask UpdateHtmlMarkersAsync(params HtmlMarkerUpdate[] updates) => await UpdateHtmlMarkersAsync(updates as IEnumerable<HtmlMarkerUpdate>);

        /// <summary>
        /// Update HtmlMarkers on the map
        /// </summary>
        /// <param name="updates">HtmlMarkers to update</param>
        /// <returns></returns>
        public async ValueTask UpdateHtmlMarkersAsync(IEnumerable<HtmlMarkerUpdate> updates)
        {
            if (updates != null)
            {
                _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_UpdateHtmlMarkersAsync, "Updating html markers");
                _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_UpdateHtmlMarkersAsync, $"{updates.Count()} html markers will be updated");
                _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_UpdateHtmlMarkersAsync, $"Ids: {string.Join('|', updates.Select(h => h.Marker.Id))}");

                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.UpdateHtmlMarkers.ToCoreNamespace(),
                updates.Select(update => new HtmlMarkerCreationOptions {
                    Id = update.Marker.Id,
                    Options = update.Options,
                    PopupOptions = update.Options?.Popup != null ? new HtmlMarkerPopupCreationOptions {
                        Events = update.Options.Popup.EventActivationFlags.EnabledEvents,
                        Id = update.Options.Popup.Id,
                        Options = update.Options.Popup.Options
                    } : null
                }));

                foreach (var updateWithPopup in updates.Where(update => update.Options?.Popup != null))
                {
                    var marker = HtmlMarkers.First(marker => marker.Id == updateWithPopup.Marker.Id);
                    marker.Options.Popup = updateWithPopup.Options.Popup;

                    if (marker.Options.Popup.JSRuntime == null)
                    {
                        marker.Options.Popup.JSRuntime = _jsRuntime;
                        marker.Options.Popup.OnRemoved += () => RemovePopup(marker.Options.Popup.Id);
                    }
                    if (marker.Options.Popup.Logger == null)
                    {
                        marker.Options.Popup.Logger = _logger;
                    }
                }
            }
        }

        /// <summary>
        /// Remove HtmlMarkers from the map
        /// </summary>
        /// <param name="markers">HtmlMarkers to remove</param>
        /// <returns></returns>
        public async ValueTask RemoveHtmlMarkersAsync(params HtmlMarker[] markers) => await RemoveHtmlMarkersAsync(markers as IEnumerable<HtmlMarker>);

        /// <summary>
        /// Remove HtmlMarkers from the map
        /// </summary>
        /// <param name="markers">HtmlMarkers to remove</param>
        /// <returns></returns>
        public async ValueTask RemoveHtmlMarkersAsync(IEnumerable<HtmlMarker> markers)
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
        public async ValueTask ClearHtmlMarkersAsync()
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
        public async ValueTask AddLayerAsync<T>(T layer) where T : Layer => await AddLayerAsync(layer, null);

        /// <summary>
        /// Add a layer to the map
        /// </summary>
        /// <typeparam name="T">Type of layer to add</typeparam>
        /// <param name="layer">Layer to add</param>
        /// <param name="before">ID of the layer before which the new layer will be added</param>
        /// <returns></returns>
        public async ValueTask AddLayerAsync<T>(T layer, string before) where T : Layer
        {
            if (layer == null)
            {
                return;
            }

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
                layer.GetLayerOptions(),
                layer.EventActivationFlags.EnabledEvents,
                DotNetObjectReference.Create(_layerEventInvokeHelper),
                layer is HtmlMarkerLayer htmlMarkerLayer ? DotNetObjectReference.Create(htmlMarkerLayer.MarkerCallbackInvokeHelper) : null);
        }

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layers">Layers to remove</param>
        /// <returns></returns>
        public async ValueTask RemoveLayersAsync(IEnumerable<Layer> layers) => await RemoveLayersAsync(layers?.Select(l => l.Id));

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layers">Layers to remove</param>
        /// <returns></returns>
        public async ValueTask RemoveLayersAsync(params Layer[] layers) => await RemoveLayersAsync(layers?.Select(l => l.Id));

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layerIds">ID of the layers to remove</param>
        /// <returns></returns>
        public async ValueTask RemoveLayersAsync(params string[] layerIds) => await RemoveLayersAsync(layerIds as IEnumerable<string>);

        /// <summary>
        /// Remove layers from the map
        /// </summary>
        /// <param name="layerIds">ID of the layers to remove</param>
        /// <returns></returns>
        public async ValueTask RemoveLayersAsync(IEnumerable<string> layerIds)
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
        public async ValueTask ClearLayersAsync()
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
        public async ValueTask ClearMapAsync()
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
        public async ValueTask SetCameraOptionsAsync(Action<CameraOptions> configure)
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
        /// Get the camera options
        /// </summary>
        /// <returns>Current camera options</returns>
        public async ValueTask<CameraOptions> GetCameraOptionsAsync()
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_GetCameraOptionsAsync, "Map - GetCameraOptionsAsync");
            CameraOptions = await _jsRuntime.InvokeAsync<CameraOptions>(Constants.JsConstants.Methods.Core.GetCamera.ToCoreNamespace());
            return CameraOptions;
        }

        /// <summary>
        /// Get the style options
        /// </summary>
        /// <returns>Current style options</returns>
        public async ValueTask<StyleOptions> GetStyleOptionsAsync()
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_GetStyleOptionsAsync, "Map - GetStyleOptionsAsync");
            StyleOptions = await _jsRuntime.InvokeAsync<StyleOptions>(Constants.JsConstants.Methods.Core.GetStyle.ToCoreNamespace());
            return StyleOptions;
        }

        /// <summary>
        /// Get the traffic options
        /// </summary>
        /// <returns>Current traffic options</returns>
        public async ValueTask<TrafficOptions> GetTrafficOptionsAsync()
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_GetTrafficOptionsAsync, "Map - GetTrafficOptionsAsync");
            TrafficOptions = await _jsRuntime.InvokeAsync<TrafficOptions>(Constants.JsConstants.Methods.Core.GetTraffic.ToCoreNamespace());
            return TrafficOptions;
        }

        /// <summary>
        /// Get the user interaction options
        /// </summary>
        /// <returns>Current user interactions</returns>
        public async ValueTask<UserInteractionOptions> GetUserInteractionOptionsAsync()
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_GetUserInteractionOptionsAsync, "Map - GetUserInteractionOptionsAsync");
            UserInteractionOptions = await _jsRuntime.InvokeAsync<UserInteractionOptions>(Constants.JsConstants.Methods.Core.GetUserInteraction.ToCoreNamespace());
            return UserInteractionOptions;
        }

        /// <summary>
        /// Update the style options of the map
        /// </summary>
        /// <param name="configure">Action setting the style options</param>
        /// <returns></returns>
        public async ValueTask SetStyleOptionsAsync(Action<StyleOptions> configure)
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
        public async ValueTask SetUserInteractionAsync(Action<UserInteractionOptions> configure)
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
        public async ValueTask SetTrafficOptionsAsync(Action<TrafficOptions> configure)
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
        ///<exception cref="PopupAlreadyExistingException">A popup with the same id already exists</exception>
        ///<exception cref="ArgumentNullException">The given popup is null</exception>
        public async ValueTask AddPopupAsync(Popup popup)
        {
            Require.NotNull(popup, nameof(popup));

            if (_popups == null)
            {
                _popups = new List<Popup>();
            }

            if (_popups.Any(p => p.Id == popup.Id))
            {
                throw new PopupAlreadyExistingException(popup.Id);
            }

            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddPopupAsync, "Map - AddPopupAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddPopupAsync, $"Id: {popup.Id}");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddPopupAsync, $"Events: {string.Join('|', popup.EventActivationFlags.EnabledEvents)}");
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopup.ToCoreNamespace(), popup.Id, popup.Options, popup.EventActivationFlags.EnabledEvents, DotNetObjectReference.Create(_popupInvokeHelper));

            popup.JSRuntime = _jsRuntime;
            popup.Logger = _logger;

            popup.OnRemoved += () => RemovePopup(popup.Id);

            _popups.Add(popup);
        }

        /// <summary>
        /// Add a new popup with the given template and properties to the map
        /// </summary>
        /// <param name="popup">Popup to add to the map</param>
        /// <param name="template">Template of the popup</param>
        /// <param name="properties">Properties to apply to the template</param>
        /// <returns></returns>
        /// <exception cref="PopupAlreadyExistingException">A popup with the same id already exists</exception>
        /// <exception cref="ArgumentNullException">The popup, the template or the properties are null</exception>
        public async ValueTask AddPopupAsync(Popup popup, PopupTemplate template, IDictionary<string, object> properties)
        {
            Require.NotNull(popup, nameof(popup));
            Require.NotNull(template, nameof(template));
            Require.NotNull(properties, nameof(properties));

            if (_popups == null)
            {
                _popups = new List<Popup>();
            }

            if (_popups.Any(p => p.Id == popup.Id))
            {
                throw new PopupAlreadyExistingException(popup.Id);
            }

            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_AddPopupAsync, "Map - AddPopupAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddPopupAsync, $"Id: {popup.Id}");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddPopupAsync, $"Events: {string.Join('|', popup.EventActivationFlags.EnabledEvents)}");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddPopupAsync, $"Template: {template}");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_AddPopupAsync, $"Properties: {string.Join('|', properties.Select(kvp => kvp.Key + " : " + kvp.Value))}");

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopupWithTemplate.ToCoreNamespace(),
                popup.Id,
                popup.Options,
                properties,
                template,
                popup.EventActivationFlags.EnabledEvents,
                DotNetObjectReference.Create(_popupInvokeHelper)
            );

            popup.JSRuntime = _jsRuntime;
            popup.Logger = _logger;

            popup.OnRemoved += () => RemovePopup(popup.Id);

            _popups.Add(popup);
        }

        /// <summary>
        /// Remove a popup from the map
        /// </summary>
        /// <param name="id">ID of the popup to remove</param>
        /// <returns></returns>
        public async ValueTask RemovePopupAsync(string id)
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
        public async ValueTask RemovePopupAsync(Popup popup) => await RemovePopupAsync(popup.Id);

        /// <summary>
        /// Remove all the popups from the map
        /// </summary>
        /// <returns></returns>
        public async ValueTask ClearPopupsAsync()
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
        public async ValueTask CreateImageFromTemplateAsync(string id, string templateName, string color = null, string secondaryColor = null, decimal? scale = null)
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

        /// <summary>
        /// Set a property on the style of the map's canvas
        /// </summary>
        /// <param name="property">Name of the property to set</param>
        /// <param name="value">Value of the property</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">The property parameter is null or made of whitespaces</exception>
        public async ValueTask SetCanvasStylePropertyAsync(string property, string value)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_SetCanvasStylePropertyAsync, "Map - SetCanvasStylePropertyAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_SetCanvasStylePropertyAsync, "Property", property);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_SetCanvasStylePropertyAsync, "Value", value);

            Require.NotNullOrWhiteSpace(property, nameof(property));

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasStyleProperty.ToCoreNamespace(), property, value);
        }

        /// <summary>
        /// Set properties on the style of the map's canvas
        /// </summary>
        /// <param name="properties">Properties to set</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">The given dictionary is null</exception>
        public async ValueTask SetCanvasStylePropertiesAsync(IReadOnlyDictionary<string, string> properties)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_SetCanvasStylePropertiesAsync, "Map - SetCanvasStylePropertiesAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_SetCanvasStylePropertiesAsync, "Properties", properties);

            Require.NotNull(properties, nameof(properties));

            var definedProperties = properties.Where(property => !string.IsNullOrWhiteSpace(property.Key));
            if (definedProperties.Any())
            {
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasStyleProperties.ToCoreNamespace(), definedProperties);
            }
        }

        /// <summary>
        /// Set a property on the style of the map's canvas container
        /// </summary>
        /// <param name="property">Name of the property to set</param>
        /// <param name="value">Value of the property</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">The property parameter is null or made of whitespaces</exception>
        public async ValueTask SetCanvasContainerStylePropertyAsync(string property, string value)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_SetCanvasContainerStylePropertyAsync, "Map - SetCanvasContainerStylePropertyAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_SetCanvasContainerStylePropertyAsync, "Property", property);
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_SetCanvasContainerStylePropertyAsync, "Value", value);

            Require.NotNullOrWhiteSpace(property, nameof(property));

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasContainerStyleProperty.ToCoreNamespace(), property, value);
        }

        /// <summary>
        /// Set properties on the style of the map's canvas container
        /// </summary>
        /// <param name="properties">Properties to set</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">The given dictionary is null</exception>
        public async ValueTask SetCanvasContainerStylePropertiesAsync(IReadOnlyDictionary<string, string> properties)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Map_SetCanvasContainerStylePropertiesAsync, "Map - SetCanvasContainerStylePropertiesAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Map_SetCanvasContainerStylePropertiesAsync, "Properties", properties);

            Require.NotNull(properties, nameof(properties));

            var definedProperties = properties.Where(property => !string.IsNullOrWhiteSpace(property.Key));
            if (definedProperties.Any())
            {
                await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasContainerStyleProperties.ToCoreNamespace(), definedProperties);
            }
        }

    }
}
