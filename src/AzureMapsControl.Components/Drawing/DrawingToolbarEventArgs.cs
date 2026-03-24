
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Atlas;
using AzureMapsControl.Components.Map;

using AzureMap = AzureMapsControl.Components.Map.Map;

namespace AzureMapsControl.Components.Drawing;
[ExcludeFromCodeCoverage]
public sealed class DrawingToolbarEventArgs : MapEventArgs
{
    public Feature<Geometry> Data { get; }

    internal DrawingToolbarEventArgs(AzureMap map, DrawingToolbarJsEventArgs eventArgs) : base(map, eventArgs.Type) => Data = eventArgs.Data;
}
