namespace AzureMapsControl.Components.Markers
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class HtmlMarkerEventType : AtlasEventType
    {
        public static readonly HtmlMarkerEventType Click = new HtmlMarkerEventType("click");
        public static readonly HtmlMarkerEventType ContextMenu = new HtmlMarkerEventType("contextmenu");
        public static readonly HtmlMarkerEventType DblClick = new HtmlMarkerEventType("dblclick");
        public static readonly HtmlMarkerEventType Drag = new HtmlMarkerEventType("drag");
        public static readonly HtmlMarkerEventType DragEnd = new HtmlMarkerEventType("dragend");
        public static readonly HtmlMarkerEventType DragStart = new HtmlMarkerEventType("dragstart");
        public static readonly HtmlMarkerEventType KeyDown = new HtmlMarkerEventType("keydown");
        public static readonly HtmlMarkerEventType KeyPress = new HtmlMarkerEventType("keypress");
        public static readonly HtmlMarkerEventType KeyUp = new HtmlMarkerEventType("keyup");
        public static readonly HtmlMarkerEventType MouseDown = new HtmlMarkerEventType("mousedown");
        public static readonly HtmlMarkerEventType MouseEnter = new HtmlMarkerEventType("mouseenter");
        public static readonly HtmlMarkerEventType MouseLeave = new HtmlMarkerEventType("mouseleave");
        public static readonly HtmlMarkerEventType MouseMove = new HtmlMarkerEventType("mousemove");
        public static readonly HtmlMarkerEventType MouseOut = new HtmlMarkerEventType("mouseout");
        public static readonly HtmlMarkerEventType MouseOver = new HtmlMarkerEventType("mouseover");
        public static readonly HtmlMarkerEventType MouseUp = new HtmlMarkerEventType("mouseup");
        private HtmlMarkerEventType(string type) : base(type) { }
    }
}
