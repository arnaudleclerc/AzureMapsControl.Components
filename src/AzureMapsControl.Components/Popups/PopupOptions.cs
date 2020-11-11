namespace AzureMapsControl.Components.Popups
{
    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// The options for a popup.
    /// </summary>
    public sealed class PopupOptions
    {
        /// <summary>
        /// Specifies if the popup can be dragged away from its position.
        /// </summary>
        public bool? Draggable { get; set; }

        /// <summary>
        /// Specifies if the close button should be displayed in the popup or not.
        /// </summary>
        public bool? CloseButton { get; set; }

        /// <summary>
        /// The content to display within the popup.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Specifies the fill color of the popup.
        /// </summary>
        public string FillColor { get; set; }

        public Pixel PixelOffset { get; set; }

        /// <summary>
        /// The position on the map where the popup should be anchored.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Specifies if the pointer should be displayed in the popup or not.
        /// </summary>
        public bool? ShowPointer { get; set; }

        /// <summary>
        /// Specifices if the popup should be opened when added to the map
        /// </summary>
        public bool? OpenOnAdd { get; set; }
    }
}
