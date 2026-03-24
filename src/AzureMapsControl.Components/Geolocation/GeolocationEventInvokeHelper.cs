
using System;
using System.Threading.Tasks;

using AzureMapsControl.Components.Events;

namespace AzureMapsControl.Components.Geolocation;
internal sealed class GeolocationEventInvokeHelper(Func<GeolocationJsEventArgs, ValueTask> callback) : EventInvokeHelper<GeolocationJsEventArgs>(callback);
