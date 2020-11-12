A data source manages raw location data in GeoJSON format locally. Good for small to medium data sets (upwards of hundreds of thousands of shapes).

## Adding geometries

Multiple types of `Geometries` can be added to a data source :

- LineString
- MultiLineString
- MultiPoint
- MultiPolygon
- Point
- Polygon

You can add geometry on a data source using the `AddAsync` method.

```
@page "/BubbleLayerOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          Zoom="2"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {
    public async Task OnMapReady(MapEventArgs events)
    {
        const string dataSourceId = "bubbleDataSource";
        var dataSource = new AzureMapsControl.Components.Data.DataSource(dataSourceId);
        await events.Map.AddSourceAsync(dataSource);


        var geometries = new List<AzureMapsControl.Components.Atlas.Geometry>();
        for (var i = 0; i < 10; i++)
        {
            geometries.Add(new AzureMapsControl.Components.Atlas.Point(new Components.Atlas.Position(i * 5, i * 5)));
        }

        await dataSource.AddAsync(geometries);

        var layer = new AzureMapsControl.Components.Layers.BubbleLayer
        {
            Options = new Components.Layers.BubbleLayerOptions
            {
                Color = new Components.Atlas.ExpressionOrString("white"),
                Radius = new Components.Atlas.ExpressionOrNumber(5),
                StrokeColor = new Components.Atlas.ExpressionOrString("#4288f7"),
                StrokeWidth = new Components.Atlas.ExpressionOrNumber(6),
                Source = dataSourceId
            }
        };

        await events.Map.AddLayerAsync(layer);
    }
}
```

## Importing data

You can import data on the data source using the `ImportDataFromUrlAsync` method. 

```
@page "/HeatmapLayerOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          Zoom="2"
          Style="grayscale_dark"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {
    public async Task OnMapReady(MapEventArgs events)
    {
        const string dataSourceId = "heatmapDataSource";
        var dataSource = new AzureMapsControl.Components.Data.DataSource(dataSourceId);
        await events.Map.AddSourceAsync(dataSource);
        await dataSource.ImportDataFromUrlAsync("https://services1.arcgis.com/0MSEUqKaxRlEPj5g/arcgis/rest/services/ncov_cases/FeatureServer/1/query?where=1%3D1&f=geojson&outFields=*");

        var layer = new AzureMapsControl.Components.Layers.HeatmapLayer
        {
            Options = new Components.Layers.HeatmapLayerOptions
            {
                //This will get converted to ['get', 'Confirmed']
                Weight = new Components.Atlas.ExpressionOrNumber(
                    new AzureMapsControl.Components.Atlas.Expression[]
                    {
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("get"),
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("Confirmed"),
                    }
                ),
                Radius = new Components.Atlas.ExpressionOrNumber(20),
                Source = dataSourceId
            }
        };

        await events.Map.AddLayerAsync(layer);
    }
}
```

## Removing geometries

Geometries can be removed from the data source using the `RemoveAsync` method.

## Clear the data source

The data source can be cleared using the `ClearAsync` method. This will remove all the geometries from the data source.