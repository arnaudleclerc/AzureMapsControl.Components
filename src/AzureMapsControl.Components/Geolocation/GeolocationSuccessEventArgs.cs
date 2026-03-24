
using System.Diagnostics.CodeAnalysis;

using AzureMapsControl.Components.Atlas;

namespace AzureMapsControl.Components.Geolocation;
[ExcludeFromCodeCoverage]
public sealed class GeolocationSuccessEventArgs
{
    public Feature<Point> Feature { get; }
    internal GeolocationSuccessEventArgs(GeolocationJsEventArgs eventArgs) => Feature = eventArgs.Feature;
}
