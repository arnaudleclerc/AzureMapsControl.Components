## Controls

![Controls](../assets/controls.png) 

The following controls are available :

- `Compass` : A control for changing the rotation of the map.
- `Pitch` :  A control for changing the pitch of the map.
- `Style`: A control for changing the style of the map.
- `Zoom` : A control for changing the zoom of the map.
- `ScaleBar`: A control displaying a scalebar on the map
- `OverviewMap`: A control displaying an overview of the map

There are two ways to add the controls to the map : 

- Using the `Controls` parameter on the `AzureMap`

```
@page "/Controls"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          Controls="new Components.Controls.Control[]
          {
                        new Components.Controls.ZoomControl(new Components.Controls.ZoomControlOptions { Style = AzureMapsControl.Components.Controls.ControlStyle.Dark }, AzureMapsControl.Components.Controls.ControlPosition.TopLeft),
            new Components.Controls.PitchControl(new Components.Controls.PitchControlOptions { Style = AzureMapsControl.Components.Controls.ControlStyle.Dark }, AzureMapsControl.Components.Controls.ControlPosition.TopRight),
            new Components.Controls.CompassControl(new Components.Controls.CompassControlOptions { Style = AzureMapsControl.Components.Controls.ControlStyle.Dark }, AzureMapsControl.Components.Controls.ControlPosition.BottomLeft),
            new Components.Controls.StyleControl(new Components.Controls.StyleControlOptions { Style = AzureMapsControl.Components.Controls.ControlStyle.Dark, MapStyles = MapStyle.All() }, AzureMapsControl.Components.Controls.ControlPosition.BottomRight)
          }" />
```

- Or subscribing to the `OnReady` event and calling the `AddControlsAsync` method on the map.

```
@page "/ControlsOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        await eventArgs.Map.AddControlsAsync
        (
            new Components.Controls.ZoomControl(position: AzureMapsControl.Components.Controls.ControlPosition.TopLeft),
            new Components.Controls.PitchControl(position: AzureMapsControl.Components.Controls.ControlPosition.TopRight),
            new Components.Controls.CompassControl(position: AzureMapsControl.Components.Controls.ControlPosition.BottomLeft),
            new Components.Controls.StyleControl(position: AzureMapsControl.Components.Controls.ControlPosition.BottomRight)
        );
    }

}
```