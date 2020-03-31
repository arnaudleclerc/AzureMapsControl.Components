namespace AzureMapsControl.Map
{
    using System;
    using Microsoft.JSInterop;

    internal class MapEventInvokeHelper
    {
        private readonly Action<MapEventArgs> _action;

        public MapEventInvokeHelper(Action<MapEventArgs> action) => _action = action;

        [JSInvokable]
        public void NotifyMapEvent(MapEventArgs mapEvent) => _action.Invoke(mapEvent);
    }
}
