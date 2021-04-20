namespace AzureMapsControl.Components.Layers
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class LayerEventType : AtlasEventType
    {
        public static LayerEventType Click = new LayerEventType("click");
        public static LayerEventType ContextMenu = new LayerEventType("contextmenu");
        public static LayerEventType DblClick = new LayerEventType("dblclick");
        public static LayerEventType LayerAdded = new LayerEventType("layeradded");
        public static LayerEventType LayerRemoved = new LayerEventType("layerremoved");
        public static LayerEventType MouseDown = new LayerEventType("mousedown");
        public static LayerEventType MouseEnter = new LayerEventType("mouseenter");
        public static LayerEventType MouseLeave = new LayerEventType("mouseleave");
        public static LayerEventType MouseMove = new LayerEventType("mousemove");
        public static LayerEventType MouseOut = new LayerEventType("mouseout");
        public static LayerEventType MouseOver = new LayerEventType("mouseover");
        public static LayerEventType MouseUp = new LayerEventType("mouseup");
        public static LayerEventType TouchCancel = new LayerEventType("touchcancel");
        public static LayerEventType TouchEnd = new LayerEventType("touchend");
        public static LayerEventType TouchMove = new LayerEventType("touchmove");
        public static LayerEventType TouchStart = new LayerEventType("touchstart");
        public static LayerEventType Wheel = new LayerEventType("wheel");

        private LayerEventType(string atlasEvent) : base(atlasEvent)
        {
        }
    }
}
