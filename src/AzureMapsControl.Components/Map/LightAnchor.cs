namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct LightAnchor
    {
        private readonly string _anchor;

        public static readonly LightAnchor Map = new LightAnchor("map");
        public static readonly LightAnchor Viewport = new LightAnchor("viewport");

        private LightAnchor(string anchor) => _anchor = anchor;

        public override string ToString() => _anchor;
    }
}
