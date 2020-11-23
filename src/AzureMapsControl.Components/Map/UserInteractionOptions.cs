namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class UserInteractionOptions
    {
        /// <summary>
        /// Whether the Shift + left click and drag will draw a zoom box. default true
        /// </summary>
        public bool BoxZoomInteraction { get; set; } = true;

        /// <summary>
        /// Whether double left click will zoom the map inwards. Default true
        /// </summary>
        public bool DblclickZoomInteraction { get; set; } = true;

        /// <summary>
        /// Whether left click and drag will pan the map. default true
        /// </summary>
        public bool DragPanInteraction { get; set; } = true;

        /// <summary>
        /// Whether right click and drag will rotate and pitch the map. default true
        /// </summary>
        public bool DragRotateInteraction { get; set; } = true;

        /// <summary>
        /// Whether the map is interactive or static. If false, all user interaction is disabled. If true, only selected user interactions will enabled. default true
        /// </summary>
        public bool Interactive { get; set; } = true;

        /// <summary>
        /// Whether the keyboard interactions are enabled. Default true
        /// </summary>
        public bool KeyboardInteraction { get; set; } = true;

        /// <summary>
        /// Whether the map should zoom on scroll input. default true
        /// </summary>
        public bool ScrollZoomInteraction { get; set; } = true;

        /// <summary>
        /// Whether touch interactions are enabled for touch devices. default true
        /// </summary>
        public bool TouchInteraction { get; set; } = true;

        /// <summary>
        /// Sets the zoom rate of the mouse wheel default 1/450
        /// </summary>
        public double WheelZoomRate { get; set; }
    }
}
