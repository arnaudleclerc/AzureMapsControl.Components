namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;
    using AzureMapsControl.Components.Map;

    public delegate void LayerMouseEvent(MapMouseEventArgs eventArgs);
    public delegate void LayerTouchEvent(MapTouchEventArgs eventArgs);
    public delegate void LayerEvent(MapEventArgs eventArgs);

    public abstract class Layer
    {
        public string Id { get; }
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
        }

        internal abstract LayerOptions GetLayerOptions();
        internal abstract IEnumerable<string> GetEnabledEvents();

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

    public abstract class Layer<T> : Layer
        where T : EventActivationFlags
    {
        public T EventActivationFlags { get; set; }
        internal Layer(string id, LayerType type) : base(id, type)
        {
        }

        internal override IEnumerable<string> GetEnabledEvents() => EventActivationFlags.EnabledEvents;

    }

    public abstract class Layer<T, U> : Layer<U>
        where T : LayerOptions
        where U : EventActivationFlags
    {
        public T Options { get; set; }

        internal Layer(string id, LayerType type) : base(id, type)
        {
        }

        internal override LayerOptions GetLayerOptions() => Options;
    }
}
