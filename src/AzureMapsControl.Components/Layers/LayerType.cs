namespace AzureMapsControl.Components.Layers
{
    internal sealed class LayerType
    {
        private readonly string _type;

        internal static LayerType TileLayer = new LayerType("tileLayer");

        private LayerType(string type) => _type = type;

        public override string ToString() => _type;
    }
}
