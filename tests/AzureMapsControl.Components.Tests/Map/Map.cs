namespace AzureMapsControl.Components.Tests.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Drawing;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Markers;
    using AzureMapsControl.Components.Popups;
    using AzureMapsControl.Components.Runtime;
    using AzureMapsControl.Components.Traffic;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    using Moq;

    using NuGet.Frameworks;

    using Xunit;

    public class MapTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();
        private readonly Mock<ILogger> _loggerMock = new();

        [Fact]
        public void Should_BeInitialized()
        {
            const string id = "id";
            var map = new Map(id, _jsRuntimeMock.Object, _loggerMock.Object);
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
            var controls = new List<Control> {
                new CompassControl()
            };

            const string id = "id";
            var map = new Map(id, _jsRuntimeMock.Object, _loggerMock.Object);

            await map.AddControlsAsync(controls);
            Assert.Equal(controls, map.Controls);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddControls.ToCoreNamespace(), It.Is<IOrderedEnumerable<Control>>(
                ctrls => ctrls.Single() == controls.Single())), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddOrderedControls_Async()
        {
            var controls = new List<Control> {
                new OverviewMapControl(),
                new CompassControl()
            };

            const string id = "id";
            var map = new Map(id, _jsRuntimeMock.Object, _loggerMock.Object);

            await map.AddControlsAsync(controls);
            Assert.Equal(controls, map.Controls);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddControls.ToCoreNamespace(), It.Is<IOrderedEnumerable<Control>>(
                ctrls => ctrls.First() == controls.ElementAt(1) && ctrls.ElementAt(1) == controls.First())), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddControls_NullCaseAsync()
        {
            const string id = "id";
            var map = new Map(id, _jsRuntimeMock.Object, _loggerMock.Object);

            await map.AddControlsAsync(null);
            Assert.Null(map.Controls);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddControls_EmptyCaseAsync()
        {
            var controls = Array.Empty<Control>();
            const string id = "id";
            var map = new Map(id, _jsRuntimeMock.Object, _loggerMock.Object);

            await map.AddControlsAsync(controls);
            Assert.Null(map.Controls);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddControls_ParamsVersion_Async()
        {
            var control = new CompassControl(position: ControlPosition.BottomLeft);
            const string id = "id";
            var map = new Map(id, _jsRuntimeMock.Object, _loggerMock.Object);

            await map.AddControlsAsync(control);
            Assert.Single(map.Controls);
            Assert.Contains(control, map.Controls);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddControls.ToCoreNamespace(), It.Is<IOrderedEnumerable<Control>>(
                ctrls => ctrls.Single() == control)), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddHtmlMarkers_Async()
        {
            var markers = new List<HtmlMarker> { new HtmlMarker(null), new HtmlMarker(null) };
            var popupInvokeHelper = new PopupInvokeHelper(null);
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(eventArgs => ValueTask.CompletedTask), popupInvokeHelper: popupInvokeHelper);

            await map.AddHtmlMarkersAsync(markers);
            Assert.Contains(markers[0], map.HtmlMarkers);
            Assert.Contains(markers[1], map.HtmlMarkers);
            Assert.Equal(markers[0].JSRuntime, _jsRuntimeMock.Object);
            Assert.Equal(markers[1].JSRuntime, _jsRuntimeMock.Object);
            Assert.Equal(markers[0].PopupInvokeHelper, popupInvokeHelper);
            Assert.Equal(markers[1].PopupInvokeHelper, popupInvokeHelper);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<HtmlMarkerCreationOptions> && (parameters[0] as IEnumerable<HtmlMarkerCreationOptions>).Count() == 2
                && parameters[1] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddHtmlMarkers_WithPopup_WithAutoOpenAsync()
        {
            var assertEvent = false;
            var popup = new HtmlMarkerPopup(new PopupOptions {
                OpenOnAdd = true
            });
            var marker = new HtmlMarker(new HtmlMarkerOptions {
                Popup = popup
            });

            marker.OnPopupToggled += () => assertEvent = true;

            var popupInvokeHelper = new PopupInvokeHelper(null);
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(eventArgs => ValueTask.CompletedTask), popupInvokeHelper: popupInvokeHelper);

            await map.AddHtmlMarkersAsync(marker);
            Assert.True(assertEvent);
            Assert.Contains(marker, map.HtmlMarkers);
            Assert.Equal(marker.JSRuntime, _jsRuntimeMock.Object);
            Assert.Equal(marker.PopupInvokeHelper, popupInvokeHelper);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<HtmlMarkerCreationOptions> && (parameters[0] as IEnumerable<HtmlMarkerCreationOptions>).Count() == 1
                && parameters[1] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.HtmlMarker.TogglePopup.ToHtmlMarkerNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == marker.Id
                && parameters[1] as string == marker.Options.Popup.Id
                && parameters[2] is IEnumerable<string>
                && parameters[3] is DotNetObjectReference<PopupInvokeHelper>
            )));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddHtmlMarkers_ParamsVersion_Async()
        {
            var marker1 = new HtmlMarker(null);
            var marker2 = new HtmlMarker(null);
            var popupInvokeHelper = new PopupInvokeHelper(null);
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(eventArgs => ValueTask.CompletedTask), popupInvokeHelper: popupInvokeHelper);

            await map.AddHtmlMarkersAsync(marker1, marker2);
            Assert.Contains(marker1, map.HtmlMarkers);
            Assert.Contains(marker2, map.HtmlMarkers);
            Assert.Equal(marker1.JSRuntime, _jsRuntimeMock.Object);
            Assert.Equal(marker2.JSRuntime, _jsRuntimeMock.Object);
            Assert.Equal(marker1.PopupInvokeHelper, popupInvokeHelper);
            Assert.Equal(marker2.PopupInvokeHelper, popupInvokeHelper);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<HtmlMarkerCreationOptions> && (parameters[0] as IEnumerable<HtmlMarkerCreationOptions>).Count() == 2
                && parameters[1] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateHtmlMarkers_Async()
        {
            var updates = new List<HtmlMarkerUpdate> { new HtmlMarkerUpdate(new HtmlMarker(null, null), null), new HtmlMarkerUpdate(new HtmlMarker(null, null), null) };
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);

            await map.UpdateHtmlMarkersAsync(updates);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.UpdateHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters.Single() is IEnumerable<HtmlMarkerCreationOptions>
                && (parameters.Single() as IEnumerable<HtmlMarkerCreationOptions>).Count() == updates.Count)
            ), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotUpdateHtmlMarkers_NullCaseAsync()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);

            await map.UpdateHtmlMarkersAsync(null);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateHtmlMarkers_ParamsVersion_Async()
        {
            var update1 = new HtmlMarkerUpdate(new HtmlMarker(null, null), null);
            var update2 = new HtmlMarkerUpdate(new HtmlMarker(null, null), null);
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);

            await map.UpdateHtmlMarkersAsync(update1, update2);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.UpdateHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                            parameters.Single() is IEnumerable<HtmlMarkerCreationOptions>
                            && (parameters.Single() as IEnumerable<HtmlMarkerCreationOptions>).Count() == 2)
                        ), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Shoud_NotRemoveAnyHtmlMarkers_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);

            await map.RemoveHtmlMarkersAsync(new List<HtmlMarker> { new HtmlMarker(null) });
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Shoud_NotRemoveAnyHtmlMarkers_Null_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(eventArgs => ValueTask.CompletedTask));
            var htmlMarker = new HtmlMarker(null);
            await map.AddHtmlMarkersAsync(htmlMarker);

            await map.RemoveHtmlMarkersAsync(null);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<HtmlMarkerCreationOptions> && (parameters[0] as IEnumerable<HtmlMarkerCreationOptions>).Count() == 1
                && parameters[1] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Shoud_RemoveAnyHtmlMarkers_Async()
        {
            var htmlMarker = new HtmlMarker(null);
            var htmlMarker2 = new HtmlMarker(null);

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddHtmlMarkersAsync(new List<HtmlMarker> { htmlMarker, htmlMarker2 });

            await map.RemoveHtmlMarkersAsync(htmlMarker);
            Assert.DoesNotContain(htmlMarker, map.HtmlMarkers);
            Assert.Contains(htmlMarker2, map.HtmlMarkers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<HtmlMarkerCreationOptions> && (parameters[0] as IEnumerable<HtmlMarkerCreationOptions>).Count() == 2
                && parameters[1] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters.Single() is IEnumerable<string> && (parameters[0] as IEnumerable<string>).Single() == htmlMarker.Id)
            ), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Shoud_RemoveAnyHtmlMarkers_ParamsVersion_Async()
        {
            var htmlMarker = new HtmlMarker(null);
            var htmlMarker2 = new HtmlMarker(null);

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddHtmlMarkersAsync(htmlMarker, htmlMarker2);

            await map.RemoveHtmlMarkersAsync(htmlMarker);
            Assert.DoesNotContain(htmlMarker, map.HtmlMarkers);
            Assert.Contains(htmlMarker2, map.HtmlMarkers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<HtmlMarkerCreationOptions> && (parameters[0] as IEnumerable<HtmlMarkerCreationOptions>).Count() == 2
                && parameters[1] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters.Single() is IEnumerable<string> && (parameters[0] as IEnumerable<string>).Single() == htmlMarker.Id)
            ), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddDrawingToolbar_Async()
        {
            var drawingToolbarOptions = new DrawingToolbarOptions();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, new DrawingToolbarEventInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddDrawingToolbarAsync(drawingToolbarOptions);

            Assert.Equal(drawingToolbarOptions, map.DrawingToolbarOptions);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Drawing.AddDrawingToolbar.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is DrawingToolbarCreationOptions
                && parameters[1] is DotNetObjectReference<DrawingToolbarEventInvokeHelper>
             )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateDrawingToolbar_Async()
        {
            var drawingToolbarOptions = new DrawingToolbarOptions();
            var updateDrawingToolbarOptions = new DrawingToolbarUpdateOptions {
                Buttons = new List<DrawingButton>(),
                ContainerId = "containerId",
                NumColumns = 2,
                Position = ControlPosition.BottomLeft,
                Style = DrawingToolbarStyle.Dark,
                Visible = false
            };
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, new DrawingToolbarEventInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddDrawingToolbarAsync(drawingToolbarOptions);
            await map.UpdateDrawingToolbarAsync(updateDrawingToolbarOptions);

            Assert.Equal(updateDrawingToolbarOptions.Buttons, map.DrawingToolbarOptions.Buttons);
            Assert.Equal(updateDrawingToolbarOptions.ContainerId, map.DrawingToolbarOptions.ContainerId);
            Assert.Equal(updateDrawingToolbarOptions.NumColumns, map.DrawingToolbarOptions.NumColumns);
            Assert.Equal(updateDrawingToolbarOptions.Position, map.DrawingToolbarOptions.Position);
            Assert.Equal(updateDrawingToolbarOptions.Style, map.DrawingToolbarOptions.Style);
            Assert.Equal(updateDrawingToolbarOptions.Visible, map.DrawingToolbarOptions.Visible);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Drawing.AddDrawingToolbar.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is DrawingToolbarCreationOptions
                && parameters[1] is DotNetObjectReference<DrawingToolbarEventInvokeHelper>
             )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Drawing.UpdateDrawingToolbar.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters.Single() is DrawingToolbarCreationOptions
             )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotUpdateDrawingToolbar_NullCaseAsync()
        {
            var drawingToolbarOptions = new DrawingToolbarOptions();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, new DrawingToolbarEventInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddDrawingToolbarAsync(drawingToolbarOptions);
            await map.UpdateDrawingToolbarAsync(null);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Drawing.AddDrawingToolbar.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is DrawingToolbarCreationOptions
                && parameters[1] is DotNetObjectReference<DrawingToolbarEventInvokeHelper>
             )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveDrawingToolbar_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, new DrawingToolbarEventInvokeHelper(eventArgs => ValueTask.CompletedTask));

            await map.AddDrawingToolbarAsync(new DrawingToolbarOptions());
            await map.RemoveDrawingToolbarAsync();

            Assert.Null(map.DrawingToolbarOptions);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Drawing.AddDrawingToolbar.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is DrawingToolbarCreationOptions
                && parameters[1] is DotNetObjectReference<DrawingToolbarEventInvokeHelper>
             )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Drawing.RemoveDrawingToolbar.ToDrawingNamespace()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemoveDrawingToolbar_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, new DrawingToolbarEventInvokeHelper(eventArgs => ValueTask.CompletedTask));

            await map.RemoveDrawingToolbarAsync();

            Assert.Null(map.DrawingToolbarOptions);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddALayer_Async()
        {
            var layer = new BubbleLayer();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));

            await map.AddLayerAsync(layer);
            Assert.Contains(layer, map.Layers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == layer.Id
                && parameters[1] == null
                && parameters[2] as string == layer.Type.ToString()
                && parameters[3] == null
                && parameters[4] is IEnumerable<string> && (parameters[4] as IEnumerable<string>).Count() == 0
                && parameters[5] is DotNetObjectReference<LayerEventInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddALayer_NullCaseAsync()
        {
            BubbleLayer layer = null;
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));

            await map.AddLayerAsync(layer);
            Assert.Null(map.Layers);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddALayerWithBefore_Async()
        {
            var layer = new BubbleLayer();
            const string before = "before";
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));

            await map.AddLayerAsync(layer, before);
            Assert.Contains(layer, map.Layers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == layer.Id
                && parameters[1] as string == before
                && parameters[2] as string == layer.Type.ToString()
                && parameters[3] == null
                && parameters[4] is IEnumerable<string> && (parameters[4] as IEnumerable<string>).Count() == 0
                && parameters[5] is DotNetObjectReference<LayerEventInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddLayerWithSameId_Async()
        {
            var layer = new BubbleLayer();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));

            await map.AddLayerAsync(layer);
            await Assert.ThrowsAnyAsync<LayerAlreadyAddedException>(async () => await map.AddLayerAsync(layer));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == layer.Id
                && parameters[1] == null
                && parameters[2] as string == layer.Type.ToString()
                && parameters[3] == null
                && parameters[4] is IEnumerable<string> && (parameters[4] as IEnumerable<string>).Count() == 0
                && parameters[5] is DotNetObjectReference<LayerEventInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveOneLayer_Async()
        {
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(layer1);

            Assert.DoesNotContain(layer1, map.Layers);
            Assert.Contains(layer2, map.Layers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.IsAny<object[]>()), Times.Exactly(2));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveLayers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<string>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveMultipleLayers_Async()
        {
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(new List<Layer> { layer1, layer2 });

            Assert.DoesNotContain(layer1, map.Layers);
            Assert.DoesNotContain(layer2, map.Layers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.IsAny<object[]>()), Times.Exactly(2));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveLayers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<string>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveMultipleLayers_ParamsVersion_Async()
        {
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(layer1, layer2);

            Assert.DoesNotContain(layer1, map.Layers);
            Assert.DoesNotContain(layer2, map.Layers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.IsAny<object[]>()), Times.Exactly(2));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveLayers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<string>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveMultipleLayers_ViaId_Async()
        {
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(new List<string> { layer1.Id, layer2.Id });

            Assert.DoesNotContain(layer1, map.Layers);
            Assert.DoesNotContain(layer2, map.Layers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.IsAny<object[]>()), Times.Exactly(2));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveLayers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<string>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveMultipleLayers_ViaId_ParamsVersion_Async()
        {
            var layer1 = new BubbleLayer();
            var layer2 = new BubbleLayer();

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddLayerAsync(layer1);
            await map.AddLayerAsync(layer2);
            await map.RemoveLayersAsync(layer1.Id, layer2.Id);

            Assert.DoesNotContain(layer1, map.Layers);
            Assert.DoesNotContain(layer2, map.Layers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.IsAny<object[]>()), Times.Exactly(2));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveLayers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<string>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddDataSource_Async()
        {
            var dataSource = new DataSource {
                EventActivationFlags = DataSourceEventActivationFlags.None().Enable(DataSourceEventType.DataAdded)
            };

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, dataSourceEventInvokeHelper: new DataSourceEventInvokeHelper(_ => ValueTask.CompletedTask));
            await map.AddSourceAsync(dataSource);

            Assert.Single(map.Sources, dataSource);
            Assert.Equal(_jsRuntimeMock.Object, dataSource.JSRuntime);
            Assert.Equal(_loggerMock.Object, dataSource.Logger);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] == null
                && parameters[2] as string == dataSource.SourceType.ToString()
                && (parameters[3] as IEnumerable<string>).Single() == DataSourceEventType.DataAdded.ToString()
                && parameters[4] is DotNetObjectReference<DataSourceEventInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddVectorTileSourceAsync()
        {
            var source = new VectorTileSource();

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);
            await map.AddSourceAsync(source);

            Assert.Single(map.Sources, source);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == source.Id
                && parameters[1] == null
                && parameters[2] as string == source.SourceType.ToString()
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddDataSource_Async()
        {
            var dataSource = new DataSource();

            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, dataSourceEventInvokeHelper: new DataSourceEventInvokeHelper(_ => ValueTask.CompletedTask));
            await map.AddSourceAsync(dataSource);
            await Assert.ThrowsAnyAsync<SourceAlreadyExistingException>(async () => await map.AddSourceAsync(dataSource));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] == null
                && parameters[2] as string == dataSource.SourceType.ToString()
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddDataSource_NullCaseAsync()
        {
            DataSource dataSource = null;
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);
            await map.AddSourceAsync(dataSource);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveDataSource_Async()
        {
            var dataSource = new DataSource();
            var dataSource2 = new DataSource();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, dataSourceEventInvokeHelper: new DataSourceEventInvokeHelper(_ => ValueTask.CompletedTask));

            await map.AddSourceAsync(dataSource);
            await map.AddSourceAsync(dataSource2);
            await map.RemoveDataSourceAsync(dataSource);

            Assert.DoesNotContain(dataSource, map.Sources);
            Assert.Contains(dataSource2, map.Sources);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] == null
                && parameters[2] as string == dataSource.SourceType.ToString()
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource2.Id
                && parameters[1] == null
                && parameters[2] as string == dataSource2.SourceType.ToString()
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveSource.ToCoreNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemoveDataSource_Async()
        {
            var dataSource = new DataSource();
            var dataSource2 = new DataSource();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, dataSourceEventInvokeHelper: new DataSourceEventInvokeHelper(_ => ValueTask.CompletedTask));

            await map.AddSourceAsync(dataSource);
            await map.RemoveDataSourceAsync(dataSource2);

            Assert.DoesNotContain(dataSource2, map.Sources);
            Assert.Contains(dataSource, map.Sources);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] == null
                && parameters[2] as string == dataSource.SourceType.ToString()
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveDataSource_ViaId_Async()
        {
            var dataSource = new DataSource();
            var dataSource2 = new DataSource();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, dataSourceEventInvokeHelper: new DataSourceEventInvokeHelper(_ => ValueTask.CompletedTask));

            await map.AddSourceAsync(dataSource);
            await map.AddSourceAsync(dataSource2);
            await map.RemoveDataSourceAsync(dataSource.Id);

            Assert.DoesNotContain(dataSource, map.Sources);
            Assert.Contains(dataSource2, map.Sources);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] == null
                && parameters[2] as string == dataSource.SourceType.ToString()
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource2.Id
                && parameters[1] == null
                && parameters[2] as string == dataSource2.SourceType.ToString()
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.RemoveSource.ToCoreNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemoveDataSource_ViaId_Async()
        {
            var dataSource = new DataSource();
            var dataSource2 = new DataSource();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, dataSourceEventInvokeHelper: new DataSourceEventInvokeHelper(_ => ValueTask.CompletedTask));

            await map.AddSourceAsync(dataSource);
            await map.RemoveDataSourceAsync(dataSource2.Id);

            Assert.DoesNotContain(dataSource2, map.Sources);
            Assert.Contains(dataSource, map.Sources);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] == null
                && parameters[2] as string == dataSource.SourceType.ToString()
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ClearMap_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, new DrawingToolbarEventInvokeHelper(eventArgs => ValueTask.CompletedTask),
                new HtmlMarkerInvokeHelper(eventArgs => ValueTask.CompletedTask),
                new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask),
                new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask),
                new DataSourceEventInvokeHelper(_ => ValueTask.CompletedTask));

            await map.AddSourceAsync(new DataSource());
            await map.AddLayerAsync(new BubbleLayer());
            await map.AddHtmlMarkersAsync(new HtmlMarker(null));
            await map.AddPopupAsync(new Popup(new PopupOptions()));

            await map.ClearMapAsync();
            Assert.Null(map.Sources);
            Assert.Null(map.Layers);
            Assert.Null(map.HtmlMarkers);
            Assert.Null(map.Popups);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddHtmlMarkers.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopup.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearMap.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ClearLayers_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, layerEventInvokeHelper: new LayerEventInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddLayerAsync(new BubbleLayer());

            await map.ClearLayersAsync();
            Assert.Null(map.Layers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddLayer.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearLayers.ToCoreNamespace()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ClearDataSources_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, dataSourceEventInvokeHelper: new DataSourceEventInvokeHelper(_ => ValueTask.CompletedTask));

            await map.AddSourceAsync(new DataSource());
            await map.ClearDataSourcesAsync();
            Assert.Null(map.Sources);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddSource.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearSources.ToCoreNamespace()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ClearHtmlMarkers_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(eventArgs => ValueTask.CompletedTask));

            await map.AddHtmlMarkersAsync(new HtmlMarker(null));
            await map.ClearHtmlMarkersAsync();

            Assert.Null(map.HtmlMarkers);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddHtmlMarkers.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is IEnumerable<HtmlMarkerCreationOptions> && (parameters[0] as IEnumerable<HtmlMarkerCreationOptions>).Count() == 1
                && parameters[1] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearHtmlMarkers.ToCoreNamespace()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddPopup_Async()
        {
            var popup = new Popup(new PopupOptions());
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddPopupAsync(popup);

            Assert.Contains(popup, map.Popups);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopup.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == popup.Id
                && parameters[1] as PopupOptions == popup.Options
                && parameters[2] is IEnumerable<string>
                && parameters[3] is DotNetObjectReference<PopupInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddPopup_NullCaseAsync()
        {
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await map.AddPopupAsync(null));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddTwiceTheSamePopup_Async()
        {
            var popup = new Popup(new PopupOptions());
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddPopupAsync(popup);
            await Assert.ThrowsAnyAsync<PopupAlreadyExistingException>(async () => await map.AddPopupAsync(popup));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopup.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == popup.Id
                && parameters[1] as PopupOptions == popup.Options
                && parameters[2] is IEnumerable<string>
                && parameters[3] is DotNetObjectReference<PopupInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddPopupWithTemplate_Async()
        {
            var popup = new Popup(new PopupOptions());
            var popupTemplate = new PopupTemplate();
            var properties = new Dictionary<string, object>();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddPopupAsync(popup, popupTemplate, properties);

            Assert.Contains(popup, map.Popups);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopupWithTemplate.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == popup.Id
                && parameters[1] as PopupOptions == popup.Options
                && parameters[2] is IDictionary<string, object>
                && parameters[3] is PopupTemplate
                && parameters[4] is IEnumerable<string>
                && parameters[5] is DotNetObjectReference<PopupInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddPopupWithTemplate_NullCaseAsync()
        {
            var popupTemplate = new PopupTemplate();
            var properties = new Dictionary<string, object>();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await map.AddPopupAsync(null, popupTemplate, properties));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddPopupWithTemplate_NullTemplateCaseAsync()
        {
            var popup = new Popup(new PopupOptions());
            var properties = new Dictionary<string, object>();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await map.AddPopupAsync(popup, null, properties));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddPopupWithTemplate_NullPropertiesCaseAsync()
        {
            var popup = new Popup(new PopupOptions());
            var popupTemplate = new PopupTemplate();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await map.AddPopupAsync(popup, popupTemplate, null));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddTwiceTheSamePopupWithTemplate_Async()
        {
            var popup = new Popup(new PopupOptions());
            var popupTemplate = new PopupTemplate();
            var properties = new Dictionary<string, object>();
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddPopupAsync(popup, popupTemplate, properties);
            await Assert.ThrowsAnyAsync<PopupAlreadyExistingException>(async () => await map.AddPopupAsync(popup, popupTemplate, properties));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopupWithTemplate.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == popup.Id
                && parameters[1] as PopupOptions == popup.Options
                && parameters[2] is IDictionary<string, object>
                && parameters[3] is PopupTemplate
                && parameters[4] is IEnumerable<string>
                && parameters[5] is DotNetObjectReference<PopupInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemovePopup_Async()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddPopupAsync(popup);
            await map.RemovePopupAsync(popup);

            Assert.DoesNotContain(popup, map.Popups);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopup.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Remove.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemovePopup_IdVersion_Async()
        {
            var popup = new Popup(new PopupOptions()) {
                JSRuntime = _jsRuntimeMock.Object
            };
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddPopupAsync(popup);
            await map.RemovePopupAsync(popup.Id);

            Assert.DoesNotContain(popup, map.Popups);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopup.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Popup.Remove.ToPopupNamespace(), popup.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemovePopup_Async()
        {
            var popup = new Popup(new PopupOptions());
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddPopupAsync(popup);
            await map.RemovePopupAsync(new Popup(new PopupOptions()));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopup.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ClearPopups_Async()
        {
            var popup = new Popup(new PopupOptions());
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object, popupInvokeHelper: new PopupInvokeHelper(eventArgs => ValueTask.CompletedTask));
            await map.AddPopupAsync(popup);
            await map.ClearPopupsAsync();

            Assert.Null(map.Popups);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.ClearPopups.ToCoreNamespace()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.AddPopup.ToCoreNamespace(), It.IsAny<object[]>()), Times.Once);
        }

        [Fact]
        public async void Should_UpdateCameraOptions_Async()
        {
            var center = new Position(10, 10);
            var initialCameraOptions = new CameraOptions {
                Duration = 10
            };
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object) {
                CameraOptions = initialCameraOptions
            };

            await map.SetCameraOptionsAsync(options => options.Center = center);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCameraOptions.ToCoreNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as CameraOptions).Duration == 10 && (parameters[0] as CameraOptions).Center.Longitude == 10 && (parameters[0] as CameraOptions).Center.Latitude == 10
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateCameraOptions_NoCameraOptionsDefined_Async()
        {
            var center = new Position(10, 10);
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);

            await map.SetCameraOptionsAsync(options => options.Center = center);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCameraOptions.ToCoreNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as CameraOptions).Center.Longitude == 10 && (parameters[0] as CameraOptions).Center.Latitude == 10
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateStyleOptions_Async()
        {
            var language = "fr";
            var initialStyleOptions = new StyleOptions {
                AutoResize = true
            };
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object) {
                StyleOptions = initialStyleOptions
            };

            await map.SetStyleOptionsAsync(options => options.Language = language);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetStyleOptions.ToCoreNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as StyleOptions).AutoResize && (parameters[0] as StyleOptions).Language == "fr"
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateStyleOptions_NoStyleOptionsDefined_Async()
        {
            var language = "fr";
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);

            await map.SetStyleOptionsAsync(options => options.Language = language);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetStyleOptions.ToCoreNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as StyleOptions).Language == "fr"
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateUserInteraction_Async()
        {
            var initialUserInteractionOptions = new UserInteractionOptions {
                BoxZoomInteraction = true
            };
            var dblClickZoomInteraction = true;
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object) {
                UserInteractionOptions = initialUserInteractionOptions
            };

            await map.SetUserInteractionAsync(options => options.DblclickZoomInteraction = dblClickZoomInteraction);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetUserInteraction.ToCoreNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as UserInteractionOptions).BoxZoomInteraction.GetValueOrDefault() && (parameters[0] as UserInteractionOptions).DblclickZoomInteraction.GetValueOrDefault()
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateUserInteraction_NoUserInteractionDefined_Async()
        {
            var dblClickZoomInteraction = true;
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);

            await map.SetUserInteractionAsync(options => options.DblclickZoomInteraction = dblClickZoomInteraction);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetUserInteraction.ToCoreNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as UserInteractionOptions).DblclickZoomInteraction.GetValueOrDefault()
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateTrafficInteraction_Async()
        {
            var initialTrafficOptions = new TrafficOptions {
                Flow = TrafficFlow.Absolute
            };
            var incidents = true;
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object) {
                TrafficOptions = initialTrafficOptions
            };

            await map.SetTrafficOptionsAsync(options => options.Incidents = incidents);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetTraffic.ToCoreNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as TrafficOptions).Flow.ToString() == TrafficFlow.Absolute.ToString() && (parameters[0] as TrafficOptions).Incidents.GetValueOrDefault()
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateTrafficInteraction_NoTrafficOptionsDefined_Async()
        {
            var incidents = true;
            var map = new Map("id", _jsRuntimeMock.Object, _loggerMock.Object);

            await map.SetTrafficOptionsAsync(options => options.Incidents = incidents);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetTraffic.ToCoreNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as TrafficOptions).Incidents.GetValueOrDefault()
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_DispatchBoxZoomEndEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "boxzoomend" };
            map.OnBoxZoomEnd += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchBoxZoomStartEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "boxzoomstart" };
            map.OnBoxZoomStart += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchClickEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "click" };
            map.OnClick += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchContextMenuEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "contextmenu" };
            map.OnContextMenu += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDataEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "data" };
            map.OnData += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDblClickEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "dblclick" };
            map.OnDblClick += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDragEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "drag" };
            map.OnDrag += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDragEndEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "dragend" };
            map.OnDragEnd += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDragStartEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "dragstart" };
            map.OnDragStart += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchErrorEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "error" };
            map.OnError += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchIdleEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "idle" };
            map.OnIdle += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchLayerAddedEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "layeradded" };
            map.OnLayerAdded += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchLayerRemovedEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "layerremoved" };
            map.OnLayerRemoved += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchLoadEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "load" };
            map.OnLoad += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMouseDownEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "mousedown" };
            map.OnMouseDown += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMouseMoveEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "mousemove" };
            map.OnMouseMove += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
        }

        [Fact]
        public void Should_DispatchMouseOutEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "mouseout" };
            map.OnMouseOut += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMouseOverEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "mouseover" };
            map.OnMouseOver += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMouseUpEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "mouseup" };
            map.OnMouseUp += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMoveEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "move" };
            map.OnMove += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMoveEndEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "moveend" };
            map.OnMoveEnd += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchMoveStartEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "movestart" };
            map.OnMoveStart += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchPitchEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "pitch" };
            map.OnPitch += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchPitchEndEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "pitchend" };
            map.OnPitchEnd += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchPitchStartEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "pitchstart" };
            map.OnPitchStart += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchReadyEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "ready" };
            map.OnReady += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchRenderEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "render" };
            map.OnRender += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchResizeEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "resize" };
            map.OnResize += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchRotateEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "rotate" };
            map.OnRotate += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchRotateEndEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "rotateend" };
            map.OnRotateEnd += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchRotateStartEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "rotatestart" };
            map.OnRotateStart += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchSourceAddedEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "sourceadded" };
            map.OnSourceAdded += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchSourceDataEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "sourcedata" };
            map.OnSourceData += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchSourceRemovedEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "sourceremoved" };
            map.OnSourceRemoved += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchStyleDataEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "styledata" };
            map.OnStyleData += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchStyleImageMissingEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "styleimagemissing" };
            map.OnStyleImageMissing += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchTokenAcquiredEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "tokenacquired" };
            map.OnTokenAcquired += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchTouchCancelEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "touchcancel" };
            map.OnTouchCancel += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchTouchEndEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "touchend" };
            map.OnTouchEnd += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchTouchMoveEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "touchmove" };
            map.OnTouchMove += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchTouchStartEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "touchstart" };
            map.OnTouchStart += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchWheelEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "wheel" };
            map.OnWheel += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchZoomEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "zoom" };
            map.OnZoom += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchZoomEndEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "zoomend" };
            map.OnZoomEnd += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchZoomStartEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new MapJsEventArgs { Type = "zoomstart" };
            map.OnZoomStart += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDrawingChangedEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new DrawingToolbarJsEventArgs { Type = "drawingchanged" };
            map.OnDrawingChanged += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchDrawingToolbarEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDrawingChangingEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new DrawingToolbarJsEventArgs { Type = "drawingchanging" };
            map.OnDrawingChanging += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchDrawingToolbarEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDrawingCompleteEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new DrawingToolbarJsEventArgs { Type = "drawingcomplete" };
            map.OnDrawingComplete += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchDrawingToolbarEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDrawingModeChangedEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new DrawingToolbarJsEventArgs { Type = "drawingmodechanged" };
            map.OnDrawingModeChanged += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchDrawingToolbarEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public void Should_DispatchDrawingStartedEvent()
        {
            var assertEvent = false;
            var map = new Map("id");
            var jsEventArgs = new DrawingToolbarJsEventArgs { Type = "drawingstarted" };
            map.OnDrawingStarted += eventArgs => assertEvent = eventArgs.Map == map && eventArgs.Type == jsEventArgs.Type;
            map.DispatchDrawingToolbarEvent(jsEventArgs);
            Assert.True(assertEvent);
        }

        [Fact]
        public async void Should_CreateImageFromTemplateAsync()
        {
            var map = new Map("id", _jsRuntimeMock.Object);
            await map.CreateImageFromTemplateAsync("imageId", "templateName", "color", "secondaryColor", 1m);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.CreateImageFromTemplate.ToCoreNamespace(), It.Is<object[]>(parameters =>
                parameters[0] is MapImageTemplate
                && ((MapImageTemplate)parameters[0]).Id == "imageId"
                && ((MapImageTemplate)parameters[0]).TemplateName == "templateName"
                && ((MapImageTemplate)parameters[0]).Color == "color"
                && ((MapImageTemplate)parameters[0]).SecondaryColor == "secondaryColor"
                && ((MapImageTemplate)parameters[0]).Scale == 1m
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetCanvasStylePropertyAsync()
        {
            var map = new Map("id", _jsRuntimeMock.Object);
            await map.SetCanvasStylePropertyAsync("property", "value");

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasStyleProperty.ToCoreNamespace(), "property", "value"), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async void Should_NotSetCanvasStyleProperty_InvalidProperty_Async(string property)
        {
            var map = new Map("id", _jsRuntimeMock.Object);
            await Assert.ThrowsAnyAsync<ArgumentException>(async () => await map.SetCanvasStylePropertyAsync(property, "value"));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetCanvasStylePropertiesAsync()
        {
            var map = new Map("id", _jsRuntimeMock.Object);
            var properties = new Dictionary<string, string> {
                { "cursor", "hand" }
            };

            await map.SetCanvasStylePropertiesAsync(properties);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasStyleProperties.ToCoreNamespace(), It.Is<IEnumerable<KeyValuePair<string, string>>>(dictionary =>
                dictionary.Single().Key == "cursor" && dictionary.Single().Value == "hand"))
            , Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetCanvasStyleProperties_NullCase_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object);

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await map.SetCanvasStylePropertiesAsync(null));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOnlyDefinedCanvasProperties_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object);

            var properties = new Dictionary<string, string> {
                { "cursor", "hand" },
                { "", "value" },
                { " ", "value" },
            };

            await map.SetCanvasStylePropertiesAsync(properties);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasStyleProperties.ToCoreNamespace(), It.Is<IEnumerable<KeyValuePair<string, string>>>(dictionary =>
                dictionary.Single().Key == "cursor" && dictionary.Single().Value == "hand"))
            , Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetCanvasProperties_EmptyCase_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object);

            var properties = new Dictionary<string, string>();

            await map.SetCanvasStylePropertiesAsync(properties);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetCanvasProperties_NoDefinedPropertiesCase_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object);

            var properties = new Dictionary<string, string> {
                { "", "value" },
                { " ", "value" },
            };
            await map.SetCanvasStylePropertiesAsync(properties);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetCanvasContainerStylePropertyAsync()
        {
            var map = new Map("id", _jsRuntimeMock.Object);
            await map.SetCanvasContainerStylePropertyAsync("property", "value");

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasContainerStyleProperty.ToCoreNamespace(), "property", "value"), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async void Should_NotSetCanvasContainerStyleProperty_InvalidProperty_Async(string property)
        {
            var map = new Map("id", _jsRuntimeMock.Object);
            await Assert.ThrowsAnyAsync<ArgumentException>(async () => await map.SetCanvasContainerStylePropertyAsync(property, "value"));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetCanvasContainerStylePropertiesAsync()
        {
            var map = new Map("id", _jsRuntimeMock.Object);
            var properties = new Dictionary<string, string> {
                { "cursor", "hand" }
            };

            await map.SetCanvasContainerStylePropertiesAsync(properties);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasContainerStyleProperties.ToCoreNamespace(), It.Is<IEnumerable<KeyValuePair<string, string>>>(dictionary =>
                dictionary.Single().Key == "cursor" && dictionary.Single().Value == "hand"))
            , Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetCanvasContainerStyleProperties_NullCase_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object);

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await map.SetCanvasContainerStylePropertiesAsync(null));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOnlyDefinedCanvasContainerProperties_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object);

            var properties = new Dictionary<string, string> {
                { "cursor", "hand" },
                { "", "value" },
                { " ", "value" },
            };

            await map.SetCanvasContainerStylePropertiesAsync(properties);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Core.SetCanvasContainerStyleProperties.ToCoreNamespace(), It.Is<IEnumerable<KeyValuePair<string, string>>>(dictionary =>
                dictionary.Single().Key == "cursor" && dictionary.Single().Value == "hand"))
            , Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetCanvasContainerProperties_EmptyCase_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object);

            var properties = new Dictionary<string, string>();

            await map.SetCanvasContainerStylePropertiesAsync(properties);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetCanvasContainerProperties_NoDefinedPropertiesCase_Async()
        {
            var map = new Map("id", _jsRuntimeMock.Object);

            var properties = new Dictionary<string, string> {
                { "", "value" },
                { " ", "value" },
            };
            await map.SetCanvasContainerStylePropertiesAsync(properties);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_GetCameraOptionsAsync()
        {
            var options = new CameraOptions();
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<CameraOptions>(It.IsAny<string>())).ReturnsAsync(options);

            var map = new Map("id", _jsRuntimeMock.Object);
            var result = await map.GetCameraOptionsAsync();
            Assert.Equal(options, map.CameraOptions);
            Assert.Equal(options, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<CameraOptions>(Constants.JsConstants.Methods.Core.GetCamera.ToCoreNamespace()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_GetStyleOptionsAsync()
        {
            var options = new StyleOptions();
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<StyleOptions>(It.IsAny<string>())).ReturnsAsync(options);

            var map = new Map("id", _jsRuntimeMock.Object);
            var result = await map.GetStyleOptionsAsync();
            Assert.Equal(options, map.StyleOptions);
            Assert.Equal(options, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<StyleOptions>(Constants.JsConstants.Methods.Core.GetStyle.ToCoreNamespace()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_GetTrafficOptionsAsync()
        {
            var options = new TrafficOptions();
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<TrafficOptions>(It.IsAny<string>())).ReturnsAsync(options);

            var map = new Map("id", _jsRuntimeMock.Object);
            var result = await map.GetTrafficOptionsAsync();
            Assert.Equal(options, map.TrafficOptions);
            Assert.Equal(options, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<TrafficOptions>(Constants.JsConstants.Methods.Core.GetTraffic.ToCoreNamespace()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_GetUserInteractionOptionsAsync()
        {
            var options = new UserInteractionOptions();
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<UserInteractionOptions>(It.IsAny<string>())).ReturnsAsync(options);

            var map = new Map("id", _jsRuntimeMock.Object);
            var result = await map.GetUserInteractionOptionsAsync();
            Assert.Equal(options, map.UserInteractionOptions);
            Assert.Equal(options, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<UserInteractionOptions>(Constants.JsConstants.Methods.Core.GetUserInteraction.ToCoreNamespace()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
