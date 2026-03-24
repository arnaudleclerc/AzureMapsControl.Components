
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Atlas;

namespace AzureMapsControl.Components.Geolocation;
[ExcludeFromCodeCoverage]
internal record GeolocationJsEventArgs
{
    public int? Code { get; init; }
    public string Message { get; init; }
    public Feature<Point> Feature { get; init; }
    public string Type { get; init; }
}
