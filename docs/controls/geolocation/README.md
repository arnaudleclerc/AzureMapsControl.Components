## Geolocation Control

The `Geolocation` control is not part of the `atlas` library, so you need to include the js file of the control into your application and reference it with a `script` tag on your razor page. It can be found on the [GitHub repository of the geolocation control](https://github.com/Azure-Samples/azure-maps-geolocation-control).

Before adding the control, you might want to check if the browser supports the geolocation. You can inject an instance of `IGeolocationService` on your pages and call its `IsGeolocationSupportedAsync method` to do so.

You can also react to two events on this control : 

- `GeolocationSuccess` will trigger if the geolocation was successful. The event's information contains a feature of the current location.
- `GeolocationError` triggers if the the geolocation fails and contains information about the error.

```
@page "/GeolocationControl"
@inject AzureMapsControl.Components.Geolocation.IGeolocationService GeolocationService

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready, MapEventType.Click)"
          OnReady="OnMapReady" />

@code  {

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        if (await GeolocationService.IsGeolocationSupportedAsync())
        {
            var control = new Components.Controls.GeolocationControl(
                new Components.Controls.GeolocationControlOptions {
                    Style = Components.Controls.ControlStyle.Auto
                },
                Components.Controls.ControlPosition.TopRight, 
                Components.Geolocation.GeolocationEventActivationFlags.All());

            await eventArgs.Map.AddControlsAsync(control);

            control.GeolocationSuccess += args =>
            {
                Console.WriteLine(args.Feature.Geometry.Coordinates.Longitude);
                Console.WriteLine(args.Feature.Geometry.Coordinates.Latitude);
            };

            control.GeolocationError += args =>
            {
                Console.WriteLine(args.Code);
                Console.WriteLine(args.Message);
            };
        }
    }

}
```