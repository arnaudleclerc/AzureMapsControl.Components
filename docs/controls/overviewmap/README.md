## OverviewMap Control

The `OverviewMap` control not being part of the `atlas` library, you need to include the js file of the scalebar into your application and reference it with a `script` tag on your razor page. It can be found on the [GitHub repository of the overviewmap control](https://github.com/Azure-Samples/azure-maps-overview-map).

```
@page "/OverviewMap"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          Controls="new Components.Controls.Control[]
          {
              new AzureMapsControl.Components.Controls.OverviewMapControl(new Components.Controls.OverviewMapControlOptions
              {
                  ShowToggle = true,
                  Style = new Components.Atlas.Either<Components.Controls.ControlStyle, string>(AzureMapsControl.Components.Controls.ControlStyle.Auto)
              },
              AzureMapsControl.Components.Controls.ControlPosition.TopLeft),
              new AzureMapsControl.Components.Controls.StyleControl(position: AzureMapsControl.Components.Controls.ControlPosition.TopRight)
          }"
          EventActivationFlags="MapEventActivationFlags.None().Enable(MapEventType.StyleData)"
          OnStyleData="OnStyleData"/>

@code {
    public async void OnStyleData(MapStyleDataEventArgs eventArgs)
    {
        var overviewControl = eventArgs.Map.Controls?.OfType<AzureMapsControl.Components.Controls.OverviewMapControl>()?.FirstOrDefault();
        if(overviewControl is not null)
        {
            await overviewControl.SetOptionsAsync(options => options.MapStyle = eventArgs.Style);
        }
    }
} 
```