
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Atlas;

namespace AzureMapsControl.Components.Drawing;
[ExcludeFromCodeCoverage]
internal record DrawingToolbarJsEventArgs
{
    public string Type { get; init; }
    public string NewMode { get; init; }
    public Feature<Geometry> Data { get; init; }
}
