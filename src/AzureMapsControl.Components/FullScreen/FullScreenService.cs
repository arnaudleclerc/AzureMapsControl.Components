namespace AzureMapsControl.Components.FullScreen
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    internal class FullScreenService : IFullScreenService
    {
        private readonly IMapJsRuntime _jsRuntime;
        private readonly ILogger<FullScreenService> _logger;

        public FullScreenService(IMapJsRuntime jsRuntime, ILogger<FullScreenService> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        public async ValueTask<bool> IsSupportedAsync()
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.FullScreenService_IsFullScreenSupportedAsync, "FullScreenService - IsSupportedAsync");
            return await _jsRuntime.InvokeAsync<bool>(Constants.JsConstants.Methods.FullScreenControl.IsFullScreenSupported.ToFullScreenControlNamespace());
        }
    }
}
