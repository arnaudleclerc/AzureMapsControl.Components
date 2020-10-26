namespace AzureMapsControl.Components.Drawing
{
    using AzureMapsControl.Components.Atlas;

    internal class DrawingToolbarJsEventArgs
    {
        public string Type { get; set; }
        public string NewMode { get; set; }
        public Feature Data { get; set; }
    }
}
