namespace AzureMapsControl.Components.Drawing
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Map;

    [ExcludeFromCodeCoverage]
    public sealed class DrawingToolbarModeEventArgs : MapEventArgs
    {
        public string NewMode { get; }

        internal DrawingToolbarModeEventArgs(Map map, DrawingToolbarJsEventArgs eventArgs) : base(map, eventArgs.Type) => NewMode = eventArgs.NewMode;
    }
}
