
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.FullScreen;
internal class FullScreenEventInvokeHelper : EventInvokeHelper<bool>
{
    public FullScreenEventInvokeHelper(Func<bool, ValueTask> callback) : base(callback)
    {
    }
}
