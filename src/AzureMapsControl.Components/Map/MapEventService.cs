namespace AzureMapsControl.Map
{
    internal class MapEventService
    {
        public event MapEventHandler OnBoxZoomEnd;
        public event MapEventHandler OnBoxZoomStart;
        public event MapMouseEventHandler OnClick;
        public event MapDataEventHandler OnData;
        public event MapMouseEventHandler OnDblClick;
        public event MapEventHandler OnDrag;
        public event MapEventHandler OnDragEnd;
        public event MapEventHandler OnDragStart;
        public event MapErrorEventHandler OnError;
        public event MapEventHandler OnIdle;
        public event MapLayerEventHandler OnLayerAdded;
        public event MapLayerEventHandler OnLayerRemoved;
        public event MapMouseEventHandler OnMouseDown;
        public event MapMouseEventHandler OnMouseMove;
        public event MapMouseEventHandler OnMouseOut;
        public event MapMouseEventHandler OnMouseOver;
        public event MapMouseEventHandler OnMouseUp;
        public event MapEventHandler OnMove;
        public event MapEventHandler OnMoveEnd;
        public event MapEventHandler OnMoveStart;
        public event MapEventHandler OnPitch;
        public event MapEventHandler OnPitchEnd;
        public event MapEventHandler OnPitchStart;
        public event MapEventHandler OnReady;
        public event MapEventHandler OnRender;
        public event MapEventHandler OnResize;
        public event MapEventHandler OnRotate;
        public event MapEventHandler OnRotateEnd;
        public event MapEventHandler OnRotateStart;
        public event MapDataEventHandler OnSourceAdded;
        public event MapDataEventHandler OnSourceData;
        public event MapDataEventHandler OnSourceRemoved;
        public event MapDataEventHandler OnStyleData;
        public event MapMessageEventHandler OnStyleImageMissing;
        public event MapEventHandler OnTokenAcquired;
        public event MapTouchEventHandler OnTouchCancel;
        public event MapTouchEventHandler OnTouchEnd;
        public event MapTouchEventHandler OnTouchMove;
        public event MapTouchEventHandler OnTouchStart;
        public event MapEventHandler OnWheel;
        public event MapEventHandler OnZoom;
        public event MapEventHandler OnZoomEnd;
        public event MapEventHandler OnZoomStart;

        internal void DispatchEvent(MapJsEventArgs mapEvent)
        {
            switch (mapEvent.Type)
            {
                case "boxzoomend":
                    OnBoxZoomEnd?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "boxzoomstart":
                    OnBoxZoomStart?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "click":
                    OnClick?.Invoke(new MapMouseEventArgs(mapEvent));
                    break;
                case "data":
                    OnData?.Invoke(new MapDataEventArgs(mapEvent));
                    break;
                case "dblclick":
                    OnDblClick?.Invoke(new MapMouseEventArgs(mapEvent));
                    break;
                case "drag":
                    OnDrag?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "dragend":
                    OnDragEnd?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "dragstart":
                    OnDragStart?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "error":
                    OnError?.Invoke(new MapErrorEventArgs(mapEvent));
                    break;
                case "idle":
                    OnIdle?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "layeradded":
                    OnLayerAdded?.Invoke(new MapLayerEventArgs(mapEvent));
                    break;
                case "layerremoved":
                    OnLayerRemoved?.Invoke(new MapLayerEventArgs(mapEvent));
                    break;
                case "mousedown":
                    OnMouseDown?.Invoke(new MapMouseEventArgs(mapEvent));
                    break;
                case "mousemove":
                    OnMouseMove?.Invoke(new MapMouseEventArgs(mapEvent));
                    break;
                case "mouseout":
                    OnMouseOut?.Invoke(new MapMouseEventArgs(mapEvent));
                    break;
                case "mouseover":
                    OnMouseOver?.Invoke(new MapMouseEventArgs(mapEvent));
                    break;
                case "mouseup":
                    OnMouseUp?.Invoke(new MapMouseEventArgs(mapEvent));
                    break;
                case "move":
                    OnMove?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "moveend":
                    OnMoveEnd?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "movestart":
                    OnMoveStart?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "pitch":
                    OnPitch?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "pitchend":
                    OnPitchEnd?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "pitchstart":
                    OnPitchStart?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "ready":
                    OnReady?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "render":
                    OnRender?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "resize":
                    OnResize?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "rotate":
                    OnRotate?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "rotateend":
                    OnRotateEnd?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "rotatestart":
                    OnRotateStart?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "sourceadded":
                    OnSourceAdded?.Invoke(new MapDataEventArgs(mapEvent));
                    break;
                case "sourcedate":
                    OnSourceData?.Invoke(new MapDataEventArgs(mapEvent));
                    break;
                case "sourceremoved":
                    OnSourceRemoved?.Invoke(new MapDataEventArgs(mapEvent));
                    break;
                case "styledata":
                    OnStyleData?.Invoke(new MapDataEventArgs(mapEvent));
                    break;
                case "styleimagemissing":
                    OnStyleImageMissing?.Invoke(new MapMessageEventArgs(mapEvent));
                    break;
                case "tokenacquired":
                    OnTokenAcquired?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "touchcancel":
                    OnTouchCancel?.Invoke(new MapTouchEventArgs(mapEvent));
                    break;
                case "touchend":
                    OnTouchEnd?.Invoke(new MapTouchEventArgs(mapEvent));
                    break;
                case "touchmove":
                    OnTouchMove?.Invoke(new MapTouchEventArgs(mapEvent));
                    break;
                case "touchstart":
                    OnTouchStart?.Invoke(new MapTouchEventArgs(mapEvent));
                    break;
                case "wheel":
                    OnWheel?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "zoom":
                    OnZoom?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "zoomend":
                    OnZoomEnd?.Invoke(new MapEventArgs(mapEvent));
                    break;
                case "zoomstart":
                    OnZoomStart?.Invoke(new MapEventArgs(mapEvent));
                    break;
            }
        }
    }
}
