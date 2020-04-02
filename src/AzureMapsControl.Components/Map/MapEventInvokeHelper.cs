namespace AzureMapsControl.Components.Map
{
    using System;
    using Microsoft.JSInterop;

    internal class MapEventInvokeHelper
    {
        private readonly Action<MapJsEventArgs> _action;

        public MapEventInvokeHelper(Action<MapJsEventArgs> action) => _action = action;

        [JSInvokable]
        public void NotifyMapEvent(MapJsEventArgs mapEvent) => _action.Invoke(mapEvent);
    }
}
