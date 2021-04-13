namespace AzureMapsControl.Components.Markers
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal class HtmlMarkerInvokeHelper : EventInvokeHelper<HtmlMarkerJsEventArgs>
    {
        public HtmlMarkerInvokeHelper(Func<HtmlMarkerJsEventArgs, Task> callback) : base(callback)
        {
        }
    }
}
