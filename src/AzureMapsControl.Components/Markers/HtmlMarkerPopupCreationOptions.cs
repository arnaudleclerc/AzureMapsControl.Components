
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Popups;

namespace AzureMapsControl.Components.Markers;
[ExcludeFromCodeCoverage]
internal class HtmlMarkerPopupCreationOptions
{
    public string Id { get; set; }
    public IEnumerable<string> Events { get; set; }
    public PopupOptions Options { get; set; }
}
