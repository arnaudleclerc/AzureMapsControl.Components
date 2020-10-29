namespace AzureMapsControl.Components.Layers
{
    using AzureMapsControl.Components.Events;

    public sealed class TileLayerEventType : AtlasEventType
    {
        public static TileLayerEventType Click = new TileLayerEventType("click");
        public static TileLayerEventType ContextMenu = new TileLayerEventType("contextmenu");
        public static TileLayerEventType DblClick = new TileLayerEventType("dblclick");
        public static TileLayerEventType LayerAdded = new TileLayerEventType("layeradded");
        public static TileLayerEventType LayerRemoved = new TileLayerEventType("layerremoved");
        public static TileLayerEventType MouseDown = new TileLayerEventType("mousedown");
        public static TileLayerEventType MouseEnter = new TileLayerEventType("mouseenter");
        public static TileLayerEventType MouseLeave = new TileLayerEventType("mouseleave");
        public static TileLayerEventType MouseMove = new TileLayerEventType("mousemove");
        public static TileLayerEventType MouseOut = new TileLayerEventType("mouseout");
        public static TileLayerEventType MouseOver = new TileLayerEventType("mouseover");
        public static TileLayerEventType MouseUp = new TileLayerEventType("mouseup");
        public static TileLayerEventType TouchCancel = new TileLayerEventType("touchcancel");
        public static TileLayerEventType TouchEnd = new TileLayerEventType("touchend");
        public static TileLayerEventType TouchMove = new TileLayerEventType("touchmove");
        public static TileLayerEventType TouchStart = new TileLayerEventType("touchstart");
        public static TileLayerEventType Wheel = new TileLayerEventType("wheel");

        internal TileLayerEventType(string atlasEvent) : base(atlasEvent)
        {
        }
    }
}
