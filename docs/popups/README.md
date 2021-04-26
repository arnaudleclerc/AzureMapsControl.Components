## Popups

![Popup](../assets/popup.png)

Popups can be added to the map using the `AddPopupAsync` method on the map. If the `OpenOnAdd` property is set to true, the popup will be opened once it has been added to the map.

```
@page "/Popups/CustomizePopup"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady"/>

@code  {

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var popup = new Components.Popups.Popup(new Components.Popups.PopupOptions {
            Content = "<div style=\"padding:10px;color:white\">Hello World</div>",
            Position = new Components.Atlas.Position(0, 0),
            FillColor = "rgba(0,0,0,0.8)",
            CloseButton = false,
            OpenOnAdd = true
        });

        await eventArgs.Map.AddPopupAsync(popup);
    }

}
```

A popup can also be opened using the `OpenAsync` method.

```
@page "/PopupOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          StyleOptions="StyleOptions"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = MapStyle.GrayscaleDark
    };

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var popup = new Components.Popups.Popup(new Components.Popups.PopupOptions
        {
            CloseButton = false,
            Content = "Please customize me",
            Position = new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534)
        });
        await eventArgs.Map.AddPopupAsync(popup);
        await popup.OpenAsync();
    }

}
```

The popup can then be close by calling the `CloseAsync` method.

```
@page "/PopupOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          StyleOptions="StyleOptions"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = MapStyle.GrayscaleDark
    };

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var popup = new Components.Popups.Popup(new Components.Popups.PopupOptions
        {
            CloseButton = false,
            Content = "Please customize me",
            Position = new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534)
        });
        await eventArgs.Map.AddPopupAsync(popup);
        await popup.OpenAsync();
        await popup.CloseAsync();
    }

}
```

Calling the `RemoveAsync` method will remove the popup from the map. You can also call the `RemovePopupAsync` method on the map and provide the popup to remove.

```
@page "/PopupOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          StyleOptions="StyleOptions"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = MapStyle.GrayscaleDark
    };

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var popup = new Components.Popups.Popup(new Components.Popups.PopupOptions
        {
            CloseButton = false,
            Content = "Please customize me",
            Position = new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534)
        });
        await eventArgs.Map.AddPopupAsync(popup);
        await popup.OpenAsync();

        await popup.RemoveAsync();
    }

}
```

The popup can be updated by calling the `UpdateAsync` method and providing the update to apply on the options. The following example only updates the content of the popup.

```
@page "/PopupOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          StyleOptions="StyleOptions"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = MapStyle.GrayscaleDark
    };

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var popup = new Components.Popups.Popup(new Components.Popups.PopupOptions
        {
            CloseButton = false,
            Content = "Please customize me",
            Position = new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534)
        });
        await eventArgs.Map.AddPopupAsync(popup);
        await popup.OpenAsync();

        await popup.UpdateAsync(options => options.Content = "Thanks for updating me");
    }

}
```

All the popups can be cleared using the `ClearPopupsAsync` method on the map.

```
@page "/PopupOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          StyleOptions="StyleOptions"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = MapStyle.GrayscaleDark
    };

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var popup = new Components.Popups.Popup(new Components.Popups.PopupOptions
        {
            CloseButton = false,
            Content = "Please customize me",
            Position = new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534)
        });
        await eventArgs.Map.AddPopupAsync(popup);
        await popup.OpenAsync();

        await popup.UpdateAsync(options => options.Content = "Thanks for updating me");
        await eventArgs.Map.ClearPopupsAsync();
    }

}
```

### Events

You can subscribe to events triggered on the popup by defining the `EventActivationFlags` and subscribing to the corresponding event on the `Popup`. 

The following events are available : `Open`, `DragStart`, `DragEnd`, `Drag` and `Close`.

The following example reacts to the opening of a popup : 

```
@page "/PopupOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          StyleOptions="StyleOptions"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = MapStyle.GrayscaleDark
    };

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var popup = new Components.Popups.Popup(new Components.Popups.PopupOptions
        {
            CloseButton = false,
            Content = "Please customize me",
            Position = new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534)
        }, AzureMapsControl.Components.Popups.PopupEventActivationFlags.None().Enable(AzureMapsControl.Components.Popups.PopupEventType.Open));

        popup.OnOpen += eventArgs =>
        {
            Console.WriteLine("Popup opened");
        };

        await eventArgs.Map.AddPopupAsync(popup);
        await popup.OpenAsync();
    }

}
```

### Templates

String templates, `PropertyInfo` templates and the combination of both are supported. The `AddPopupAsync` method of the `Map` accepts a `PopupTemplate` and a dictionary of properties as parameters.

```
@page "/Popups/StringTemplate"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public class PropertySubValues
    {
        public string SubValue { get; set; }
    }

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var template = new Components.Popups.PopupTemplate
        {
            Content = new Components.Atlas.Either<string, IEnumerable<Components.Atlas.PropertyInfo>, IEnumerable<Components.Atlas.Either<string, IEnumerable<Components.Atlas.PropertyInfo>>>>
                ("This template uses a string template with placeholders.<br/><br/> - Value 1 = {value1}<br/> - Value 2 = {value2/subValue}<br/> - Array value [2] = {arrayValue/2}"),
            NumberFormat = new Components.Atlas.FormatOptions.NumberFormatOptions
            {
                MaximumFractionDigits = 2
            }
        };

        var properties = new Dictionary<string, object>
        {
            { "title", "Template 1 - String template" },
            { "value1", 1.2345678},
            { "value2", new PropertySubValues { SubValue = "Pizza" } },
            { "arrayValue", new [] { 3,4,5,6 } }
        };

        var position = new Components.Atlas.Position(0, 0);

        var popup = new Components.Popups.Popup(new Components.Popups.PopupOptions {
            Position = position,
            OpenOnAdd = true
        });

        await eventArgs.Map.AddPopupAsync(popup, template, properties);
    }

}
```

If you want to reuse a template on an already existing popup, you can use the `ApplyTemplateAsync` method directly on the popup. You can also specify some updates which need to be applied on the options of the popup, if you want for example to change the location of the popup. If the content is specified on the options, it will be overriden by the template.

```
@page "/Popups/ReuseTemplate"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center= new AzureMapsControl.Components.Atlas.Position(-122.33, 47.63), Zoom = 11}"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public class PropertySubValues
    {
        public string SubValue { get; set; }
    }

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var datasource = new Components.Data.DataSource();
        await eventArgs.Map.AddSourceAsync(datasource);

        await datasource.AddAsync(
            new Components.Atlas.Feature<Components.Atlas.Point>(
                "feature1",
                new Components.Atlas.Point(new Components.Atlas.Position(-122.33, 47.6))
            ),
            new Components.Atlas.Feature<Components.Atlas.Point>(
                "feature2",
                new Components.Atlas.Point(new Components.Atlas.Position(-122.335, 47.645))
            ),
            new Components.Atlas.Feature<Components.Atlas.Point>(
                "feature3",
                new Components.Atlas.Point(new Components.Atlas.Position(-122.325, 47.635))
            )
        );

        var template = new Components.Popups.PopupTemplate
        {
            Content = new Components.Atlas.Either<string, IEnumerable<Components.Atlas.PropertyInfo>, IEnumerable<Components.Atlas.Either<string, IEnumerable<Components.Atlas.PropertyInfo>>>>
                ("This template uses a string template with placeholders.<br/><br/> - Value 1 = {value1}<br/> - Value 2 = {value2/subValue}<br/> - Array value [2] = {arrayValue/2}"),
            NumberFormat = new Components.Atlas.FormatOptions.NumberFormatOptions
            {
                MaximumFractionDigits = 2
            }
        };

        var layer = new Components.Layers.SymbolLayer {
            Options = new Components.Layers.SymbolLayerOptions {
                Source = datasource.Id,
                IconOptions = new Components.Layers.IconOptions {
                    AllowOverlap = true
                }
            },
            EventActivationFlags = Components.Layers.LayerEventActivationFlags.None().Enable(Components.Layers.LayerEventType.Click)
        };

        await eventArgs.Map.AddLayerAsync(layer);

        var popup = new Components.Popups.Popup();
        await eventArgs.Map.AddPopupAsync(popup);

        layer.OnClick += async clickEventArgs => {
            if(clickEventArgs.Shapes is not null && clickEventArgs.Shapes.Any()) {
                var shape = clickEventArgs.Shapes.First();
                Dictionary<string, object> properties = null;
                switch (shape.Id)
                {
                    case "feature1":
                        properties = new Dictionary<string, object> {
                        { "title", "First feature" },
                        { "value1", 1.2345678},
                        { "value2", new PropertySubValues { SubValue = "Pizza" } },
                        { "arrayValue", new [] { 3,4,5,6 } }
                    };
                        break;
                    case "feature2":
                        properties = new Dictionary<string, object> {
                    { "title", "Second feature"},
                    { "value1", 3.14159},
                    { "value2", new PropertySubValues { SubValue = "Donut" } },
                    { "arrayValue", new [] { 13, 34, 42, 46 } }
                    };
                        break;
                    case "feature3":
                        properties = new Dictionary<string, object> {
                    { "title", "Third feature"},
                    { "value1", 100.1000001},
                    { "value2", new PropertySubValues { SubValue = "Taco" } },
                    { "arrayValue", new [] { 0, 0, 0 } }
                    };
                        break;
                }
                await popup.ApplyTemplateAsync(template, properties, options => {
                    options.Position = (shape.Geometry as Components.Atlas.Point).Coordinates;
                    options.PixelOffset = new Components.Atlas.Pixel(0, -18);
                });
                await popup.OpenAsync();
            }
        };

    }

}
```