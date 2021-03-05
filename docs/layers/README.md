## Layers

Layers can be added to the map after the `MapReady` event has been triggered by calling the `AddLayerAsync` method and providing the desired layer with its `Options` and `EventsActivationFlags`. 

- [Bubble Layer](./bubblelayer)
- [Heatmap Layer](./heatmaplayer)
- [Image Layer](./imagelayer)
- [Line Layer](./linelayer)
- [Polygon Extrusion Layer](./polygonextrusionlayer)
- [Polygon Layer](./polygonlayer)
- [Symbol Layer](./symbollayer)

## Add a layer

```
@page "/TileLayerOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center = new Components.Atlas.Position(-99.47, 40.75), Zoom = 4}"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {
    public async Task OnMapReady(MapEventArgs events)
    {
        var layer = new AzureMapsControl.Components.Layers.TileLayer
        {
            Options = new Components.Layers.TileLayerOptions("https://mesonet.agron.iastate.edu/cache/tile.py/1.0.0/nexrad-n0q-900913/{z}/{x}/{y}.png"),
            EventActivationFlags = AzureMapsControl.Components.Layers.LayerEventActivationFlags.None().Enable(AzureMapsControl.Components.Layers.LayerEventType.LayerAdded)
        };

        layer.OnLayerAdded += e =>
        {
            Console.WriteLine("Layer added");
        };

        await events.Map.AddLayerAsync(layer);
    }
}
```

## Events

The following events are available and can be enabled or disabled for all the different types of layers : `MouseDown`, `MouseUp`, `MouseOver`, `MouseMove`, `Click`, `DblClick`, `MouseOut`, `MouseEnter`, `MouseLeave`, `ContextMenu`, `TouchStart`, `TouchEnd`, `TouchMove`, `TouchCancel`, `Wheel`. By default, all the events are deactivated. The events flags can be passed on the `Options` while creating the layer.

```
var layer = new AzureMapsControl.Components.Layers.TileLayer
        {
            Options = new Components.Layers.TileLayerOptions("https://mesonet.agron.iastate.edu/cache/tile.py/1.0.0/nexrad-n0q-900913/{z}/{x}/{y}.png"),
            EventActivationFlags = AzureMapsControl.Components.Layers.LayerEventActivationFlags.None().Enable(AzureMapsControl.Components.Layers.LayerEventType.LayerAdded, AzureMapsControl.Components.Layers.LayerEventType.LayerRemoved)
        };
```

## Remove a layer

Layers can be removed by calling the `RemoveLayersAsync` method on the map and providing the list of layers to remove or their ids. 

```
@page "/LayerRemove"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center = new Components.Atlas.Position(-99.47, 40.75), Zoom = 4 } "
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {
    public async Task OnMapReady(MapEventArgs events)
    {
        var layer = new AzureMapsControl.Components.Layers.TileLayer
        {
            Options = new Components.Layers.TileLayerOptions("https://mesonet.agron.iastate.edu/cache/tile.py/1.0.0/nexrad-n0q-900913/{z}/{x}/{y}.png"),
            EventActivationFlags = AzureMapsControl.Components.Layers.LayerEventActivationFlags.None().Enable(AzureMapsControl.Components.Layers.LayerEventType.LayerAdded, AzureMapsControl.Components.Layers.LayerEventType.LayerRemoved)
        };

        layer.OnLayerAdded += e =>
        {
            Console.WriteLine("Layer added");
        };

        layer.OnLayerRemoved += e =>
        {
            Console.WriteLine("Layer removed");
        };

        await events.Map.AddLayerAsync(layer);

        System.Threading.Thread.Sleep(10000);

        await events.Map.RemoveLayersAsync(layer);
    }
}
```

A `ClearLayersAsync` method on the Map allows you to remove all the user added layers.