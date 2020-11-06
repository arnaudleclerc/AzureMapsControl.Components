namespace AzureMapsControl.Components.Markers
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    public sealed class HtmlMarkerOptions
    {
        /// <summary>
        /// Indicates the marker's location relative to its position on the map.
        /// </summary>
        public MarkerAnchor Anchor { get; set; }

        /// <summary>
        /// A color value that replaces any {color} placeholder property that has been included in a string htmlContent.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Indicates if the user can drag the position of the marker using the mouse or touch controls.
        /// </summary>
        public bool? Draggable { get; set; }

        /// <summary>
        /// The HTML content of the marker.
        /// </summary>
        public string HtmlContent { get; set; }

        /// <summary>
        /// An offset in pixels to move the popup relative to the markers center.
        /// Negatives indicate left and up.
        /// </summary>
        public Pixel PixelOffset { get; set; }

        /// <summary>
        /// The position of the marker.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// A color value that replaces any {secondaryColor} placeholder property that has been included in a string htmlContent.
        /// </summary>
        public string SecondaryColor { get; set; }

        /// <summary>
        /// A string of text that replaces any {text} placeholder property that has been included in a string htmlContent.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Specifies if the marker is visible or not.
        /// </summary>
        public bool? Visible { get; set; }
    }
}
