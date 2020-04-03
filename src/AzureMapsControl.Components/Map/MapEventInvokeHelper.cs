namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.JSInterop;

    internal class MapEventInvokeHelper
    {
        private readonly Func<MapJsEventArgs, Task> _action;

        public MapEventInvokeHelper(Func<MapJsEventArgs, Task> action) => _action = action;

        [JSInvokable]
        public async Task NotifyMapEventAsync(MapJsEventArgs mapEvent) => await _action.Invoke(mapEvent);
    }
}
