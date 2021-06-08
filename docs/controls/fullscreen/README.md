## Fullscreen Control

The `FullScreen` control is not part of the `atlas` library, so you need to include the js file of the control into your application and reference it with a `script` tag on your razor page. It can be found on the [GitHub repository of the fullscreen control](https://github.com/Azure-Samples/azure-maps-fullscreen-control).

Before adding the control, you might want to check if the browser supports the fullscreen mode. You can inject an instance of `IFullScreenService` on your pages and call its `IsSupportedAsync` method to do so.

You can also react to the `OnFullScreenChanged` event on this control. This event exposes a boolean representating the current state (`true` if the container is in full screen mode, otherwise `false`).

```
@page "/Controls/FullScreen"

@inject AzureMapsControl.Components.FullScreen.IFullScreenService FullScreenService

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          EventActivationFlags="AzureMapsControl.Components.Map.MapEventActivationFlags.None().Enable(MapEventType.Ready)"
          OnReady="OnMapReady"
          StyleOptions="new AzureMapsControl.Components.Map.StyleOptions { Style = AzureMapsControl.Components.Map.MapStyle.GrayscaleLight }"/>

@code {
    public async Task OnMapReady(MapEventArgs eventArgs)
    {
        if (await FullScreenService.IsSupportedAsync())
        {
            var fullScreenControl = new AzureMapsControl.Components.Controls.FullScreenControl(new Components.Controls.FullScreenControlOptions
            {
                Style = new Components.Atlas.Either<Components.Controls.ControlStyle, string>(Components.Controls.ControlStyle.Auto)
            },
          AzureMapsControl.Components.Controls.ControlPosition.TopRight,
          AzureMapsControl.Components.FullScreen.FullScreenEventActivationFlags.All());

            fullScreenControl.OnFullScreenChanged += async isFullScreen => {
                if(isFullScreen) 
                {
                    await eventArgs.Map.SetStyleOptionsAsync(options => options.Style = MapStyle.GrayscaleDark);
                } 
                else
                {
                    await eventArgs.Map.SetStyleOptionsAsync(options => options.Style = MapStyle.GrayscaleLight);
                }
      };

            await eventArgs.Map.AddControlsAsync(fullScreenControl);
        }
    }
}
```