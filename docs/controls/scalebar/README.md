## Scalebar Control

The `Scalebar` control not being part of the `atlas` library, you need to include the js file of the scalebar into your application and reference it with a `script` tag on your razor page. It can be found on the [GitHub repository of the scalebar](https://github.com/Azure-Samples/azure-maps-scale-bar-control).

```
@page "/Scalebar"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          Controls="new Components.Controls.Control[]
          {
              new AzureMapsControl.Components.Controls.ScaleBarControl(new Components.Controls.ScaleBarControlOptions
              {
                  Units = AzureMapsControl.Components.Controls.ScaleBarControlUnits.Metric
              },
              AzureMapsControl.Components.Controls.ControlPosition.BottomRight),
          }" />
```