namespace AzureMapsControl.Components.Layers
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class LayerEventType : AtlasEventType
    {
        public static readonly LayerEventType Click = new LayerEventType("click");
        public static readonly LayerEventType ContextMenu = new LayerEventType("contextmenu");
        public static readonly LayerEventType DblClick = new LayerEventType("dblclick");
        public static readonly LayerEventType LayerAdded = new LayerEventType("layeradded");
        public static readonly LayerEventType LayerRemoved = new LayerEventType("layerremoved");
        public static readonly LayerEventType MouseDown = new LayerEventType("mousedown");
        public static readonly LayerEventType MouseEnter = new LayerEventType("mouseenter");
        public static readonly LayerEventType MouseLeave = new LayerEventType("mouseleave");
        public static readonly LayerEventType MouseMove = new LayerEventType("mousemove");
        public static readonly LayerEventType MouseOut = new LayerEventType("mouseout");
        public static readonly LayerEventType MouseOver = new LayerEventType("mouseover");
        public static readonly LayerEventType MouseUp = new LayerEventType("mouseup");
        public static readonly LayerEventType TouchCancel = new LayerEventType("touchcancel");
        public static readonly LayerEventType TouchEnd = new LayerEventType("touchend");
        public static readonly LayerEventType TouchMove = new LayerEventType("touchmove");
        public static readonly LayerEventType TouchStart = new LayerEventType("touchstart");
        public static readonly LayerEventType Wheel = new LayerEventType("wheel");

        private LayerEventType(string atlasEvent) : base(atlasEvent)
        {
        }
    }
}
