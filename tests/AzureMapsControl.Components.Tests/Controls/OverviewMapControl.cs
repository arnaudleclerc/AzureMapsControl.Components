namespace AzureMapsControl.Components.Tests.Controls
{
    using System;

    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Runtime;
    using AzureMapsControl.Components.Tests.Json;

    using Moq;

    using Xunit;

    public class OverviewMapControlTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();

        [Fact]
        public void Should_Create()
        {
            var options = new OverviewMapControlOptions();
            var position = ControlPosition.BottomLeft;
            var control = new OverviewMapControl(options, position);
            Assert.Equal(options, control.Options);
            Assert.Equal(position, control.Position);
            Assert.NotEqual(Guid.Empty, control.Id);
            Assert.Equal("overviewmap", control.Type);
        }

        [Fact]
        public async void Should_UpdateAsync()
        {
            var options = new OverviewMapControlOptions();
            var position = ControlPosition.BottomLeft;
            var control = new OverviewMapControl(options, position) {
                JsRuntime = _jsRuntimeMock.Object
            };

            await control.UpdateAsync(options => options.Interactive = true);
            Assert.True(options.Interactive);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.OverviewMapControl.SetOptions.ToOverviewMapControlNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as Guid?).GetValueOrDefault().ToString() == control.Id.ToString()
                && (parameters[1] as OverviewMapControlOptions) == control.Options
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotUpdate_NotAddtoMapCase_Async()
        {
            var control = new OverviewMapControl();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.UpdateAsync(options => options.Interactive = true));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOptionsAsync()
        {
            var options = new OverviewMapControlOptions();
            var position = ControlPosition.BottomLeft;
            var control = new OverviewMapControl(options, position) {
                JsRuntime = _jsRuntimeMock.Object
            };

            await control.SetOptionsAsync(options => options.Interactive = true);
            Assert.True(options.Interactive);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.OverviewMapControl.SetOptions.ToOverviewMapControlNamespace(), It.Is<object[]>(parameters =>
                            (parameters[0] as Guid?).GetValueOrDefault().ToString() == control.Id.ToString()
                            && (parameters[1] as OverviewMapControlOptions) == control.Options
                        )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetOptions_NotAddtoMapCase_Async()
        {
            var control = new OverviewMapControl();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.SetOptionsAsync(options => options.Interactive = true));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_UpdateAsyncWithDefaultOptionsAsync()
        {
            var control = new OverviewMapControl {
                JsRuntime = _jsRuntimeMock.Object
            };

            await control.UpdateAsync(options => options.Interactive = true);
            Assert.True(control.Options.Interactive);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.OverviewMapControl.SetOptions.ToOverviewMapControlNamespace(), It.Is<object[]>(parameters =>
                            (parameters[0] as Guid?).GetValueOrDefault().ToString() == control.Id.ToString()
                            && (parameters[1] as OverviewMapControlOptions) == control.Options
                        )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOptionsAsyncWithDefaultOptionsAsync()
        {
            var control = new OverviewMapControl {
                JsRuntime = _jsRuntimeMock.Object
            };

            await control.SetOptionsAsync(options => options.Interactive = true);
            Assert.True(control.Options.Interactive);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.OverviewMapControl.SetOptions.ToOverviewMapControlNamespace(), It.Is<object[]>(parameters =>
                            (parameters[0] as Guid?).GetValueOrDefault().ToString() == control.Id.ToString()
                            && (parameters[1] as OverviewMapControlOptions) == control.Options
                        )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }

    public class OverviewMapControlJsonConverterTests : JsonConverterTests<OverviewMapControl>
    {
        public OverviewMapControlJsonConverterTests() : base(new OverviewMapControlJsonConverter()) { }

        [Fact]
        public void Should_WriteWithControlStyle()
        {
            var control = new OverviewMapControl(new OverviewMapControlOptions {
                Height = 1,
                Interactive = true,
                MapStyle = MapStyle.Blank,
                MarkerOptions = new Components.Markers.HtmlMarkerOptions {
                    Anchor = Components.Markers.MarkerAnchor.Bottom,
                    Color = "color",
                    Draggable = false,
                    HtmlContent = "htmlContent",
                    PixelOffset = new Components.Atlas.Pixel(2, 3),
                    Position = new Components.Atlas.Position(4, 5, 6),
                    SecondaryColor = "secondaryColor",
                    Text = "text",
                    Visible = true,
                },
                Minimized = false,
                Overlay = OverviewMapControlOverlay.Area,
                ShowToggle = true,
                Style = new Components.Atlas.Either<ControlStyle, string>(ControlStyle.Auto),
                SyncBearingPitch = false,
                SyncZoom = true,
                Visible = false,
                Width = 7,
                Zoom = 8,
                ZoomOffset = 9
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"height\":" + control.Options.Height.Value
                + ",\"interactive\":" + control.Options.Interactive.Value
                + ",\"mapStyle\":\"" + control.Options.MapStyle.ToString() + "\""
                + ",\"markerOptions\":{"
                + "\"anchor\":\"" + control.Options.MarkerOptions.Anchor.ToString() + "\""
                + ",\"color\":\"" + control.Options.MarkerOptions.Color.ToString() + "\""
                + ",\"draggable\":" + control.Options.MarkerOptions.Draggable.Value
                + ",\"htmlContent\":\"" + control.Options.MarkerOptions.HtmlContent.ToString() + "\""
                + ",\"pixelOffset\":[" + control.Options.MarkerOptions.PixelOffset.X + "," + control.Options.MarkerOptions.PixelOffset.Y + "]"
                + ",\"position\":[" + control.Options.MarkerOptions.Position.Longitude + "," + control.Options.MarkerOptions.Position.Latitude + "," + control.Options.MarkerOptions.Position.Elevation.Value + "]"
                + ",\"secondaryColor\":\"" + control.Options.MarkerOptions.SecondaryColor.ToString() + "\""
                + ",\"text\":\"" + control.Options.MarkerOptions.Text.ToString() + "\""
                + ",\"visible\":" + control.Options.MarkerOptions.Visible.Value
                + "}"
                + ",\"minimized\":" + control.Options.Minimized.Value
                + ",\"overlay\":\"" + control.Options.Overlay.ToString() + "\""
                + ",\"showToggle\":" + control.Options.ShowToggle.Value
                + ",\"style\":\"" + control.Options.Style.FirstChoice.ToString() + "\""
                + ",\"syncBearingPitch\":" + control.Options.SyncBearingPitch.Value
                + ",\"syncZoom\":" + control.Options.SyncZoom.Value
                + ",\"visible\":" + control.Options.Visible.Value
                + ",\"width\":" + control.Options.Width.Value
                + ",\"zoom\":" + control.Options.Zoom.Value
                + ",\"zoomOffset\":" + control.Options.ZoomOffset.Value
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

        [Fact]
        public void Should_WriteWithCssStyle()
        {
            var control = new OverviewMapControl(new OverviewMapControlOptions {
                Height = 1,
                Interactive = true,
                MapStyle = MapStyle.Blank,
                MarkerOptions = new Components.Markers.HtmlMarkerOptions {
                    Anchor = Components.Markers.MarkerAnchor.Bottom,
                    Color = "color",
                    Draggable = false,
                    HtmlContent = "htmlContent",
                    PixelOffset = new Components.Atlas.Pixel(2, 3),
                    Position = new Components.Atlas.Position(4, 5, 6),
                    SecondaryColor = "secondaryColor",
                    Text = "text",
                    Visible = true,
                },
                Minimized = false,
                Overlay = OverviewMapControlOverlay.Area,
                ShowToggle = true,
                Style = new Components.Atlas.Either<ControlStyle, string>("#000000"),
                SyncBearingPitch = false,
                SyncZoom = true,
                Visible = false,
                Width = 7,
                Zoom = 8,
                ZoomOffset = 9
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"height\":" + control.Options.Height.Value
                + ",\"interactive\":" + control.Options.Interactive.Value
                + ",\"mapStyle\":\"" + control.Options.MapStyle.ToString() + "\""
                + ",\"markerOptions\":{"
                + "\"anchor\":\"" + control.Options.MarkerOptions.Anchor.ToString() + "\""
                + ",\"color\":\"" + control.Options.MarkerOptions.Color.ToString() + "\""
                + ",\"draggable\":" + control.Options.MarkerOptions.Draggable.Value
                + ",\"htmlContent\":\"" + control.Options.MarkerOptions.HtmlContent.ToString() + "\""
                + ",\"pixelOffset\":[" + control.Options.MarkerOptions.PixelOffset.X + "," + control.Options.MarkerOptions.PixelOffset.Y + "]"
                + ",\"position\":[" + control.Options.MarkerOptions.Position.Longitude + "," + control.Options.MarkerOptions.Position.Latitude + "," + control.Options.MarkerOptions.Position.Elevation.Value + "]"
                + ",\"secondaryColor\":\"" + control.Options.MarkerOptions.SecondaryColor.ToString() + "\""
                + ",\"text\":\"" + control.Options.MarkerOptions.Text.ToString() + "\""
                + ",\"visible\":" + control.Options.MarkerOptions.Visible.Value
                + "}"
                + ",\"minimized\":" + control.Options.Minimized.Value
                + ",\"overlay\":\"" + control.Options.Overlay.ToString() + "\""
                + ",\"showToggle\":" + control.Options.ShowToggle.Value
                + ",\"style\":\"" + control.Options.Style.SecondChoice + "\""
                + ",\"syncBearingPitch\":" + control.Options.SyncBearingPitch.Value
                + ",\"syncZoom\":" + control.Options.SyncZoom.Value
                + ",\"visible\":" + control.Options.Visible.Value
                + ",\"width\":" + control.Options.Width.Value
                + ",\"zoom\":" + control.Options.Zoom.Value
                + ",\"zoomOffset\":" + control.Options.ZoomOffset.Value
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }
    }
}
