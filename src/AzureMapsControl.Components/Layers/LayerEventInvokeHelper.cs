namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;
    using AzureMapsControl.Components.Map;

    internal sealed class LayerEventInvokeHelper : EventInvokeHelper<MapJsEventArgs>
    {
        public LayerEventInvokeHelper(Func<MapJsEventArgs, Task> callback) : base(callback)
        {
        }
    }
}
