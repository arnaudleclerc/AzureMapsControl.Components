
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Markers;
[ExcludeFromCodeCoverage]
internal record HtmlMarkerJsEventArgs
{
    public string MarkerId { get; init; }
    public string Type { get; init; }
    public HtmlMarkerOptions Options { get; init; }
}
