namespace AzureMapsControl.Components.Drawing
{
    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Map;

    public class DrawingToolbarEventArgs : MapEventArgs
    {
        public Feature Data { get; }

        internal DrawingToolbarEventArgs(Map map, DrawingToolbarJsEventArgs eventArgs) : base(map, eventArgs.Type) => Data = eventArgs.Data;
    }
}
