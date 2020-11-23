## Polygon Extrusion Layer

The polygon extrusion layer renders areas of `Polygon` and `MultiPolygon` feature geometries as extruded shapes.

![Polygon Extrusion Layer](../../assets/polygonextrusionlayer.png)

```
@page "/PolygonExtrusionLayerOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center = new Components.Atlas.Position(11.581990, 48.143534), Zoom = 4, Pitch = 45 }"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {
    public async Task OnMapReady(MapEventArgs events)
    {
        const string dataSourceId = "dataSource";
        var dataSource = new AzureMapsControl.Components.Data.DataSource(dataSourceId);
        await events.Map.AddSourceAsync(dataSource);

        await dataSource.ImportDataFromUrlAsync("https://raw.githubusercontent.com/arnaudleclerc/ng-azure-maps/main/assets/data/countries.geojson.json");

        var layer = new AzureMapsControl.Components.Layers.PolygonExtrusionLayer
        {
            Options = new Components.Layers.PolygonExtrusionLayerOptions
            {
                Source = "dataSource",
                Base = new Components.Atlas.ExpressionOrNumber(100),
                FillColor = new Components.Atlas.ExpressionOrString(new AzureMapsControl.Components.Atlas.Expression[] {
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("step"),
                    new AzureMapsControl.Components.Atlas.Expression(new AzureMapsControl.Components.Atlas.Expression[]{
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("get"),
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("DENSITY")
                    }),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#00ff80"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(10),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#09e076"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(20),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#0bbf67"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(50),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f7e305"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(100),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f7c707"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(200),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f78205"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(500),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f75e05"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(1000),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f72505"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(10000),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#6b0a05")
                }),
                FillOpacity = 0.7,
                Height = new Components.Atlas.ExpressionOrNumber(new AzureMapsControl.Components.Atlas.Expression[]
                {
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("interpolate"),
                    new Components.Atlas.Expression(new AzureMapsControl.Components.Atlas.Expression[]
{
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("linear")
                    }),
                    new Components.Atlas.Expression(new AzureMapsControl.Components.Atlas.Expression[]
{
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("get"),
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("DENSITY")
                    }),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(0),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(100),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(1200),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(960000)
                                    })
            }
        };


        await events.Map.AddLayerAsync(layer, "labels");
    }
}
```