namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;
    using AzureMapsControl.Components.Markers;

    internal class HtmlMarkerInvokeHelper : EventInvokeHelper<HtmlMarkerJsEventArgs>
    {
        public HtmlMarkerInvokeHelper(Func<HtmlMarkerJsEventArgs, Task> callback) : base(callback)
        {
        }
    }
}
