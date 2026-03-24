
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.FullScreen;
internal sealed class FullScreenEventInvokeHelper(Func<bool, ValueTask> callback) : EventInvokeHelper<bool>(callback);
