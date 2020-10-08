namespace AzureMapsControl.Components.Drawing
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Markers;

    /// <summary>
    /// Options for the DrawingToolbar
    /// </summary>
    public sealed class DrawingToolbarOptions
    {
        /// <summary>
        /// An array of buttons to display in the toolbar. The order of this array will match the order of the buttons in the toolbar.
        /// </summary>
        public IEnumerable<DrawingButton> Buttons { get; set; }

        /// <summary>
        /// The id of a HTML element in which the toolbar should be added to. If not specified, the toolbar will be added to the map.
        /// </summary>
        public string ContainerId { get; set; }

        /// <summary>
        /// The number of columns to display the buttons with. If the number of columns is greater than or equal to the number of buttons the toolbar will be a single horizontal row. If only one column is used the toolbar will be a single vertical column
        /// </summary>
        public int? NumColumns { get; set; }

        /// <summary>
        /// If the toolbar is added to the map, this value will specify where on the map the toolbar control will be added.
        /// </summary>
        public ControlPosition Position { get; set; } = ControlPosition.NonFixed;

        /// <summary>
        /// The style of the DrawingToolbar.
        /// </summary>
        public DrawingToolbarStyle Style { get; set; } = DrawingToolbarStyle.Light;

        /// <summary>
        /// Specifies if the toolbar is visible or not.
        /// </summary>
        public bool Visible { get; set; } = true;

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
