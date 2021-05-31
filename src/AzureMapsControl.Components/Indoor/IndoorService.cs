namespace AzureMapsControl.Components.Indoor
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    internal class IndoorService : IIndoorService
    {
        private readonly IMapJsRuntime _jsRuntime;
        private readonly ILogger<IndoorService> _logger;

        public IndoorService(IMapJsRuntime jsRuntime, ILogger<IndoorService> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        public async ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options) => await CreateIndoorManagerAsync(options, null);

        public async ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options, IndoorManagerEventActivationFlags eventFlags)
        {
            _logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "IndoorService - CreateIndoorManagerAsync");
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "Options", options);
            _logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "EventFlags", eventFlags);

            var indoorManager = new IndoorManager(_jsRuntime, _logger);
            await _jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), indoorManager.Id, options, eventFlags?.EnabledEvents, DotNetObjectReference.Create(indoorManager.EventHelper));
            return indoorManager;
        }
    }
}
