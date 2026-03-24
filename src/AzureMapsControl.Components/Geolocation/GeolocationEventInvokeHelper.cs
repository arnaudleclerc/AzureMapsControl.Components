
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Geolocation;
internal class GeolocationEventInvokeHelper : EventInvokeHelper<GeolocationJsEventArgs>
{
    public GeolocationEventInvokeHelper(Func<GeolocationJsEventArgs, ValueTask> callback) : base(callback)
    {
    }
}
