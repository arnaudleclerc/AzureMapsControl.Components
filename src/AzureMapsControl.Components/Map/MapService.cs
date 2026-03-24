
using System.Threading.Tasks;

using AzureMapsControl.Components.Logger;

using Microsoft.Extensions.Logging;

namespace AzureMapsControl.Components.Map;
public delegate ValueTask MapReadyEvent();

internal sealed class MapService(ILogger<MapService> logger) : IMapAdderService
{
    public Map Map
    {
        get;
        private set;
    }

    public event MapReadyEvent OnMapReadyAsync;

    public async ValueTask AddMapAsync(Map map)
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.MapService_AddMapAsync, "Adding instance of map");
        Map = map;

        if (OnMapReadyAsync is not null)
        {
            logger?.LogAzureMapsControlInfo(AzureMapLogEvent.MapService_AddMapAsync, "Emitting OnMapReadyAsync");
            await OnMapReadyAsync.Invoke().ConfigureAwait(false);
        }
    }

}
