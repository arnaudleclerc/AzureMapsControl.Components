namespace AzureMapsControl.Components.Drawing
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    internal class DrawingToolbarJsEventArgs
    {
        public string Type { get; set; }
        public string NewMode { get; set; }
        public Feature<Geometry> Data { get; set; }
    }
}
