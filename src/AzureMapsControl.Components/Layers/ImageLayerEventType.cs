namespace AzureMapsControl.Components.Layers
{
    using AzureMapsControl.Components.Events;

    public sealed class ImageLayerEventType : AtlasEventType
    {
        public static ImageLayerEventType Click = new ImageLayerEventType("click");
        public static ImageLayerEventType ContextMenu = new ImageLayerEventType("contextmenu");
        public static ImageLayerEventType DblClick = new ImageLayerEventType("dblclick");
        public static ImageLayerEventType LayerAdded = new ImageLayerEventType("layeradded");
        public static ImageLayerEventType LayerRemoved = new ImageLayerEventType("layerremoved");
        public static ImageLayerEventType MouseDown = new ImageLayerEventType("mousedown");
        public static ImageLayerEventType MouseEnter = new ImageLayerEventType("mouseenter");
        public static ImageLayerEventType MouseLeave = new ImageLayerEventType("mouseleave");
        public static ImageLayerEventType MouseMove = new ImageLayerEventType("mousemove");
        public static ImageLayerEventType MouseOut = new ImageLayerEventType("mouseout");
        public static ImageLayerEventType MouseOver = new ImageLayerEventType("mouseover");
        public static ImageLayerEventType MouseUp = new ImageLayerEventType("mouseup");
        public static ImageLayerEventType TouchCancel = new ImageLayerEventType("touchcancel");
        public static ImageLayerEventType TouchEnd = new ImageLayerEventType("touchend");
        public static ImageLayerEventType TouchMove = new ImageLayerEventType("touchmove");
        public static ImageLayerEventType TouchStart = new ImageLayerEventType("touchstart");
        public static ImageLayerEventType Wheel = new ImageLayerEventType("wheel");

        internal ImageLayerEventType(string atlasEvent) : base(atlasEvent)
        {
        }
    }
}
