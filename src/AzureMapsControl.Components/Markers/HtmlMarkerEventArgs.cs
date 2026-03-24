
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Map;

using AzureMap = AzureMapsControl.Components.Map.Map;

namespace AzureMapsControl.Components.Markers;
[ExcludeFromCodeCoverage]
public sealed class HtmlMarkerEventArgs : MapEventArgs
{
    public HtmlMarker HtmlMarker { get; }
    internal HtmlMarkerEventArgs(AzureMap map, string type, HtmlMarker htmlMarker) : base(map, type) => HtmlMarker = htmlMarker;
}
