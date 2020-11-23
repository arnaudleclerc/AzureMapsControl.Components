## Traffic

![Traffic](../assets/traffic.png)

Traffic information can be added to the map by providing the `TrafficOptions` parameter on the `AzureMap` component. 

```
@page "/TrafficOptionsUpdate"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center = new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534), Zoom = 10}"
          StyleOptions="StyleOptions"
          EventActivationFlags="MapEventActivationFlags.None().Enable(MapEventType.Ready)"
          TrafficOptions="new Components.Traffic.TrafficOptions { Incidents = false, Flow = AzureMapsControl.Components.Traffic.TrafficFlow.Relative }"
          OnReady="OnMapReadyAsync" />

@code {

    public StyleOptions StyleOptions = new StyleOptions
    {
        ShowLogo = false
    };

    public async Task OnMapReadyAsync(MapEventArgs eventArgs)
    {
        System.Threading.Thread.Sleep(5000);
        await eventArgs.Map.SetTrafficOptionsAsync(options => options.Incidents = true);
    }
} 
```