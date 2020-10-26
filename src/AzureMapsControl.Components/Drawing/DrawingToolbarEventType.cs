namespace AzureMapsControl.Components.Drawing
{
    using AzureMapsControl.Components.Events;

    public sealed class DrawingToolbarEventType : AtlasEventType
    {
        public static DrawingToolbarEventType DrawingChanged = new DrawingToolbarEventType("drawingchanged");
        public static DrawingToolbarEventType DrawingChanging = new DrawingToolbarEventType("drawingchanging");
        public static DrawingToolbarEventType DrawingComplete = new DrawingToolbarEventType("drawingcomplete");
        public static DrawingToolbarEventType DrawingModeChanged = new DrawingToolbarEventType("drawingmodechanged");
        public static DrawingToolbarEventType DrawingStarted = new DrawingToolbarEventType("drawingstarted");

        private DrawingToolbarEventType(string type) : base(type) { }

    }
}
