
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Atlas;

namespace AzureMapsControl.Components.Drawing;
[ExcludeFromCodeCoverage]
internal class DrawingToolbarJsEventArgs
{
    public string Type { get; set; }
    public string NewMode { get; set; }
    public Feature<Geometry> Data { get; set; }
}
