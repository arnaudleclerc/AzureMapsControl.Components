namespace AzureMapsControl.Components.Drawing
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    public class DrawingToolbarUpdateOptions
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

        public static DrawingToolbarUpdateOptions FromOptions(DrawingToolbarUpdateOptions options)
        {
            return new DrawingToolbarUpdateOptions {
                Buttons = options.Buttons,
                ContainerId = options.ContainerId,
                NumColumns = options.NumColumns,
                Position = options.Position,
                Style = options.Style,
                Visible = options.Visible
            };
        }
    }
}
