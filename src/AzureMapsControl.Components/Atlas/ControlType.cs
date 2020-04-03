namespace AzureMapsControl.Components.Atlas
{
    public sealed class ControlType
    {
        private readonly string _type;

        /// <summary>
        /// A control for changing the rotation of the map.
        /// </summary>
        public static ControlType Compass = new ControlType("compass");

        /// <summary>
        /// A control for changing the pitch of the map.
        /// </summary>
        public static ControlType Pitch = new ControlType("pitch");

        /// <summary>
        /// A control for changing the style of the map.
        /// </summary>
        public static ControlType Style = new ControlType("style");

        /// <summary>
        /// A control for changing the zoom of the map.
        /// </summary>
        public static ControlType Zoom = new ControlType("zoom");

        private ControlType(string type) => _type = type;

        public override string ToString() => _type;
    }
}
