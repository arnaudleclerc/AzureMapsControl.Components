
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;
using AzureMapsControl.Components.Map;

namespace AzureMapsControl.Components.Layers;
internal sealed class LayerEventInvokeHelper : EventInvokeHelper<MapJsEventArgs>
{
    public LayerEventInvokeHelper(Func<MapJsEventArgs, ValueTask> callback) : base(callback)
    {
    }
}
