namespace AzureMapsControl.Components.Layers
{
    internal sealed class LayerType
    {
        private readonly string _type;

        internal static LayerType BubbleLayer = new LayerType("bubbleLayer");
        internal static LayerType HeatmapLayer = new LayerType("heatmapLayer");
        internal static LayerType ImageLayer = new LayerType("imageLayer");
        internal static LayerType TileLayer = new LayerType("tileLayer");

        private LayerType(string type) => _type = type;

        public override string ToString() => _type;
    }
}
