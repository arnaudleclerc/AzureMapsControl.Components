
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Markers;
internal sealed class HtmlMarkerInvokeHelper(Func<HtmlMarkerJsEventArgs, ValueTask> callback) : EventInvokeHelper<HtmlMarkerJsEventArgs>(callback);
