
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Map;

using AzureMap = AzureMapsControl.Components.Map.Map;

namespace AzureMapsControl.Components.Drawing;
[ExcludeFromCodeCoverage]
public sealed class DrawingToolbarModeEventArgs : MapEventArgs
{
    public string NewMode { get; }

    internal DrawingToolbarModeEventArgs(AzureMap map, DrawingToolbarJsEventArgs eventArgs) : base(map, eventArgs.Type) => NewMode = eventArgs.NewMode;
}
