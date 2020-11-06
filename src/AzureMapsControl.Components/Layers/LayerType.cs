namespace AzureMapsControl.Components.Layers
{
    internal sealed class LayerType
    {
        private readonly string _type;

        internal static LayerType BubbleLayer = new LayerType("bubbleLayer");
        internal static LayerType HeatmapLayer = new LayerType("heatmapLayer");
        internal static LayerType ImageLayer = new LayerType("imageLayer");
        internal static LayerType LineLayer = new LayerType("lineLayer");
        internal static LayerType PolygonExtrusionLayer = new LayerType("polygonExtrusionLayer");
        internal static LayerType PolygonLayer = new LayerType("polygonLayer");
        internal static LayerType SymbolLayer = new LayerType("symbolLayer");
        internal static LayerType TileLayer = new LayerType("tileLayer");

        private LayerType(string type) => _type = type;

        public override string ToString() => _type;
    }
}
