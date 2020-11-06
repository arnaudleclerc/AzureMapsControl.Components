namespace AzureMapsControl.Components.Drawing
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Markers;

    [ExcludeFromCodeCoverage]
    internal sealed class DrawingToolbarCreationOptions
    {
        public string[] Buttons { get; set; }
        public string ContainerId { get; set; }
        public int? NumColumns { get; set; }
        public string Position { get; set; }
        public string Style { get; set; }
        public bool Visible { get; set; }

        public HtmlMarkerOptions DragHandleStyle { get; set; }
        public int FreehandInterval { get; set; }
        public string InteractionType { get; set; }
        public string Mode { get; set; }
        public HtmlMarkerOptions SecondaryDragHandleStyle { get; set; }
        public bool ShapeDraggingEnabled { get; set; }
        public IEnumerable<string> Events { get; set; }
    }
}
