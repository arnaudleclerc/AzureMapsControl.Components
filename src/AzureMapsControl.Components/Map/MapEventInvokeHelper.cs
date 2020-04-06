namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal class MapEventInvokeHelper : EventInvokeHelper<MapJsEventArgs>
    {
        public MapEventInvokeHelper(Func<MapJsEventArgs, Task> callback) : base(callback)
        {
        }
    }
}
