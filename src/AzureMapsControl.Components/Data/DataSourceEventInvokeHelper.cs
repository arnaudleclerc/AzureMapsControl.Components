
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Data;
internal class DataSourceEventInvokeHelper : EventInvokeHelper<DataSourceEventArgs>
{
    public DataSourceEventInvokeHelper(Func<DataSourceEventArgs, ValueTask> callback) : base(callback)
    {
    }
}
