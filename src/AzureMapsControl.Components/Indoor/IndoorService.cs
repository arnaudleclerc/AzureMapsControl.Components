namespace AzureMapsControl.Components.Indoor
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    internal class IndoorService : IIndoorService
    {
        private readonly IMapJsRuntime _jsRuntime;
        private readonly ILogger<IndoorService> _logger;
        private readonly IMapService _mapService;

        public IndoorService(IMapJsRuntime jsRuntime, ILogger<IndoorService> logger, IMapService mapService)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
            _mapService = mapService;
        }

        public async ValueTask<IndoorManager> CreateIndoorManagerAsync(string mapId, IndoorManagerOptions options) => await CreateIndoorManagerAsync(mapId, options, null);

        public async ValueTask<IndoorManager> CreateIndoorManagerAsync(string mapId, IndoorManagerOptions options, IndoorManagerEventActivationFlags eventFlags)
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "IndoorService - CreateIndoorManagerAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "MapId", mapId);
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "Options", options);
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "EventFlags", eventFlags);

            var indoorManager = new IndoorManager(_jsRuntime, _logger) {
                MapId = mapId
            };

            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), mapId, indoorManager.Id, options, eventFlags?.EnabledEvents, DotNetObjectReference.Create(indoorManager.EventHelper));
            return indoorManager;
        }

        // Legacy methods for backward compatibility
        [System.Obsolete("Use CreateIndoorManagerAsync(mapId, options) instead for multi-map support")]
        public async ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options) {
            // Get the current map ID from MapService instead of using hardcoded "default"
            var mapId = _mapService.Map?.Id ?? "default";
            return await CreateIndoorManagerAsync(mapId, options, null);
        }

        [System.Obsolete("Use CreateIndoorManagerAsync(mapId, options, eventFlags) instead for multi-map support")]
        public async ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options, IndoorManagerEventActivationFlags eventFlags) {
            // Get the current map ID from MapService instead of using hardcoded "default"
            var mapId = _mapService.Map?.Id ?? "default";
            return await CreateIndoorManagerAsync(mapId, options, eventFlags); 
        }
    }
}
