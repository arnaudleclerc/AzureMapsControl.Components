
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;
using AzureMapsControl.Components.Map;

namespace AzureMapsControl.Components.Layers;
internal sealed class LayerEventInvokeHelper(Func<MapJsEventArgs, ValueTask> callback) : EventInvokeHelper<MapJsEventArgs>(callback);
