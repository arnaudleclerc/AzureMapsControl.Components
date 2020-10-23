namespace AzureMapsControl.Components.Drawing
{
    using AzureMapsControl.Components.Markers;

    /// <summary>
    /// Options for the DrawingToolbar
    /// </summary>
    public sealed class DrawingToolbarOptions : DrawingToolbarUpdateOptions
    {
        /// <summary>
        /// The style options for the primary drag handles
        /// </summary>
        public HtmlMarkerOptions DragHandleStyle { get; set; }

        /// <summary>
        /// Specifies the number of pixels the mouse or touch must move before another coordinate is added to a shape when in "freehand" or "hybrid" drawing modes.
        /// </summary>
        public int FreehandInterval { get; set; } = 3;

        /// <summary>
        /// The type of drawing interaction the manager should adhere to.
        /// </summary>
        public DrawingInteractionType InteractionType { get; set; } = DrawingInteractionType.Hybrid;

        /// <summary>
        /// The drawing mode the manager is in.
        /// </summary>
        public DrawingMode Mode { get; set; } = DrawingMode.Idle;

        /// <summary>
        /// The style options for the secondary drag handles. These provide handles at mid-points for creating new coordinates between existing coordinates.
        /// </summary>
        public HtmlMarkerOptions SecondaryDragHandleStyle { get; set; }

        /// <summary>
        /// Specifies if shapes can be dragged when in edit or select mode.
        /// </summary>
        public bool ShapesDraggingEnabled { get; set; } = true;

    }
}
