namespace AzureMapsControl.Components.Controls
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Markers;

    [ExcludeFromCodeCoverage]
    public sealed class OverviewMapControlOptions : IControlOptions
    {
        /// <summary>
        /// The height of the overview map in pixels.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Specifies if the overview map is interactive.
        /// </summary>
        public bool? Interactive { get; set; }

        /// <summary>
        /// The name of the style to use when rendering the map.
        /// </summary>
        public MapStyle MapStyle { get; set; }

        /// <summary>
        /// Options for customizing the marker overlay. If the draggable option of the marker it enabled, the map will center over the marker location after it has been dragged to a new location.
        /// </summary>
        public HtmlMarkerOptions MarkerOptions { get; set; }

        /// <summary>
        /// Specifies if the overview map is minimized or not
        /// </summary>
        public bool? Minimized { get; set; }

        /// <summary>
        /// Specifies the type of information to overlay on top of the map.
        /// </summary>
        public OverviewMapControlOverlay Overlay { get; set; }

        /// <summary>
        /// Specifies if a toggle button for minimizing the overview map should be displayed or not.
        /// </summary>
        public bool? ShowToggle { get; set; }

        /// <summary>
        /// The style of the control. Can be an instance of ControlStyle, or any CSS3 color.
        /// </summary>
        public Either<ControlStyle, string> Style { get; set; }

        /// <summary>
        /// Specifies if bearing and pitch changes should be synchronized.
        /// </summary>
        public bool? SyncBearingPitch { get; set; }

        /// <summary>
        /// Specifies if zoom level changes should be synchronized.
        /// </summary>
        public bool? SyncZoom { get; set; }

        /// <summary>
        /// Specifies if the overview map control is visible or not
        /// </summary>
        public bool? Visible { get; set; }

        /// <summary>
        /// The width of the overview map in pixels
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Zoom level to set on overview map when not synchronizing zoom level changes.
        /// </summary>
        public double? Zoom { get; set; }

        /// <summary>
        /// The number of zoom levels to offset from the parent map zoom level when synchronizing zoom level changes.
        /// </summary>
        public int? ZoomOffset { get; set; }
    }
}
