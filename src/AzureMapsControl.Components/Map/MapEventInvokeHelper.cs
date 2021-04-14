namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal sealed class MapEventInvokeHelper : EventInvokeHelper<MapJsEventArgs>
    {
        public MapEventInvokeHelper(Func<MapJsEventArgs, ValueTask> callback) : base(callback)
        {
        }
    }
}
