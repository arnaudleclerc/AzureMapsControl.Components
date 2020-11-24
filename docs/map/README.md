## Map

![Custom Azure Map](../assets/map.png) 

You can use the `AzureMap` component to display a map.

```
@page "/"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center= new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534), Zoom= 10 }"
          StyleOptions="StyleOptions"/>

@code {
    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = "grayscale_dark",
        ShowLogo = false,
        ShowFeedbackLink = false
    };
}
```

Different inputs can be specified to customize the map. Please refer to the [Azure Maps Web SDK Documentation](https://docs.microsoft.com/en-us/azure/azure-maps/map-create) for more information.

## Retrieve the map

An instance of the `Map` class is created whenever the `ready` event of the map has been created. You can retrieve this instance either on the events triggered by the map, either by using the instance of `IMapService` from the DI container.

### React to map events

The events of the map are exposed as `EventCallback` on the `AzureMap` component. The following events are implemented : 

| Native event key | AzureMap event | Description |
| -- | -- | -- |
| `boxzoomend` | `OnBoxZoomEnd` | Fired when a "box zoom" interaction ends. |
| `boxzoomstart` | `OnBoxZoomStart` | Fired when a "box zoom" interaction starts. | 
| `click` | `OnClick` | Fired when a pointing device is pressed and released at the same point on the map. |
| `contextmenu` | `OnContextMenu` | Fired when the right button of the mouse is clicked. |
| `data` | `OnData` | Fired when any map data loads or changes. |
| `dblclick` | `OnDblClick` | Fired when a pointing device is clicked twice at the same point on the map. |
| `drag` | `OnDrag` | Fired repeatedly during a "drag to pan" interaction on the map, popup, or HTML marker. |
| `dragend` | `OnDragEnd` | Fired when a "drag to pan" interaction ends on the map, popup, or HTML marker. |
| `dragstart` | `OnDragStart` | Fired when a "drag to pan" interaction starts on the map, popup, or HTML marker. |
| `error` | `OnError` | Fired when an error occurs. |
| `idle` | `OnIdle` | <p>Fired after the last frame rendered before the map enters an "idle" state:<ul><li>No camera transitions are in progress.</li><li>All currently requested tiles have loaded.</li><li>All fade/transition animations have completed.</li></ul></p> |
| `layeradded` | `OnLayerAdded` | Fired after a layer has been added to the map |
| `layerremoved` | `OnLayerRemoved` | Fired after a layer has been removed from the map |
| `load` | `OnLoad` | Fired immediately after all necessary resources have been downloaded and the first visually complete rendering of the map has occurred.
| `mousedown` | `OnMouseDown` | Fired when a pointing device is pressed within the map or when on top of an element. |
| `mousemove` | `OnMouseMove` | Fired when a pointing device is moved within the map or an element. |
| `mouseout` | `OnMouseOut` | Fired when a point device leaves the map's canvas our leaves an element. |
| `mouseover` | `OnMouseOver` | Fired when a pointing device is moved over the map or an element. |
| `mouseup` | `OnMouseUp` | Fired when a pointing device is released within the map or when on top of an element. |
| `move` | `OnMove` | Fired repeatedly during an animated transition from one view to another, as the result of either user interaction or methods. |
| `moveend` | `OnMoveEnd` | Fired just after the map completes a transition from one view to another, as the result of either user interaction or methods. |
| `movestart` | `OnMoveStart` | Fired just before the map begins a transition from one view to another, as the result of either user interaction or methods. |
| `pitch` | `OnPitch` | Fired whenever the map's pitch (tilt) changes as the result of either user interaction or methods. |
| `pitchend` | `OnPitchEnd` | Fired immediately after the map's pitch (tilt) finishes changing as the result of either user interaction or methods. |
| `pitchstart` | `OnPitchStart` | Fired whenever the map's pitch (tilt) begins a change as the result of either user interaction or methods. |
| `ready` | `OnReady` | Fired when the minimum required map resources are loaded before the map is ready to be programmatically interacted with. |
| `render` | `OnRender` | <p>Fired whenever the map is drawn to the screen, as the result of:<ul><li>A change to the map's position, zoom, pitch, or bearing.</li><li>A change to the map's style.</li><li>A change to a `DataSource` source.</li><li>The loading of a vector tile, GeoJSON file, glyph, or sprite.</li></ul></p> |
| `resize` | `OnResize` | Fired immediately after the map has been resized. |
| `rotate` | `OnRotate` | Fired repeatedly during a "drag to rotate" interaction. |
| `rotateend` | `OnRotateEnd` | Fired when a "drag to rotate" interaction ends. |
| `rotatestart` | `OnRotateStart` | Fired when a "drag to rotate" interaction starts. |
| `sourceadded` | `OnSourceAdded` | Fired when a DataSource or VectorTileSource is added to the map. |
| `sourcedata` | `OnSourceData` | Fired when one of the map's sources loads or changes, including if a tile belonging to a source loads or changes. |
| `sourceremoved` | `OnSourceRemoved` | Fired when a DataSource or VectorTileSource is removed from the map. |
| `styledata` | `OnStyleData` | Fired when the map's style loads or changes. |
| `styleimagemissing` | `OnStyleImageMissing` | Fired when a layer tries to load an image from the image sprite that doesn't exist |
| `tokenacquired` | `OnTokenAcquired` | Fired when an AAD access token is obtained. |
| `touchcancel` | `OnTouchCancel` | Fired when a touchcancel event occurs within the map. |
| `touchend` | `OnTouchEnd` | Fired when a touchend event occurs within the map. |
| `touchmove` | `OnTouchMove` | Fired when a touchmove event occurs within the map. |
| `touchstart` | `OnTouchStart` | Fired when a touchstart event occurs within the map. |
| `wheel` | `OnWheel` | Fired when a mouse wheel event occurs within the map. |
| `zoom` | `OnZoom` | Fired repeatedly during an animated transition from one zoom level to another, as the result of either user interaction or methods. |
| `zoomend` | `OnZoomEnd` | Fired just after the map completes a transition from one zoom level to another, as the result of either user interaction or methods. |
| `zoomstart` | `OnZoomStart` | Fired just before the map begins a transition from one zoom level to another, as the result of either user interaction or methods. |

Those events are also triggered by the `Map` property on `IMapService`.

### Activate or deactivate events

It is possible and recommended to active only the events you need, or at least deactivate the events which are often triggered in order to avoid overflowing the WebSocket communication between the component and the server application. The `AzureMap` component accepts an `EventActivationFlags` parameter where you can define which events should be triggered by the map. By default, all events except `Ready` are deactivated.

The following example activates all the events except `Drag`, `Idle`, `MouseMove` and `Render`.

```
@page "/"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center= new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534), Zoom= 10 }"
          StyleOptions="StyleOptions"
          OnReady="@(async (e) => { Console.WriteLine(e.Type); })"
          EventActivationFlags="MapEventActivationFlags
                                .All()
                                .Disable(MapEventType.Drag,
                                    MapEventType.Idle,
                                    MapEventType.MouseMove,
                                    MapEventType.Render)"/>

@code {
    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = "grayscale_dark",
        ShowLogo = false,
        ShowFeedbackLink = false
    };
}
```

The following example deactivates all the events except `Ready`.

```
@page "/"

@using AzureMapsControl.Components.Map
<AzureMap Id="map"
          CameraOptions="new CameraOptions { Center= new AzureMapsControl.Components.Atlas.Position(11.581990, 48.143534), Zoom= 10 }"
          StyleOptions="StyleOptions"
          OnReady="@(async (e) => { Console.WriteLine(e.Type); })"
          EventActivationFlags="MapEventActivationFlags
                                .None()
                                .Enable(MapEventType.Ready)"/>

@code {
    public StyleOptions StyleOptions = new StyleOptions
    {
        Style = "grayscale_dark",
        ShowLogo = false,
        ShowFeedbackLink = false
    };
}
```