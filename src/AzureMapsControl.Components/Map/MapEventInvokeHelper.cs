
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Map;
internal sealed class MapEventInvokeHelper : EventInvokeHelper<MapJsEventArgs>
{
    public MapEventInvokeHelper(Func<MapJsEventArgs, ValueTask> callback) : base(callback)
    {
    }
}
