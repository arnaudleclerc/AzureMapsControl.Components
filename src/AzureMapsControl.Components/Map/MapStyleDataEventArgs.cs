
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Map;
[ExcludeFromCodeCoverage]
public sealed class MapStyleDataEventArgs : MapDataEventArgs
{
    /// <summary>
    /// Style of the map
    /// </summary>
    public MapStyle Style { get; }

    internal MapStyleDataEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs) => Style = MapStyle.FromString(eventArgs.Style);
}
