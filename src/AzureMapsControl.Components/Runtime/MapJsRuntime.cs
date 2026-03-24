
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace AzureMapsControl.Components.Runtime;
internal sealed class MapJsRuntime(IJSRuntime jsRuntime, ILogger<MapJsRuntime> logger) : IMapJsRuntime
{
    public async ValueTask InvokeVoidAsync(string identifier, params object[] args)
    {
        logger?.LogDebug($"MapJsRuntime - InvokeVoidAsync - {identifier}");
        await jsRuntime.InvokeVoidAsync(identifier, args).ConfigureAwait(false);
    }

    public async ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object[] args)
    {
        logger?.LogDebug($"MapJsRuntime - InvokeAsync - {identifier}");
        return await jsRuntime.InvokeAsync<TValue>(identifier, args).ConfigureAwait(false);
    }
}
