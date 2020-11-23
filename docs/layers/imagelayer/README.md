## Image Layer

The image layer supports the following image formats

- JPEG
- PNG
- BMP
- GIF (no animations)

![Image Layer](../../assets/imagelayer.png)

```
@page "/ImageLayerOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center = new Components.Atlas.Position(11.575454, 48.137392), Zoom = 13 }"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {
    public async Task OnMapReady(MapEventArgs events)
    {
        var layer = new AzureMapsControl.Components.Layers.ImageLayer
        {
            Options = new Components.Layers.ImageLayerOptions("https://ngazuremaps.blob.core.windows.net/images/munich_1858.jpg", new AzureMapsControl.Components.Atlas.Position[] {
                new Components.Atlas.Position(11.540774, 48.151994),
                new Components.Atlas.Position(11.598952, 48.151994),
                new Components.Atlas.Position(11.598952, 48.127172),
                new Components.Atlas.Position(11.540774, 48.127172)
            })
        };

        await events.Map.AddLayerAsync(layer);
    }
}
```