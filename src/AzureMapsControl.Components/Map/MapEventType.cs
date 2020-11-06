namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class MapEventType : AtlasEventType
    {
        public static readonly MapEventType BoxZoomEnd = new MapEventType("boxzoomend");
        public static readonly MapEventType BoxZoomStart = new MapEventType("boxzoomstart");
        public static readonly MapEventType Click = new MapEventType("click");
        public static readonly MapEventType ContextMenu = new MapEventType("contextmenu");
        public static readonly MapEventType Data = new MapEventType("data");
        public static readonly MapEventType DblClick = new MapEventType("dblclick");
        public static readonly MapEventType Drag = new MapEventType("drag");
        public static readonly MapEventType DragEnd = new MapEventType("dragend");
        public static readonly MapEventType DragStart = new MapEventType("dragstart");
        public static readonly MapEventType Error = new MapEventType("error");
        public static readonly MapEventType Idle = new MapEventType("idle");
        public static readonly MapEventType LayerAdded = new MapEventType("layeradded");
        public static readonly MapEventType LayerRemoved = new MapEventType("layerremoved");
        public static readonly MapEventType Load = new MapEventType("load");
        public static readonly MapEventType MouseDown = new MapEventType("mousedown");
        public static readonly MapEventType MouseMove = new MapEventType("mousemove");
        public static readonly MapEventType MouseOut = new MapEventType("mouseout");
        public static readonly MapEventType MouseOver = new MapEventType("mouseover");
        public static readonly MapEventType MouseUp = new MapEventType("mouseup");
        public static readonly MapEventType Move = new MapEventType("move");
        public static readonly MapEventType MoveEnd = new MapEventType("moveend");
        public static readonly MapEventType MoveStart = new MapEventType("movestart");
        public static readonly MapEventType Pitch = new MapEventType("pitch");
        public static readonly MapEventType PitchEnd = new MapEventType("pitchend");
        public static readonly MapEventType PitchStart = new MapEventType("pitchstart");
        public static readonly MapEventType Ready = new MapEventType("ready");
        public static readonly MapEventType Render = new MapEventType("render");
        public static readonly MapEventType Resize = new MapEventType("resize");
        public static readonly MapEventType Rotate = new MapEventType("rotate");
        public static readonly MapEventType RotateEnd = new MapEventType("rotateend");
        public static readonly MapEventType RotateStart = new MapEventType("rotatestart");
        public static readonly MapEventType SourceAdded = new MapEventType("sourceadded");
        public static readonly MapEventType SourceDate = new MapEventType("sourcedate");
        public static readonly MapEventType SourceRemoved = new MapEventType("sourceremoved");
        public static readonly MapEventType StyleData = new MapEventType("styledata");
        public static readonly MapEventType StyleImageMissing = new MapEventType("styleimagemissing");
        public static readonly MapEventType TokenAcquired = new MapEventType("tokenacquired");
        public static readonly MapEventType TouchCancel = new MapEventType("touchcancel");
        public static readonly MapEventType TouchEnd = new MapEventType("touchend");
        public static readonly MapEventType TouchMove = new MapEventType("touchmove");
        public static readonly MapEventType TouchStart = new MapEventType("touchstart");
        public static readonly MapEventType Wheel = new MapEventType("wheel");
        public static readonly MapEventType Zoom = new MapEventType("zoom");
        public static readonly MapEventType ZoomEnd = new MapEventType("zoomend");
        public static readonly MapEventType ZoomStart = new MapEventType("zoomstart");

        private MapEventType(string type) : base(type) { }
    }
}
