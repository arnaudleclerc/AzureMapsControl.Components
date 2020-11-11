## Traffic

![Traffic](./assets/traffic.png)

Traffic information can be added to the map by providing the `TrafficOptions` parameter on the `AzureMap` component. 

```
@page "/Traffic"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          TrafficOptions="new Components.Traffic.TrafficOptions { Incidents = true, Flow = AzureMapsControl.Components.Traffic.TrafficFlow.Relative }"
          Center="new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534)"
          Zoom="10"/>
```