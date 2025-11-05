namespace AzureMapsControl.Components.Map
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Logger;
    using Microsoft.Extensions.Logging;

    public delegate ValueTask MapReadyEvent(Map map);

    internal class MapService : IMapAdderService
    {
        private readonly ILogger<MapService> _logger;
        private readonly ConcurrentDictionary<string, Map> _maps = new();
        private string _latestMapId;

        public IReadOnlyDictionary<string, Map> Maps => _maps;

        // Backward compatibility: return the most recently added map or first available map
        public Map Map 
        { 
            get
            {
                if (!string.IsNullOrEmpty(_latestMapId) && _maps.TryGetValue(_latestMapId, out var map))
                {
                    return map;
                }
                
                if (!_maps.IsEmpty)
                {
                    return _maps.Values.First();
                }
                
                return null;
            }
        }

        public MapService(ILogger<MapService> logger) => _logger = logger;

        public event MapReadyEvent OnMapReadyAsync;

        public Map GetMap(string mapId) => _maps.TryGetValue(mapId, out var map) ? map : null;

        public async ValueTask AddMapAsync(Map map)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.MapService_AddMapAsync, "Adding instance of map");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.MapService_AddMapAsync, $"Map ID: {map.Id}");
            
            _maps.AddOrUpdate(map.Id, map, (key, oldValue) => map);
            _latestMapId = map.Id; // Track the most recently added map

            if (OnMapReadyAsync != null)
            {
                _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.MapService_AddMapAsync, "Emitting OnMapReadyAsync");
                await OnMapReadyAsync.Invoke(map);
            }
        }

        public async ValueTask RemoveMapAsync(string mapId)
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.MapService_AddMapAsync, "Removing map instance");
            _logger?.LogAzureMapsControlDebug(AzureMapLogEvent.MapService_AddMapAsync, $"Map ID: {mapId}");
            
            if (_maps.TryRemove(mapId, out _))
            {
                // If we're removing the latest map, update the latest map ID
                if (_latestMapId == mapId)
                {
                    _latestMapId = _maps.Keys.FirstOrDefault();
                }
                
                _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.MapService_AddMapAsync, "Map removed successfully");
            }
            
            await ValueTask.CompletedTask;
        }
    }
}
