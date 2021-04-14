namespace AzureMapsControl.Components.Popups
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal sealed class PopupInvokeHelper : EventInvokeHelper<PopupEventArgs>
    {
        public PopupInvokeHelper(Func<PopupEventArgs, ValueTask> callback) : base(callback) { }
    }
}
