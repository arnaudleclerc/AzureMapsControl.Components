namespace AzureMapsControl.Map
{
    public sealed class LightAnchor
    {
        private readonly string _anchor;

        public static LightAnchor Map = new LightAnchor("map");
        public static LightAnchor Viewport = new LightAnchor("viewport");

        private LightAnchor(string anchor) => _anchor = anchor;

        public override string ToString() => _anchor;
    }
}
