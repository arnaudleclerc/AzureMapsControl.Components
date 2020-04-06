namespace AzureMapsControl.Components.Events
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;

    internal abstract class EventInvokeHelper<T> where T : new ()
    {
        private readonly Func<T, Task> _callback;
        public EventInvokeHelper(Func<T, Task> callback) => _callback = callback;

        [JSInvokable]
        public async Task NotifyEventAsync(T arg) => await _callback.Invoke(arg);
    }
}
