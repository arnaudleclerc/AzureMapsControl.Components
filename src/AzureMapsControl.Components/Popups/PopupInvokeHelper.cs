
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Popups;
internal sealed class PopupInvokeHelper : EventInvokeHelper<PopupEventArgs>
{
    public PopupInvokeHelper(Func<PopupEventArgs, ValueTask> callback) : base(callback) { }
}
