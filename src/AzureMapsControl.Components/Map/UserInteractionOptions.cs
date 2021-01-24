namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class UserInteractionOptions
    {
        /// <summary>
        /// Whether the Shift + left click and drag will draw a zoom box.
        /// </summary>
        public bool? BoxZoomInteraction { get; set; }

        /// <summary>
        /// Whether double left click will zoom the map inwards.
        /// </summary>
        public bool? DblclickZoomInteraction { get; set; }

        /// <summary>
        /// Whether left click and drag will pan the map.
        /// </summary>
        public bool? DragPanInteraction { get; set; }

        /// <summary>
        /// Whether right click and drag will rotate and pitch the map.
        /// </summary>
        public bool? DragRotateInteraction { get; set; }

        /// <summary>
        /// Whether the map is interactive or static. If false, all user interaction is disabled. If true, only selected user interactions will enabled.
        /// </summary>
        public bool? Interactive { get; set; }

        /// <summary>
        /// Whether the keyboard interactions are enabled.
        /// </summary>
        public bool? KeyboardInteraction { get; set; }

        /// <summary>
        /// Whether the map should zoom on scroll input.
        /// </summary>
        public bool? ScrollZoomInteraction { get; set; }

        /// <summary>
        /// Whether touch interactions are enabled for touch devices.
        /// </summary>
        public bool? TouchInteraction { get; set; }

        /// <summary>
        /// Sets the zoom rate of the mouse wheel
        /// </summary>
        public double? WheelZoomRate { get; set; }
    }
}
