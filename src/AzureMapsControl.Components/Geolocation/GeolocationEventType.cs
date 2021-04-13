namespace AzureMapsControl.Components.Geolocation
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Events;

    [ExcludeFromCodeCoverage]
    public sealed class GeolocationEventType : AtlasEventType
    {
        public static readonly GeolocationEventType GeolocationError = new GeolocationEventType("geolocationerror");
        public static readonly GeolocationEventType GeolocationSuccess = new GeolocationEventType("geolocationsuccess");
        private GeolocationEventType(string type): base(type) { }
    }
}
