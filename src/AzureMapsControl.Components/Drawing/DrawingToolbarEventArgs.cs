namespace AzureMapsControl.Components.Drawing
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Map;

    [ExcludeFromCodeCoverage]
    public sealed class DrawingToolbarEventArgs : MapEventArgs
    {
        public Feature<Geometry> Data { get; }

        internal DrawingToolbarEventArgs(Map map, DrawingToolbarJsEventArgs eventArgs) : base(map, eventArgs.Type) => Data = eventArgs.Data;
    }
}
