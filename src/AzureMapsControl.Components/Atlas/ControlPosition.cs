namespace AzureMapsControl.Components.Atlas
{
    /// <summary>
    /// Positions where the control can be placed on the map.
    /// </summary>
    public sealed class ControlPosition
    {
        private readonly string _position;

        /// <summary>
        /// Places the control in the bottom left of the map.
        /// </summary>
        public static readonly ControlPosition BottomLeft = new ControlPosition("bottom-left");

        /// <summary>
        /// Places the control in the bottom right of the map.
        /// </summary>
        public static readonly ControlPosition BottomRight = new ControlPosition("bottom-right");

        /// <summary>
        /// The control will place itself in its default location.
        /// </summary>
        public static readonly ControlPosition NonFixed = new ControlPosition("non-fixed");

        /// <summary>
        /// Places the control in the top left of the map.
        /// </summary>
        public static readonly ControlPosition TopLeft = new ControlPosition("top-left");

        /// <summary>
        /// Places the control in the top right of the map.
        /// </summary>
        public static readonly ControlPosition TopRight = new ControlPosition("top-right");

        private ControlPosition(string position) => _position = position;

        public override string ToString() => _position;
    }
}
