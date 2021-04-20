namespace AzureMapsControl.Components.Data
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal class DataSourceEventInvokeHelper : EventInvokeHelper<DataSourceEventArgs>
    {
        public DataSourceEventInvokeHelper(Func<DataSourceEventArgs, ValueTask> callback) : base(callback)
        {
        }
    }
}
