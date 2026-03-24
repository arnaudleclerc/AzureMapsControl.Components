
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Markers;
[ExcludeFromCodeCoverage]
internal class HtmlMarkerJsEventArgs
{
    public string MarkerId { get; set; }
    public string Type { get; set; }
    public HtmlMarkerOptions Options { get; set; }
}
