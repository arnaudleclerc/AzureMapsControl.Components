namespace AzureMapsControl.Components.Map
{
    internal class UserInteractionOptions
    {
        public bool BoxZoomInteraction { get; set; }
        public bool DblclickZoomInteraction { get; set; }
        public bool DragPanInteraction { get; set; }
        public bool DragRotateInteraction { get; set; }
        public bool Interactive { get; set; }
        public bool KeyboardInteraction { get; set; }
        public bool ScrollZoomInteraction { get; set; }
        public bool TouchInteraction { get; set; }
        public double WheelZoomRate { get; set; }
    }
}
