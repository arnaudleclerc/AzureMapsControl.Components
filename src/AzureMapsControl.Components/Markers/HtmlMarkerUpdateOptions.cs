
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Markers;
[ExcludeFromCodeCoverage]
internal class HtmlMarkerUpdateOptions
{
    public string Id { get; set; }
    public HtmlMarkerOptions Options { get; set; }
    public HtmlMarkerPopupCreationOptions PopupOptions { get; set; }
}
