namespace AzureMapsControl.Components.Geolocation
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Events;

    public sealed class GeolocationEventActivationFlags : EventActivationFlags<GeolocationEventType, GeolocationEventActivationFlags>
    {
        private GeolocationEventActivationFlags(bool defaultFlag) :
            base(new Dictionary<GeolocationEventType, bool> {
                { GeolocationEventType.GeolocationError, defaultFlag },
                { GeolocationEventType.GeolocationSuccess, defaultFlag }
            })
        { }

        public static GeolocationEventActivationFlags All() => new GeolocationEventActivationFlags(true);
        public static GeolocationEventActivationFlags None() => new GeolocationEventActivationFlags(false);
    }
}
