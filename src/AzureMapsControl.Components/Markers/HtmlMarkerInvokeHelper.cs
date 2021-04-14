namespace AzureMapsControl.Components.Markers
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal class HtmlMarkerInvokeHelper : EventInvokeHelper<HtmlMarkerJsEventArgs>
    {
        public HtmlMarkerInvokeHelper(Func<HtmlMarkerJsEventArgs, ValueTask> callback) : base(callback)
        {
        }
    }
}
