namespace AzureMapsControl.Components.Markers
{
    /// <summary>
    /// Indicates the marker's location relative to its position on the map.
    /// </summary>
    public sealed class MarkerAnchor
    {
        private readonly string _anchor;

        public static MarkerAnchor Bottom = new MarkerAnchor("bottom");
        public static MarkerAnchor BottomLeft = new MarkerAnchor("bottom-left");
        public static MarkerAnchor BottomRight = new MarkerAnchor("bottom-right");
        public static MarkerAnchor Center = new MarkerAnchor("center");
        public static MarkerAnchor Left = new MarkerAnchor("left");
        public static MarkerAnchor Right = new MarkerAnchor("right");
        public static MarkerAnchor Top = new MarkerAnchor("top");
        public static MarkerAnchor TopLeft = new MarkerAnchor("top-left");
        public static MarkerAnchor TopRight = new MarkerAnchor("top-right");

        private MarkerAnchor(string anchor) => _anchor = anchor;

        public override string ToString() => _anchor;
    }
}
