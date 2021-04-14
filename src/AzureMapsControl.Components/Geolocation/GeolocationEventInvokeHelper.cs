namespace AzureMapsControl.Components.Geolocation
{
    using System;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Events;

    internal class GeolocationEventInvokeHelper : EventInvokeHelper<GeolocationJsEventArgs>
    {
        public GeolocationEventInvokeHelper(Func<GeolocationJsEventArgs, ValueTask> callback) : base(callback)
        {
        }
    }
}
