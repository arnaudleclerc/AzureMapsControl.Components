
using System.Collections.Generic;

using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Markers;
[ExcludeFromCodeCoverage]
internal class HtmlMarkerCreationOptions : HtmlMarkerUpdateOptions
{
    public IEnumerable<string> Events { get; set; }
}
