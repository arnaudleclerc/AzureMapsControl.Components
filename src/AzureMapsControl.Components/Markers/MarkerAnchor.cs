namespace AzureMapsControl.Components.Markers
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Indicates the marker's location relative to its position on the map.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class MarkerAnchor
    {
        private readonly string _anchor;

        public static readonly MarkerAnchor Bottom = new MarkerAnchor("bottom");
        public static readonly MarkerAnchor BottomLeft = new MarkerAnchor("bottom-left");
        public static readonly MarkerAnchor BottomRight = new MarkerAnchor("bottom-right");
        public static readonly MarkerAnchor Center = new MarkerAnchor("center");
        public static readonly MarkerAnchor Left = new MarkerAnchor("left");
        public static readonly MarkerAnchor Right = new MarkerAnchor("right");
        public static readonly MarkerAnchor Top = new MarkerAnchor("top");
        public static readonly MarkerAnchor TopLeft = new MarkerAnchor("top-left");
        public static readonly MarkerAnchor TopRight = new MarkerAnchor("top-right");

        private MarkerAnchor(string anchor) => _anchor = anchor;

        public override string ToString() => _anchor;
    }
}
