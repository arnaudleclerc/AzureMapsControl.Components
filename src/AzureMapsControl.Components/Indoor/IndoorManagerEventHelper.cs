namespace AzureMapsControl.Components.Indoor
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal class IndoorManagerEventHelper : EventInvokeHelper<IndoorManagerJsEventArgs>
    {
        public IndoorManagerEventHelper(Func<IndoorManagerJsEventArgs, ValueTask> callback) : base(callback)
        {
        }
    }
}
