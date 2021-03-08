## Animations

The `animations` are not part of the `atlas` library, so you need to include the js file directly into your application via a script tag. It can be found on the [GitHub repository of the animations](https://github.com/Azure-Samples/azure-maps-animations).

To create an animation, you can inject an instance of `IAnimationService` inside your razor views or services. The following animations can be created via this service : 

- `DropAsync` : Adds an offset array property to point shapes and animates its y value to simulate dropping.
- `DropMarkersAsync` : Adds an offset to HtmlMarkers to animate its y value to simulate dropping.
- `GroupAnimationAsync` : Group multiple animations
- `FlowingDashedLineAsync` : Animates the dash-array of a line layer to make it appear to flow. 
- `MoveAlongPath` : Animates a Point shape or a marker along a path.
- `MoveAlongRouteAsync` : Animates a Point shape along a route path. 
- `MorphAsync` : Animates the morphing of a shape from one geometry type or set of coordinates to another.
- `SetCoordinatesAsync` : Animates the update of coordinates on a shape.
- `SnakelineAsync` : Animates the path of a LineString. 

Each of those methods gives back an Animation which you can further control using its `PlayAsync`, `PauseAsync`, `ResetAsync`, `SeekAsync`, `StopAsync` and `DisposeAsync` methods. Not all the animations exposes those methods.

Note that if you try to use an animation which has already been disposed, the library will raise an `AnimationAlreadyDisposedException`.

The following example shows how to create a drop animation.

```
@page "/Animations/Drop"
@inject Components.Animations.IAnimationService AnimationService

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center = new Components.Atlas.Position(-122.33825, 47.53945), Zoom = 7 }"
          StyleOptions="StyleOptions"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Click, MapEventType.Ready)"
          OnClick="OnMapClick"
          OnReady="OnMapReady" />

@code  {
    private AzureMapsControl.Components.Data.DataSource _dataSource;
    private AzureMapsControl.Components.Animations.IDropAnimation _animation;

    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = MapStyle.GrayscaleDark
    };

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        _dataSource = new Components.Data.DataSource();
        await eventArgs.Map.AddSourceAsync(_dataSource);

        await eventArgs.Map.AddLayerAsync(new AzureMapsControl.Components.Layers.SymbolLayer
        {
            Options = new Components.Layers.SymbolLayerOptions
            {
                Source = _dataSource.Id,
                IconOptions = new Components.Layers.IconOptions
                {
                    AllowOverlap = true,
                    IgnorePlacement = true,
                    Offset = new Components.Atlas.Expression(new AzureMapsControl.Components.Atlas.Expression[]
                    {
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("get"),
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("offset")
                    })
                }
            }
        });
    }

    public async Task OnMapClick(MapMouseEventArgs eventArgs)
    {
        if(_animation != null)
        {
            await _animation.DisposeAsync();
        }
        
        _animation = await AnimationService.DropAsync(new AzureMapsControl.Components.Atlas.Point(eventArgs.Position), _dataSource, null, new AzureMapsControl.Components.Animations.Options.DropAnimationOptions
        {
            AutoPlay = true,
            Easing = AzureMapsControl.Components.Animations.Options.Easing.EaseOutBounce
        });
    }
}
```