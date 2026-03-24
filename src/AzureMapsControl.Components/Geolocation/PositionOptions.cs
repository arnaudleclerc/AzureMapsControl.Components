
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Geolocation;
[ExcludeFromCodeCoverage]
public sealed class PositionOptions
{
    public bool? EnableHighAccuracy { get; set; }
    public int? MaximumAge { get; set; }
    public int? Timeout { get; set; }
}
