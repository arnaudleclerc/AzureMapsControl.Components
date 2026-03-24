
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Indoor;
internal class IndoorManagerEventHelper : EventInvokeHelper<IndoorManagerJsEventArgs>
{
    public IndoorManagerEventHelper(Func<IndoorManagerJsEventArgs, ValueTask> callback) : base(callback)
    {
    }
}
