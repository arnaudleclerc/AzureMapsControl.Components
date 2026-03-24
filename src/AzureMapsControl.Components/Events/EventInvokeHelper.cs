
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Microsoft.JSInterop;

namespace AzureMapsControl.Components.Events;
[ExcludeFromCodeCoverage]
internal abstract class EventInvokeHelper<T> where T : new()
{
    private readonly Func<T, ValueTask> _callback;
    public EventInvokeHelper(Func<T, ValueTask> callback) => _callback = callback;

    [JSInvokable]
    public async ValueTask NotifyEventAsync(T arg) => await _callback.Invoke(arg).ConfigureAwait(false);
}
