namespace AzureMapsControl.Components.Runtime
{
    using System.Threading.Tasks;

    using Microsoft.JSInterop;

    internal class MapJsRuntime : IMapJsRuntime
    {
        private readonly IJSRuntime _jsRuntime;

        public MapJsRuntime(IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;

        public async ValueTask InvokeVoidAsync(string identifier, params object[] args) => await _jsRuntime.InvokeVoidAsync(identifier, args);
    }
}
