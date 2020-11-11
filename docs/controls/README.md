## Controls

![Controls](../assets/controls.png) 

4 types of controls are available :

- `Compass` : A control for changing the rotation of the map.
- `Pitch` :  A control for changing the pitch of the map.
- `Style`: A control for changing the style of the map.
- `Zoom` : A control for changing the zoom of the map.

There are two ways to add the controls to the map : 

- Using the `Controls` parameter on the `AzureMap`

```
@page "/Controls"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          Controls="new []
          {
                        new Components.Atlas.Control(AzureMapsControl.Components.Atlas.ControlType.Zoom, AzureMapsControl.Components.Atlas.ControlPosition.TopLeft),
            new Components.Atlas.Control(AzureMapsControl.Components.Atlas.ControlType.Pitch, AzureMapsControl.Components.Atlas.ControlPosition.TopRight),
            new Components.Atlas.Control(AzureMapsControl.Components.Atlas.ControlType.Compass, AzureMapsControl.Components.Atlas.ControlPosition.BottomLeft),
            new Components.Atlas.Control(AzureMapsControl.Components.Atlas.ControlType.Style, AzureMapsControl.Components.Atlas.ControlPosition.BottomRight)
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
            new Components.Atlas.Control(AzureMapsControl.Components.Atlas.ControlType.Zoom, AzureMapsControl.Components.Atlas.ControlPosition.TopLeft),
            new Components.Atlas.Control(AzureMapsControl.Components.Atlas.ControlType.Pitch, AzureMapsControl.Components.Atlas.ControlPosition.TopRight),
            new Components.Atlas.Control(AzureMapsControl.Components.Atlas.ControlType.Compass, AzureMapsControl.Components.Atlas.ControlPosition.BottomLeft),
            new Components.Atlas.Control(AzureMapsControl.Components.Atlas.ControlType.Style, AzureMapsControl.Components.Atlas.ControlPosition.BottomRight)
        );
    }

}
```