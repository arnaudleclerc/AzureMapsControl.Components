## OverviewMap Control

The `OverviewMap` control not being part of the `atlas` library, you need to include the js file of the scalebar into your application and reference it with a `script` tag on your razor page. It can be found on the [GitHub repository of the overviewmap control](https://github.com/Azure-Samples/azure-maps-overview-map).

For this control only, there is the possibility to update its options. The `OverviewMapControl` class exposes an `UpdateAsync` with which you can update the options of the control. The following example updates the style of the map in the overlay when the style of the map has been changed.

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
            await overviewControl.UpdateAsync(options => options.MapStyle = eventArgs.Style);
        }
    }
} 
```