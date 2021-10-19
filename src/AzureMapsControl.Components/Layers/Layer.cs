namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    public delegate void LayerMouseEvent(MapMouseEventArgs eventArgs);
    public delegate void LayerTouchEvent(MapTouchEventArgs eventArgs);
    public delegate void LayerEvent(MapEventArgs eventArgs);

    public abstract class Layer
    {
        public string Id { get; }
        public LayerEventActivationFlags EventActivationFlags { get; set; }

        internal LayerType Type { get; private set; }

        public event LayerMouseEvent OnClick;
        public event LayerMouseEvent OnContextMenu;
        public event LayerMouseEvent OnDblClick;
        public event LayerEvent OnLayerAdded;
        public event LayerEvent OnLayerRemoved;
        public event LayerMouseEvent OnMouseDown;
        public event LayerMouseEvent OnMouseEnter;
        public event LayerMouseEvent OnMouseLeave;
        public event LayerMouseEvent OnMouseMove;
        public event LayerMouseEvent OnMouseOut;
        public event LayerMouseEvent OnMouseOver;
        public event LayerMouseEvent OnMouseUp;
        public event LayerTouchEvent OnTouchCancel;
        public event LayerTouchEvent OnTouchEnd;
        public event LayerTouchEvent OnTouchMove;
        public event LayerTouchEvent OnTouchStart;
        public event LayerEvent OnWheel;

        internal Layer(string id, LayerType type)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            Type = type;
            EventActivationFlags = LayerEventActivationFlags.None();
        }

        internal abstract LayerOptions GetLayerOptions();
        internal abstract void AddInterop(IMapJsRuntime jsRuntime, ILogger logger);

        internal void DispatchEvent(Map map, MapJsEventArgs eventArgs)
        {
            switch (eventArgs.Type)
            {
                case "click":
                    OnClick?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "contextmenu":
                    OnContextMenu?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "dblclick":
                    OnDblClick?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "layeradded":
                    OnLayerAdded?.Invoke(new MapEventArgs(map, eventArgs.Type));
                    break;

                case "layerremoved":
                    OnLayerRemoved?.Invoke(new MapEventArgs(map, eventArgs.Type));
                    break;

                case "mousedown":
                    OnMouseDown?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "mouseenter":
                    OnMouseEnter?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "mouseleave":
                    OnMouseLeave?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "mousemove":
                    OnMouseMove?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "mouseout":
                    OnMouseOut?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "mouseover":
                    OnMouseOver?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "mouseup":
                    OnMouseUp?.Invoke(new MapMouseEventArgs(map, eventArgs));
                    break;

                case "touchcancel":
                    OnTouchCancel?.Invoke(new MapTouchEventArgs(map, eventArgs));
                    break;

                case "touchend":
                    OnTouchEnd?.Invoke(new MapTouchEventArgs(map, eventArgs));
                    break;
                case "touchmove":
                    OnTouchMove?.Invoke(new MapTouchEventArgs(map, eventArgs));
                    break;

                case "touchstart":
                    OnTouchStart?.Invoke(new MapTouchEventArgs(map, eventArgs));
                    break;

                case "wheel":
                    OnWheel?.Invoke(new MapEventArgs(map, eventArgs.Type));
                    break;
            }
        }

    }

    public abstract class Layer<TOptions> : Layer
        where TOptions : LayerOptions, new()
    {
        internal IMapJsRuntime _mapJsRuntime;
        internal ILogger _logger;

        public TOptions Options { get; set; }

        internal Layer(string id, LayerType type) : base(id, type)
        {
        }

        /// <summary>
        /// Set the options of a layer
        /// </summary>
        /// <param name="update">Update to apply on the options</param>
        /// <exception cref="Exceptions.ComponentNotAddedToMapException">The control has not been added to the map</exception>
        /// <returns></returns>
        public async ValueTask SetOptionsAsync(Action<TOptions> update)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.Layer_SetOptionsAsync, "Layer - SetOptionsAsync");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.Layer_SetOptionsAsync, $"Id: {Id}");

            EnsureJsRuntimeExists();

            if (Options is null)
            {
                Options = new();
            }

            update(Options);

            await _mapJsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Layer.SetOptions.ToLayerNamespace(), Id, Options);
        }

        internal override LayerOptions GetLayerOptions() => Options;
        internal override void AddInterop(IMapJsRuntime jsRuntime, ILogger logger)
        {
            _mapJsRuntime = jsRuntime;
            _logger = logger;
        }

        private void EnsureJsRuntimeExists()
        {
            if (_mapJsRuntime is null)
            {
                throw new ComponentNotAddedToMapException();
            }
        }
    }
}
