## Symbol Layer

Connect a symbol to a data source, and use it to render an icon or a text at a given point.

Symbol layers are rendered using WebGL. Use a symbol layer to render large collections of points on the map. Compared to HTML marker, the symbol layer renders a large number of point data on the map, with better performance. However, the symbol layer doesn't support traditional CSS and HTML elements for styling.

![Symbol Layer](../../assets/symbollayer.png)

```
@page "/Layers/SymbolLayerOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Zoom = 2 }"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready, MapEventType.SourceAdded)"
          OnReady="OnMapReady"
          OnSourceAdded="OnDatasourceAdded"/>

@code  {

    private readonly string _blueDatasourceId = "blueDatasource";
    private readonly string _redDatasourceId = "redDatasource";

    public async Task OnMapReady(MapEventArgs events)
    {
        var blueDataSource = new AzureMapsControl.Components.Data.DataSource(_blueDatasourceId);
        await events.Map.AddSourceAsync(blueDataSource);

        var redDataSource = new AzureMapsControl.Components.Data.DataSource(_redDatasourceId);
        await events.Map.AddSourceAsync(redDataSource);

        var bluePoints = new List<AzureMapsControl.Components.Atlas.Shape>();
        var redPoints = new List<AzureMapsControl.Components.Atlas.Shape>();

        for (var i = 0; i < 10; i++)
        {
            bluePoints.Add(new Components.Atlas.Shape<Components.Atlas.Point>(new Components.Atlas.Point(new Components.Atlas.Position(i * 5, i * 5))));
            redPoints.Add(new Components.Atlas.Shape<Components.Atlas.Point>(new Components.Atlas.Point(new Components.Atlas.Position(i * -5, i * 5))));
        }

        await blueDataSource.AddAsync(bluePoints);
        await redDataSource.AddAsync(redPoints);
    }

    public async Task OnDatasourceAdded(MapSourceEventArgs eventArgs)
    {
        var layer = new AzureMapsControl.Components.Layers.SymbolLayer
        {
            Options = new Components.Layers.SymbolLayerOptions
            {
                Source = eventArgs.Source.Id
            }
        };

        if (eventArgs.Source.Id == _redDatasourceId)
        {
            layer.Options.IconOptions = new Components.Layers.IconOptions
            {
                Image = new Components.Atlas.ExpressionOrString("pin-red")
            };
        }

        await eventArgs.Map.AddLayerAsync(layer);
    }
}
```