namespace AzureMapsControl.Components.Map
{
    using AzureMapsControl.Components.Events;

    public sealed class MapEventType : AtlasEventType
    {
        public static MapEventType BoxZoomEnd = new MapEventType("boxzoomend");
        public static MapEventType BoxZoomStart = new MapEventType("boxzoomstart");
        public static MapEventType Click = new MapEventType("click");
        public static MapEventType ContextMenu = new MapEventType("contextmenu");
        public static MapEventType Data = new MapEventType("data");
        public static MapEventType DblClick = new MapEventType("dblclick");
        public static MapEventType Drag = new MapEventType("drag");
        public static MapEventType DragEnd = new MapEventType("dragend");
        public static MapEventType DragStart = new MapEventType("dragstart");
        public static MapEventType Error = new MapEventType("error");
        public static MapEventType Idle = new MapEventType("idle");
        public static MapEventType LayerAdded = new MapEventType("layeradded");
        public static MapEventType LayerRemoved = new MapEventType("layerremoved");
        public static MapEventType Load = new MapEventType("load");
        public static MapEventType MouseDown = new MapEventType("mousedown");
        public static MapEventType MouseMove = new MapEventType("mousemove");
        public static MapEventType MouseOut = new MapEventType("mouseout");
        public static MapEventType MouseOver = new MapEventType("mouseover");
        public static MapEventType MouseUp = new MapEventType("mouseup");
        public static MapEventType Move = new MapEventType("move");
        public static MapEventType MoveEnd = new MapEventType("moveend");
        public static MapEventType MoveStart = new MapEventType("movestart");
        public static MapEventType Pitch = new MapEventType("pitch");
        public static MapEventType PitchEnd = new MapEventType("pitchend");
        public static MapEventType PitchStart = new MapEventType("pitchstart");
        public static MapEventType Ready = new MapEventType("ready");
        public static MapEventType Render = new MapEventType("render");
        public static MapEventType Resize = new MapEventType("resize");
        public static MapEventType Rotate = new MapEventType("rotate");
        public static MapEventType RotateEnd = new MapEventType("rotateend");
        public static MapEventType RotateStart = new MapEventType("rotatestart");
        public static MapEventType SourceAdded = new MapEventType("sourceadded");
        public static MapEventType SourceDate = new MapEventType("sourcedate");
        public static MapEventType SourceRemoved = new MapEventType("sourceremoved");
        public static MapEventType StyleData = new MapEventType("styledata");
        public static MapEventType StyleImageMissing = new MapEventType("styleimagemissing");
        public static MapEventType TokenAcquired = new MapEventType("tokenacquired");
        public static MapEventType TouchCancel = new MapEventType("touchcancel");
        public static MapEventType TouchEnd = new MapEventType("touchend");
        public static MapEventType TouchMove = new MapEventType("touchmove");
        public static MapEventType TouchStart = new MapEventType("touchstart");
        public static MapEventType Wheel = new MapEventType("wheel");
        public static MapEventType Zoom = new MapEventType("zoom");
        public static MapEventType ZoomEnd = new MapEventType("zoomend");
        public static MapEventType ZoomStart = new MapEventType("zoomstart");

        private MapEventType(string type) : base(type) { }
    }
}
