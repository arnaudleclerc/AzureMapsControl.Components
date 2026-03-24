
using System.Threading.Tasks;

using AzureMapsControl.Components.Logger;
using AzureMapsControl.Components.Runtime;

using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace AzureMapsControl.Components.Indoor;
internal sealed class IndoorService(IMapJsRuntime jsRuntime, ILogger<IndoorService> logger) : IIndoorService
{
    public async ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options) => await CreateIndoorManagerAsync(options, null).ConfigureAwait(false);

    public async ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options, IndoorManagerEventActivationFlags eventFlags)
    {
        logger.LogAzureMapsControlInfo(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "IndoorService - CreateIndoorManagerAsync");
        logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "Options", options);
        logger.LogAzureMapsControlDebug(AzureMapLogEvent.IndoorService_CreateIndoorManagerAsync, "EventFlags", eventFlags);

        var indoorManager = new IndoorManager(jsRuntime, logger);
        await jsRuntime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), indoorManager.Id, options, eventFlags?.EnabledEvents, DotNetObjectReference.Create(indoorManager.EventHelper)).ConfigureAwait(false);
        return indoorManager;
    }
}
