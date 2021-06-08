namespace AzureMapsControl.Components.FullScreen
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal class FullScreenEventInvokeHelper : EventInvokeHelper<bool>
    {
        public FullScreenEventInvokeHelper(Func<bool, ValueTask> callback) : base(callback)
        {
        }
    }
}
