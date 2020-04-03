namespace AzureMapsControl.Components.Markers
{
    using AzureMapsControl.Components.Events;

    public sealed class HtmlMarkerEventType : AtlasEventType
    {
        public static HtmlMarkerEventType Click = new HtmlMarkerEventType("click");
        public static HtmlMarkerEventType ContextMenu = new HtmlMarkerEventType("contextmenu");
        public static HtmlMarkerEventType DblClick = new HtmlMarkerEventType("dblclick");
        public static HtmlMarkerEventType Drag = new HtmlMarkerEventType("drag");
        public static HtmlMarkerEventType DragEnd = new HtmlMarkerEventType("dragend");
        public static HtmlMarkerEventType DragStart = new HtmlMarkerEventType("dragstart");
        public static HtmlMarkerEventType KeyDown = new HtmlMarkerEventType("keydown");
        public static HtmlMarkerEventType KeyPress = new HtmlMarkerEventType("keypress");
        public static HtmlMarkerEventType KeyUp = new HtmlMarkerEventType("keyup");
        public static HtmlMarkerEventType MouseDown = new HtmlMarkerEventType("mousedown");
        public static HtmlMarkerEventType MouseEnter = new HtmlMarkerEventType("mouseenter");
        public static HtmlMarkerEventType MouseLeave = new HtmlMarkerEventType("mouseleave");
        public static HtmlMarkerEventType MouseMove = new HtmlMarkerEventType("mousemove");
        public static HtmlMarkerEventType MouseOut = new HtmlMarkerEventType("mouseout");
        public static HtmlMarkerEventType MouseOver = new HtmlMarkerEventType("mouseover");
        public static HtmlMarkerEventType MouseUp = new HtmlMarkerEventType("mouseup");
        private HtmlMarkerEventType(string type) : base(type) { }
    }
}
