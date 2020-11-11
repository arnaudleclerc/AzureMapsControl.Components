## Popups

![Popup](../assets/popup.png)

Popups can be added to the map using the `AddPopupAsync` method on the map. If the `OpenOnAdd` property is set to true, the popup will be opened once it has been added to the map.

```
@page "/PopupOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          Style="grayscale_dark"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        await eventArgs.Map.AddPopupAsync(new Components.Popups.Popup
        {
            Options = new Components.Popups.PopupOptions
            {
                CloseButton = false,
                Content = "Please customize me",
                Position = new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534),
                OpenOnAdd = true
            }
        });
    }

}
```

A popup can also be opened using the `OpenAsync` method.

```
@page "/PopupOnReady"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          Style="grayscale_dark"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"
          OnReady="OnMapReady" />

@code  {

    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        var popup = new Components.Popups.Popup
        {
            Options = new Components.Popups.PopupOptions
            {
                CloseButton = false,
                Content = "Please customize me",
                Position = new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534)
            }
        };
        await eventArgs.Map.AddPopupAsync(popup);
        await popup.OpenAsync();
    }

}
```