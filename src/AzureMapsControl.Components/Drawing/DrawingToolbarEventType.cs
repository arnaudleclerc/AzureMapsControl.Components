namespace AzureMapsControl.Components.Drawing
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class DrawingToolbarEventType : AtlasEventType
    {
        public static readonly DrawingToolbarEventType DrawingChanged = new DrawingToolbarEventType("drawingchanged");
        public static readonly DrawingToolbarEventType DrawingChanging = new DrawingToolbarEventType("drawingchanging");
        public static readonly DrawingToolbarEventType DrawingComplete = new DrawingToolbarEventType("drawingcomplete");
        public static readonly DrawingToolbarEventType DrawingModeChanged = new DrawingToolbarEventType("drawingmodechanged");
        public static readonly DrawingToolbarEventType DrawingStarted = new DrawingToolbarEventType("drawingstarted");

        private DrawingToolbarEventType(string type) : base(type) { }

    }
}
