
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Map;
internal sealed class MapEventInvokeHelper(Func<MapJsEventArgs, ValueTask> callback) : EventInvokeHelper<MapJsEventArgs>(callback);
