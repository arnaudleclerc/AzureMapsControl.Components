namespace AzureMapsControl.Components.Controls
{
    /// <summary>
    /// Available styles for a control
    /// </summary>
    public struct ControlStyle
    {
        private readonly string _style;

        /// <summary>
        /// The control will be in the light style.
        /// </summary>
        public static readonly ControlStyle Light = new ControlStyle("light");

        /// <summary>
        /// The control will be in the dark style.
        /// </summary>
        public static readonly ControlStyle Dark = new ControlStyle("dark");

        /// <summary>
        /// The control will automatically switch styles based on the style of the map.
        /// If a control doesn't support automatic styling the light style will be used by default.
        /// </summary>
        public static readonly ControlStyle Auto = new ControlStyle("auto");

        private ControlStyle(string style) => _style = style;

        public override string ToString() => _style;
    }
}
