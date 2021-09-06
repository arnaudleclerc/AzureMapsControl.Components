namespace AzureMapsControl.Components.Tests.Controls
{
    using System.Linq;

    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Geolocation;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class ControlJsonConverterTests : JsonConverterTests<Control>
    {
        public ControlJsonConverterTests(): base(new ControlJsonConverter()) { }

        [Fact]
        public void Should_WriteCompassControl()
        {
            var compassControl = new CompassControl(new CompassControlOptions {
                RotationDegreesDelta = 1,
                Style = ControlStyle.Auto
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + compassControl.Id + "\""
                + ",\"type\":\"" + compassControl.Type + "\""
                + ",\"position\":\"" + compassControl.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"rotationDegreesDelta\":" + compassControl.Options.RotationDegreesDelta.Value
                + ",\"style\":\"" + compassControl.Options.Style.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(compassControl, expectedJson);
        }

        [Fact]
        public void Should_WritePitchControl()
        {
            var control = new PitchControl(new PitchControlOptions {
                PitchDegreesDelta = 1,
                Style = ControlStyle.Auto
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"pitchDegreesDelta\":" + control.Options.PitchDegreesDelta.Value
                + ",\"style\":\"" + control.Options.Style.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

        [Fact]
        public void Should_WriteFullScreenControl_WithControlStyle()
        {
            var control = new FullScreenControl(new FullScreenControlOptions {
                Container = "container",
                HideIfUnsupported = true,
                Style = new Components.Atlas.Either<ControlStyle, string>(ControlStyle.Auto)
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"container\":\"" + control.Options.Container + "\""
                + ",\"hideIfUnsupported\":" + control.Options.HideIfUnsupported
                + ",\"style\":\"" + control.Options.Style.FirstChoice.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

        [Fact]
        public void Should_WriteFullScreenControl_WithCssStyle()
        {
            var control = new FullScreenControl(new FullScreenControlOptions {
                Container = "container",
                HideIfUnsupported = true,
                Style = new Components.Atlas.Either<ControlStyle, string>("#000000")
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"container\":\"" + control.Options.Container + "\""
                + ",\"hideIfUnsupported\":" + control.Options.HideIfUnsupported
                + ",\"style\":\"" + control.Options.Style.SecondChoice + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

        [Fact]
        public void Should_WriteGeolocationControl()
        {
            var control = new GeolocationControl(new GeolocationControlOptions {
                CalculateMissingValues = true,
                MarkerColor = "markerColor",
                MaxZoom = 1,
                PositionOptions = new PositionOptions {
                    EnableHighAccuracy = false,
                    MaximumAge = 2,
                    Timeout = 3,
                },
                ShowUserLocation = true,
                TrackUserLocation = false,
                UpdateMapCamera = true,
                Style = ControlStyle.Auto
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"calculateMissingValues\":" + control.Options.CalculateMissingValues.Value
                + ",\"markerColor\":\"" + control.Options.MarkerColor.ToString() + "\""
                + ",\"maxZoom\":" + control.Options.MaxZoom.Value
                + ",\"positionOptions\":{"
                + "\"enableHighAccuracy\":" + control.Options.PositionOptions.EnableHighAccuracy.Value
                + ",\"maximumAge\":" + control.Options.PositionOptions.MaximumAge.Value
                + ",\"timeout\":" + control.Options.PositionOptions.Timeout.Value
                + "}"
                + ",\"showUserLocation\":" + control.Options.ShowUserLocation.Value
                + ",\"style\":\"" + control.Options.Style.ToString() + "\""
                + ",\"trackUserLocation\":" + control.Options.TrackUserLocation.Value
                + ",\"updateMapCamera\":" + control.Options.UpdateMapCamera.Value
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

        [Fact]
        public void Should_WriteOverviewMapControl_WithControlStyle()
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
        public void Should_WriteOverviewMapControl_WithCssStyle()
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

        [Fact]
        public void Should_WriteScalebarControl()
        {
            var control = new ScaleBarControl(new ScaleBarControlOptions {
                MaxBarLength = 1,
                Units = ScaleBarControlUnits.Metric
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"maxBarLength\":" + control.Options.MaxBarLength.Value
                + ",\"units\":\"" + control.Options.Units.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

        [Fact]
        public void Should_WriteStyleControl()
        {
            var control = new StyleControl(new StyleControlOptions {
                MapStyles = new[] { MapStyle.Blank, MapStyle.GrayscaleDark },
                Style = ControlStyle.Auto,
                StyleControlLayout = StyleControlLayout.Icons
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"mapStyles\":[\"" + control.Options.MapStyles.First().ToString() + "\",\"" + control.Options.MapStyles.ElementAt(1).ToString() + "\"]"
                + ",\"style\":\"" + control.Options.Style.ToString() + "\""
                + ",\"styleControlLayout\":\"" + control.Options.StyleControlLayout.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

        [Fact]
        public void Should_WriteZoomControl()
        {
            var control = new ZoomControl(new ZoomControlOptions {
                Style = ControlStyle.Auto,
                ZoomDelta = 1
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"style\":\"" + control.Options.Style.ToString() + "\""
                + ",\"zoomDelta\":" + control.Options.ZoomDelta.Value
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }
    }
}
