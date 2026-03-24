
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Markers;
internal class HtmlMarkerInvokeHelper : EventInvokeHelper<HtmlMarkerJsEventArgs>
{
    public HtmlMarkerInvokeHelper(Func<HtmlMarkerJsEventArgs, ValueTask> callback) : base(callback)
    {
    }
}
