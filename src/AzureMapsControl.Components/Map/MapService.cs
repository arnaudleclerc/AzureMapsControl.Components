namespace AzureMapsControl.Components.Map
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Logger;
    using Microsoft.Extensions.Logging;

    public delegate ValueTask MapReadyEvent();

    internal class MapService : IMapAdderService
    {
        private readonly ILogger<MapService> _logger;

        public Map Map
        {
            get;
            private set;
        }

        public MapService(ILogger<MapService> logger) => _logger = logger;

        public event MapReadyEvent OnMapReadyAsync;

        public async ValueTask AddMapAsync(Map map)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.MapService_AddMapAsync, "Adding instance of map");
            Map = map;

            if (OnMapReadyAsync != null)
            {
                _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.MapService_AddMapAsync, "Emitting OnMapReadyAsync");
                await OnMapReadyAsync.Invoke();
            }
        }

    }
}
