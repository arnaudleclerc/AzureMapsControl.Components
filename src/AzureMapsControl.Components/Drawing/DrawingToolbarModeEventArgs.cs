namespace AzureMapsControl.Components.Drawing
{
    using AzureMapsControl.Components.Map;

    public class DrawingToolbarModeEventArgs : MapEventArgs
    {
        public string NewMode { get; }

        internal DrawingToolbarModeEventArgs(Map map, DrawingToolbarJsEventArgs eventArgs) : base(map, eventArgs.Type) => NewMode = eventArgs.NewMode;
    }
}
