namespace AzureMapsControl.Components.Runtime
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    internal class MapJsRuntime : IMapJsRuntime
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<MapJsRuntime> _logger;

        public MapJsRuntime(IJSRuntime jsRuntime, ILogger<MapJsRuntime> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        public async ValueTask InvokeVoidAsync(string identifier, params object[] args)
        {
            _logger?.LogDebug($"MapJsRuntime - InvokeVoidAsync - {identifier}");
            await _jsRuntime.InvokeVoidAsync(identifier, args);
        }

        public async ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object[] args)
        {
            _logger?.LogDebug($"MapJsRuntime - InvokeAsync - {identifier}");
            return await _jsRuntime.InvokeAsync<TValue>(identifier, args);
        }
    }
}
