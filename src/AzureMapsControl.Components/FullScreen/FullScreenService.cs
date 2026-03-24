
using System.Threading.Tasks;

using AzureMapsControl.Components.Logger;
using AzureMapsControl.Components.Runtime;

using Microsoft.Extensions.Logging;

namespace AzureMapsControl.Components.FullScreen;
internal sealed class FullScreenService(IMapJsRuntime jsRuntime, ILogger<FullScreenService> logger) : IFullScreenService
{
    public async ValueTask<bool> IsSupportedAsync()
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.FullScreenService_IsFullScreenSupportedAsync, "FullScreenService - IsSupportedAsync");
        return await jsRuntime.InvokeAsync<bool>(Constants.JsConstants.Methods.FullScreenControl.IsFullScreenSupported.ToFullScreenControlNamespace()).ConfigureAwait(false);
    }
}
