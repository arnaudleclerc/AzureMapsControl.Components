namespace AzureMapsControl.Components.Tests.Map
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Drawing;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Markers;
    using AzureMapsControl.Components.Popups;

    using Xunit;

    public class MapTests
    {
        [Fact]
        public void Should_BeInitialized()
        {
            const string id = "id";
            var map = new Components.Map.Map(id);
            Assert.Equal(id, map.Id);
            Assert.Null(map.Controls);
            Assert.Null(map.HtmlMarkers);
            Assert.Null(map.DrawingToolbarOptions);
            Assert.Null(map.Layers);
            Assert.Null(map.Sources);
            Assert.Null(map.Popups);
        }

        [Fact]
        public async void Should_AddControls_Async()
        {
            var assertControlsCallback = false;
            var controls = new List<Control> {
                new Control(ControlType.Compass, ControlPosition.BottomLeft)
            };
            var map = new Components.Map.Map("id", addControlsCallback: async callbacksControls => assertControlsCallback = callbacksControls == controls);

            await map.AddControlsAsync(controls);
            Assert.True(assertControlsCallback);
            Assert.Equal(controls, map.Controls);
        }

        [Fact]
        public async void Should_AddControls_ParamsVersion_Async()
        {
            var assertControlsCallback = false;
            var control = new Control(ControlType.Compass, ControlPosition.BottomLeft);
            var map = new Components.Map.Map("id", addControlsCallback: async callbacksControls => assertControlsCallback = callbacksControls.SingleOrDefault() == control);

            await map.AddControlsAsync(control);
            Assert.True(assertControlsCallback);
            Assert.Single(map.Controls);
            Assert.Contains(control, map.Controls);
        }

        [Fact]
        public async void Should_AddHtmlMarkers_Async()
        {
            var assertHtmlMarkersCallback = false;
            var markers = new List<HtmlMarker> { new HtmlMarker(null), new HtmlMarker(null) };
            var map = new Components.Map.Map("id", addHtmlMarkersCallback: async callbackHtmlMarkers => assertHtmlMarkersCallback = callbackHtmlMarkers == markers);

            await map.AddHtmlMarkersAsync(markers);
            Assert.True(assertHtmlMarkersCallback);
            Assert.Contains(markers[0], map.HtmlMarkers);
            Assert.Contains(markers[1], map.HtmlMarkers);
        }

        [Fact]
        public async void Should_AddHtmlMarkers_ParamsVersion_Async()
        {
            var assertHtmlMarkersCallback = false;
            var marker1 = new HtmlMarker(null);
            var marker2 = new HtmlMarker(null);
            var map = new Components.Map.Map("id", addHtmlMarkersCallback: async callbackHtmlMarkers => assertHtmlMarkersCallback = callbackHtmlMarkers.Count() == 2 && callbackHtmlMarkers.Contains(marker1) && callbackHtmlMarkers.Contains(marker2));

            await map.AddHtmlMarkersAsync(marker1, marker2);
            Assert.True(assertHtmlMarkersCallback);
            Assert.Contains(marker1, map.HtmlMarkers);
            Assert.Contains(marker2, map.HtmlMarkers);
        }

        [Fact]
        public async void Should_UpdateHtmlMarkers_Async()
        {
            var assertUpdateCallback = false;
            var updates = new List<HtmlMarkerUpdate> { new HtmlMarkerUpdate(new HtmlMarker(null, null), null), new HtmlMarkerUpdate(new HtmlMarker(null, null), null) };
            var map = new Components.Map.Map("id", updateHtmlMarkersCallback: async updatesCallback => assertUpdateCallback = updates == updatesCallback);

            await map.UpdateHtmlMarkersAsync(updates);
            Assert.True(assertUpdateCallback);
        }

        [Fact]
        public async void Should_UpdateHtmlMarkers_ParamsVersion_Async()
        {
            var assertUpdateCallback = false;
            var update1 = new HtmlMarkerUpdate(new HtmlMarker(null, null), null);
            var update2 = new HtmlMarkerUpdate(new HtmlMarker(null, null), null);
            var map = new Components.Map.Map("id", updateHtmlMarkersCallback: async updatesCallback => assertUpdateCallback = updatesCallback.Count() == 2 && updatesCallback.Contains(update1) && updatesCallback.Contains(update2));

            await map.UpdateHtmlMarkersAsync(update1, update2);
            Assert.True(assertUpdateCallback);
        }

        [Fact]
        public async void Shoud_NotRemoveAnyHtmlMarkers_Async()
        {
            var assertRemoveCallback = false;
            var map = new Components.Map.Map("id", null, async markersAddCallback => { }, null, async markersRemoveCallback => assertRemoveCallback = true, null, null, null, null, null);

            await map.RemoveHtmlMarkersAsync(new List<HtmlMarker> { new HtmlMarker(null) });
            Assert.False(assertRemoveCallback);
        }

        [Fact]
        public async void Shoud_NotRemoveAnyHtmlMarkers_Null_Async()
        {
            var assertRemoveCallback = false;
            var map = new Components.Map.Map("id", addHtmlMarkersCallback: async markersAddCallback => { }, removeHtmlMarkersCallback: async markersRemoveCallback => assertRemoveCallback = true);
            var htmlMarker = new HtmlMarker(null);
            await map.AddHtmlMarkersAsync(htmlMarker);

            await map.RemoveHtmlMarkersAsync(null);
            Assert.False(assertRemoveCallback);
        }

        [Fact]
        public async void Shoud_RemoveAnyHtmlMarkers_Async()
        {
            var assertRemoveCallback = false;
            var htmlMarker = new HtmlMarker(null);
            var htmlMarker2 = new HtmlMarker(null);

            var map = new Components.Map.Map("id", addHtmlMarkersCallback: async markersAddCallback => { },
                removeHtmlMarkersCallback: async markersRemoveCallback => assertRemoveCallback = markersRemoveCallback.Single() == htmlMarker);
            await map.AddHtmlMarkersAsync(new List<HtmlMarker> { htmlMarker, htmlMarker2 });

            await map.RemoveHtmlMarkersAsync(htmlMarker);
            Assert.True(assertRemoveCallback);
            Assert.DoesNotContain(htmlMarker, map.HtmlMarkers);
            Assert.Contains(htmlMarker2, map.HtmlMarkers);
        }

        [Fact]
        public async void Shoud_RemoveAnyHtmlMarkers_ParamsVersion_Async()
        {
            var assertRemoveCallback = false;
            var htmlMarker = new HtmlMarker(null);
            var htmlMarker2 = new HtmlMarker(null);

            var map = new Components.Map.Map("id", addHtmlMarkersCallback: async markersAddCallback => { },
                removeHtmlMarkersCallback: async markersRemoveCallback => assertRemoveCallback = markersRemoveCallback.Single() == htmlMarker);
            await map.AddHtmlMarkersAsync(htmlMarker, htmlMarker2);

            await map.RemoveHtmlMarkersAsync(htmlMarker);
            Assert.True(assertRemoveCallback);
            Assert.DoesNotContain(htmlMarker, map.HtmlMarkers);
            Assert.Contains(htmlMarker2, map.HtmlMarkers);
        }

        [Fact]
        public async void Should_AddDrawingToolbar_Async()
        {
            var assertAddDrawingToolbarCallback = false;
            var drawingToolbarOptions = new DrawingToolbarOptions();
            var map = new Components.Map.Map("id", addDrawingToolbarCallback: async addDrawingCallback => assertAddDrawingToolbarCallback = addDrawingCallback == drawingToolbarOptions);
            await map.AddDrawingToolbarAsync(drawingToolbarOptions);

            Assert.True(assertAddDrawingToolbarCallback);
            Assert.Equal(drawingToolbarOptions, map.DrawingToolbarOptions);
        }

        [Fact]
        public async void Should_UpdateDrawingToolbar_Async()
        {
            var assertUpdateDrawingToolbarCallback = false;
            var drawingToolbarOptions = new DrawingToolbarOptions();
            var updateDrawingToolbarOptions = new DrawingToolbarUpdateOptions {
                Buttons = new List<DrawingButton>(),
                ContainerId = "containerId",
                NumColumns = 2,
                Position = ControlPosition.BottomLeft,
                Style = DrawingToolbarStyle.Dark,
                Visible = false
            };
            var map = new Components.Map.Map("id", addDrawingToolbarCallback: async addDrawingCallback => { }, updateDrawingToolbarCallback: async updateDrawingCallback => assertUpdateDrawingToolbarCallback = updateDrawingCallback == updateDrawingToolbarOptions);
            await map.AddDrawingToolbarAsync(drawingToolbarOptions);
            await map.UpdateDrawingToolbarAsync(updateDrawingToolbarOptions);

            Assert.True(assertUpdateDrawingToolbarCallback);
            Assert.Equal(updateDrawingToolbarOptions.Buttons, map.DrawingToolbarOptions.Buttons);
            Assert.Equal(updateDrawingToolbarOptions.ContainerId, map.DrawingToolbarOptions.ContainerId);
            Assert.Equal(updateDrawingToolbarOptions.NumColumns, map.DrawingToolbarOptions.NumColumns);
            Assert.Equal(updateDrawingToolbarOptions.Position, map.DrawingToolbarOptions.Position);
            Assert.Equal(updateDrawingToolbarOptions.Style, map.DrawingToolbarOptions.Style);
            Assert.Equal(updateDrawingToolbarOptions.Visible, map.DrawingToolbarOptions.Visible);
        }

        [Fact]
        public async void Should_RemoveDrawingToolbar_Async()
        {
            var assertRemoveDrawingToolbarCallback = false;
            var map = new Components.Map.Map("id", addDrawingToolbarCallback: async addDrawingCallback => { }, removeDrawingToolbarCallback: async () => assertRemoveDrawingToolbarCallback = true);

            await map.AddDrawingToolbarAsync(new DrawingToolbarOptions());
            await map.RemoveDrawingToolbarAsync();

            Assert.True(assertRemoveDrawingToolbarCallback);
            Assert.Null(map.DrawingToolbarOptions);
        }

        [Fact]
        public async void Should_NotRemoveDrawingToolbar_Async()
        {
            var assertRemoveDrawingToolbarCallback = false;
            var map = new Components.Map.Map("id", removeDrawingToolbarCallback: async () => assertRemoveDrawingToolbarCallback = true);

            await map.RemoveDrawingToolbarAsync();

            Assert.False(assertRemoveDrawingToolbarCallback);
            Assert.Null(map.DrawingToolbarOptions);
        }

        [Fact]
        public async void Should_AddALayer_Async()
        {
            var assertAddLayerCallback = false;
            var layer = new BubbleLayer();
            var map = new Components.Map.Map("id", addLayerCallback: async (layerCallback, beforeCallback) => assertAddLayerCallback = layerCallback == layer && beforeCallback == null);

            await map.AddLayerAsync(layer);
            Assert.True(assertAddLayerCallback);
            Assert.Contains(layer, map.Layers);
        }

        [Fact]
        public async void Should_AddALayerWithBefore_Async()
        {
            var assertAddLayerCallback = false;
            var layer = new BubbleLayer();
            const string before = "before";
            var map = new Components.Map.Map("id", addLayerCallback: async (layerCallback, beforeCallback) => assertAddLayerCallback = layerCallback == layer && beforeCallback == before);

            await map.AddLayerAsync(layer, before);
            Assert.True(assertAddLayerCallback);
            Assert.Contains(layer, map.Layers);
        }

        [Fact]
        public async void Should_NotAddLayerWithSameId_Async()
        {
            var layer = new BubbleLayer();
            var map = new Components.Map.Map("id", addLayerCallback: async (layerCallback, beforeCallback) => { });

            await map.AddLayerAsync(layer);
            await Assert.ThrowsAnyAsync<LayerAlreadyAddedException>(async () => await map.AddLayerAsync(layer));
        }

        [Fact]
        public async void Should_RemoveOneLayer_Async()
        {
            var assertRemoveLayerCallback = false;
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Components.Map.Map("id", addLayerCallback: async (layerCallback, beforeCallback) => { }, removeLayersCallback: async layers => assertRemoveLayerCallback = layers.Single() == layer1.Id);
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(layer1);

            Assert.True(assertRemoveLayerCallback);
            Assert.DoesNotContain(layer1, map.Layers);
            Assert.Contains(layer2, map.Layers);
        }

        [Fact]
        public async void Should_RemoveMultipleLayers_Async()
        {
            var assertRemoveLayerCallback = false;
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Components.Map.Map("id", addLayerCallback: async (layerCallback, beforeCallback) => { }, removeLayersCallback: async layers => assertRemoveLayerCallback = layers.Contains(layer1.Id) && layers.Contains(layer2.Id));
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(new List<Layer> { layer1, layer2 });

            Assert.True(assertRemoveLayerCallback);
            Assert.DoesNotContain(layer1, map.Layers);
            Assert.DoesNotContain(layer2, map.Layers);
        }

        [Fact]
        public async void Should_RemoveMultipleLayers_ParamsVersion_Async()
        {
            var assertRemoveLayerCallback = false;
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Components.Map.Map("id", addLayerCallback: async (layerCallback, beforeCallback) => { }, removeLayersCallback: async layers => assertRemoveLayerCallback = layers.Contains(layer1.Id) && layers.Contains(layer2.Id));
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(layer1, layer2);

            Assert.True(assertRemoveLayerCallback);
            Assert.DoesNotContain(layer1, map.Layers);
            Assert.DoesNotContain(layer2, map.Layers);
        }

        [Fact]
        public async void Should_RemoveMultipleLayers_ViaId_Async()
        {
            var assertRemoveLayerCallback = false;
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Components.Map.Map("id", addLayerCallback: async (layerCallback, beforeCallback) => { }, removeLayersCallback: async layers => assertRemoveLayerCallback = layers.Contains(layer1.Id) && layers.Contains(layer2.Id));
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(new List<string> { layer1.Id, layer2.Id });

            Assert.True(assertRemoveLayerCallback);
            Assert.DoesNotContain(layer1, map.Layers);
            Assert.DoesNotContain(layer2, map.Layers);
        }

        [Fact]
        public async void Should_RemoveMultipleLayers_ViaId_ParamsVersion_Async()
        {
            var assertRemoveLayerCallback = false;
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Components.Map.Map("id", addLayerCallback: async (layerCallback, beforeCallback) => { }, removeLayersCallback: async layers => assertRemoveLayerCallback = layers.Contains(layer1.Id) && layers.Contains(layer2.Id));
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(layer1.Id, layer2.Id);

            Assert.True(assertRemoveLayerCallback);
            Assert.DoesNotContain(layer1, map.Layers);
            Assert.DoesNotContain(layer2, map.Layers);
        }

        [Fact]
        public async void Should_AddDataSource_Async()
        {
            var assertaddSourceCallback = false;
            var assertAttachEventsCallback = false;
            var dataSource = new DataSource();

            var map = new Components.Map.Map("id", addSourceCallback: async dataSourceCallback => assertaddSourceCallback = Equals(dataSource, dataSourceCallback), attachDataSourceEventsCallback: async (dataSourceCallback) => assertAttachEventsCallback = Equals(dataSource, dataSourceCallback));
            await map.AddSourceAsync(dataSource);

            Assert.True(assertaddSourceCallback);
            Assert.True(assertAttachEventsCallback);
            Assert.Single(map.Sources, dataSource);
        }

        [Fact]
        public async void Should_AddVectorTileSourceAsync()
        {
            var assertaddSourceCallback = false;
            var assertAttachEventsCallback = false;
            var source = new VectorTileSource();

            var map = new Components.Map.Map("id", addSourceCallback: async dataSourceCallback => assertaddSourceCallback = Equals(source, dataSourceCallback), attachDataSourceEventsCallback: async (dataSourceCallback) => assertAttachEventsCallback = Equals(source, dataSourceCallback));
            await map.AddSourceAsync(source);

            Assert.True(assertaddSourceCallback);
            Assert.False(assertAttachEventsCallback);
            Assert.Single(map.Sources, source);
        }

        [Fact]
        public async void Should_NotAddDataSource_Async()
        {
            var assertaddSourceCallback = false;
            var assertAttachEventsCallback = false;
            var dataSource = new DataSource();

            var map = new Components.Map.Map("id", addSourceCallback: async dataSourceCallback => assertaddSourceCallback = dataSource == dataSourceCallback, attachDataSourceEventsCallback: async (dataSourceCallback) => assertAttachEventsCallback = Equals(dataSource, dataSourceCallback));
            await map.AddSourceAsync(dataSource);
            await Assert.ThrowsAnyAsync<SourceAlreadyExistingException>(async () => await map.AddSourceAsync(dataSource));
        }

        [Fact]
        public async void Should_RemoveDataSource_Async()
        {
            var assertremoveSourceCallback = false;
            var dataSource = new DataSource();
            var dataSource2 = new DataSource();
            var map = new Components.Map.Map("id", addSourceCallback: async dataSourceCallback => { }, removeSourceCallback: async removeSource => assertremoveSourceCallback = removeSource == dataSource.Id, attachDataSourceEventsCallback: async (dataSourceCallback) => { });

            await map.AddSourceAsync(dataSource);
            await map.AddSourceAsync(dataSource2);
            await map.RemoveDataSourceAsync(dataSource);

            Assert.True(assertremoveSourceCallback);
            Assert.DoesNotContain(dataSource, map.Sources);
            Assert.Contains(dataSource2, map.Sources);
        }

        [Fact]
        public async void Should_NotRemoveDataSource_Async()
        {
            var assertremoveSourceCallback = false;
            var dataSource = new DataSource();
            var dataSource2 = new DataSource();
            var map = new Components.Map.Map("id", addSourceCallback: async dataSourceCallback => { }, removeSourceCallback: async removeSourceCallback => { }, attachDataSourceEventsCallback: async (dataSourceCallback) => { });

            await map.AddSourceAsync(dataSource);
            await map.RemoveDataSourceAsync(dataSource2);

            Assert.False(assertremoveSourceCallback);
            Assert.DoesNotContain(dataSource2, map.Sources);
            Assert.Contains(dataSource, map.Sources);
        }

        [Fact]
        public async void Should_RemoveDataSource_ViaId_Async()
        {
            var assertremoveSourceCallback = false;
            var dataSource = new DataSource();
            var dataSource2 = new DataSource();
            var map = new Components.Map.Map("id", addSourceCallback: async dataSourceCallback => { }, removeSourceCallback: async removeSourceCallback => assertremoveSourceCallback = removeSourceCallback == dataSource.Id, attachDataSourceEventsCallback: async _ => { });

            await map.AddSourceAsync(dataSource);
            await map.AddSourceAsync(dataSource2);
            await map.RemoveDataSourceAsync(dataSource.Id);

            Assert.True(assertremoveSourceCallback);
            Assert.DoesNotContain(dataSource, map.Sources);
            Assert.Contains(dataSource2, map.Sources);
        }

        [Fact]
        public async void Should_NotRemoveDataSource_ViaId_Async()
        {
            var assertremoveSourceCallback = false;
            var dataSource = new DataSource();
            var dataSource2 = new DataSource();
            var map = new Components.Map.Map("id", addSourceCallback: async dataSourceCallback => { }, removeSourceCallback: async removeSourceCallback => { }, attachDataSourceEventsCallback: async _ => { });

            await map.AddSourceAsync(dataSource);
            await map.RemoveDataSourceAsync(dataSource2.Id);

            Assert.False(assertremoveSourceCallback);
            Assert.DoesNotContain(dataSource2, map.Sources);
            Assert.Contains(dataSource, map.Sources);
        }

        [Fact]
        public async void Should_ClearMap_Async()
        {
            var assertClearMapCallback = false;
            var map = new Components.Map.Map("id", addHtmlMarkersCallback: async _ => { }, addLayerCallback: async (_, before) => { }, addSourceCallback: async _ => { }, addPopupCallback: async _ => { }, clearMapCallback: async () => assertClearMapCallback = true, attachDataSourceEventsCallback: async _ => { });
            await map.AddSourceAsync(new DataSource());
            await map.AddLayerAsync(new BubbleLayer());
            await map.AddHtmlMarkersAsync(new HtmlMarker(null));
            await map.AddPopupAsync(new Popup(new PopupOptions()));

            await map.ClearMapAsync();
            Assert.True(assertClearMapCallback);
            Assert.Null(map.Sources);
            Assert.Null(map.Layers);
            Assert.Null(map.HtmlMarkers);
            Assert.Null(map.Popups);
        }

        [Fact]
        public async void Should_ClearLayers_Async()
        {
            var assertClearLayersCallback = false;
            var map = new Components.Map.Map("id", addLayerCallback: async (_, before) => { }, clearLayersCallback: async () => assertClearLayersCallback = true);
            await map.AddLayerAsync(new BubbleLayer());

            await map.ClearLayersAsync();
            Assert.True(assertClearLayersCallback);
            Assert.Null(map.Layers);
        }

        [Fact]
        public async void Should_ClearDataSources_Async()
        {
            var assertClearDataSourcesCallback = false;
            var map = new Components.Map.Map("id", addSourceCallback: async _ => { }, clearSourcesCallback: async () => assertClearDataSourcesCallback = true, attachDataSourceEventsCallback: _ => { });

            await map.AddSourceAsync(new DataSource());
            await map.ClearDataSourcesAsync();
            Assert.True(assertClearDataSourcesCallback);
            Assert.Null(map.Sources);
        }

        [Fact]
        public async void Should_ClearHtmlMarkers_Async()
        {
            var assertClearHtmlMarkers = false;
            var map = new Components.Map.Map("id", addHtmlMarkersCallback: async _ => { }, clearHtmlMarkersCallback: async () => assertClearHtmlMarkers = true);

            await map.AddHtmlMarkersAsync(new HtmlMarker(null));
            await map.ClearHtmlMarkersAsync();

            Assert.True(assertClearHtmlMarkers);
            Assert.Null(map.HtmlMarkers);
        }

        [Fact]
        public async void Should_AddPopup_Async()
        {
            var assertAddPopup = false;
            var popup = new Popup(new PopupOptions());
            var map = new Components.Map.Map("id", addPopupCallback: async popupCallback => assertAddPopup = popupCallback == popup);
            await map.AddPopupAsync(popup);
            Assert.True(assertAddPopup);
            Assert.Contains(popup, map.Popups);
        }

        [Fact]
        public async void Should_NotAddTwiceTheSamePopup_Async()
        {
            var popup = new Popup(new PopupOptions());
            var map = new Components.Map.Map("id", addPopupCallback: async _ => { });
            await map.AddPopupAsync(popup);
            await Assert.ThrowsAnyAsync<PopupAlreadyExistingException>(async () => await map.AddPopupAsync(popup));
        }

        [Fact]
        public async void Should_RemovePopupFromPopupsCollection_Async()
        {
            var popup = new Popup(new PopupOptions());
            var map = new Components.Map.Map("id", addPopupCallback: async _ => { });
            await map.AddPopupAsync(popup);
            map.RemovePopup(popup.Id);

            Assert.DoesNotContain(popup, map.Popups);
        }

        [Fact]
        public async void Should_RemovePopup_Async()
        {
            var assertRemoveCallback = false;
            var popup = new Popup(new PopupOptions());
            var map = new Components.Map.Map("id", addPopupCallback: async _ => { }, removePopupCallback: async popupId => assertRemoveCallback = popupId == popup.Id);
            await map.AddPopupAsync(popup);
            await map.RemovePopupAsync(popup);

            Assert.True(assertRemoveCallback);
            Assert.DoesNotContain(popup, map.Popups);
        }

        [Fact]
        public async void Should_RemovePopup_IdVersion_Async()
        {
            var assertRemoveCallback = false;
            var popup = new Popup(new PopupOptions());
            var map = new Components.Map.Map("id", addPopupCallback: async _ => { }, removePopupCallback: async popupId => assertRemoveCallback = popupId == popup.Id);
            await map.AddPopupAsync(popup);
            await map.RemovePopupAsync(popup.Id);

            Assert.True(assertRemoveCallback);
            Assert.DoesNotContain(popup, map.Popups);
        }

        [Fact]
        public async void Should_NotRemovePopup_Async()
        {
            var assertRemoveCallback = false;
            var popup = new Popup(new PopupOptions());
            var map = new Components.Map.Map("id", addPopupCallback: async _ => { }, removePopupCallback: async popupId => assertRemoveCallback = true);
            await map.AddPopupAsync(popup);
            await map.RemovePopupAsync(new Popup(new PopupOptions()));

            Assert.False(assertRemoveCallback);
        }

        [Fact]
        public async void Should_ClearPopups_Async()
        {
            var assertClearCallback = false;
            var popup = new Popup(new PopupOptions());
            var map = new Components.Map.Map("id", addPopupCallback: async _ => { }, clearPopupsCallback: async () => assertClearCallback = true);
            await map.AddPopupAsync(popup);
            await map.ClearPopupsAsync();

            Assert.True(assertClearCallback);
            Assert.Null(map.Popups);
        }

        [Fact]
        public async void Should_UpdateCameraOptions_Async()
        {
            var assertOptionsCallback = false;
            var center = new Position(10, 10);
            var initialCameraOptions = new CameraOptions {
                Duration = 10
            };
            var map = new Map("id", setCameraCallback: async options => assertOptionsCallback = options.Center == center && options.Duration == initialCameraOptions.Duration) {
                CameraOptions = initialCameraOptions
            };

            await map.SetCameraOptionsAsync(options => options.Center = center);
            Assert.True(assertOptionsCallback);
        }

        [Fact]
        public async void Should_UpdateStyleOptions_Async()
        {
            var assertOptionsCallback = false;
            var language = "fr";
            var initialStyleOptions = new StyleOptions {
                AutoResize = true
            };
            var map = new Map("id", setStyleCallback: async options => assertOptionsCallback = options.AutoResize == initialStyleOptions.AutoResize && options.Language == language) {
                StyleOptions = initialStyleOptions
            };

            await map.SetStyleOptionsAsync(options => options.Language = language);
            Assert.True(assertOptionsCallback);
        }
    }
}
