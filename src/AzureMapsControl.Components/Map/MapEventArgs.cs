
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Map;
[ExcludeFromCodeCoverage]
public class MapEventArgs
{
    public Map Map { get; }
    public string Type { get; }

    internal MapEventArgs(Map map, string type)
    {
        Map = map;
        Type = type;
    }
}
