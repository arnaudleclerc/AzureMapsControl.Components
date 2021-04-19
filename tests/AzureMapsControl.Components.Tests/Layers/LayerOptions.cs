namespace AzureMapsControl.Components.Tests.Layers
{
    using System.Text.Json;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class LayerOptionsJsonConverterTests : JsonConverterTests<LayerOptions>
    {
        public LayerOptionsJsonConverterTests() : base(new LayerOptionsJsonConverter()) { }

        [Fact]
        public void Should_SerializeBubbleLayerOptions()
        {
            var options = new BubbleLayerOptions {
                Filter = new ExpressionOrNumber(1),
                MaxZoom = 2,
                MinZoom = 3,
                Visible = true,
                Source = "source",
                SourceLayer = "sourceLayer",
                Blur = new ExpressionOrNumber(4),
                Color = new ExpressionOrString("color"),
                Opacity = new ExpressionOrNumber(5),
                PitchAlignment = PitchAlignment.Map,
                Radius = new ExpressionOrNumber(6),
                StrokeColor = new ExpressionOrString("strokeColor"),
                StrokeOpacity = new ExpressionOrNumber(7),
                StrokeWidth = new ExpressionOrNumber(8)
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializeHeatmapLayerOptions()
        {
            var options = new HeatmapLayerOptions {
                Filter = new ExpressionOrNumber(1),
                MaxZoom = 2,
                MinZoom = 3,
                Visible = true,
                Source = "source",
                SourceLayer = "sourceLayer",
                Color = new ExpressionOrString("color"),
                Intensity = new ExpressionOrNumber(4),
                Opacity = new ExpressionOrNumber(5),
                Radius = new ExpressionOrNumber(6),
                Weight = new ExpressionOrNumber(7)
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializeImageLayerOptions()
        {
            var url = "url";
            var positions = new[] {
                new Position(11, 12),
                new Position(13, 14),
                new Position(15, 16),
                new Position(17, 18),
            };

            var options = new ImageLayerOptions(url, positions) {
                Filter = new ExpressionOrNumber(1),
                MaxZoom = 2,
                MinZoom = 3,
                Visible = true,
                Contrast = 4,
                FadeDuration = 5,
                HueRotation = 6,
                MaxBrightness = 7,
                MinBrightness = 8,
                Opacity = 9,
                Saturation = 10
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializeLineLayerOptions()
        {
            var options = new LineLayerOptions {
                Filter = new ExpressionOrNumber(1),
                MaxZoom = 2,
                MinZoom = 3,
                Visible = true,
                Source = "source",
                SourceLayer = "sourceLayer",
                Blur = new ExpressionOrNumber(4),
                LineCap = LineCap.Butt,
                LineJoin = LineJoin.Bevel,
                Offset = new ExpressionOrNumber(5),
                StrokeColor = new ExpressionOrString("strokeColor"),
                StrokeDashArray = new double[] { 6, 7 },
                StrokeGradient = new ExpressionOrString("strokeGradient"),
                StrokeOpacity = new ExpressionOrNumber(8),
                StrokeWidth = new ExpressionOrNumber(9),
                Translate = new Pixel(10, 11),
                TranslateAnchor = PitchAlignment.Map
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializePolygonExtrusionLayerOptions()
        {
            var options = new PolygonExtrusionLayerOptions {
                Filter = new ExpressionOrNumber(1),
                MaxZoom = 2,
                MinZoom = 3,
                Visible = true,
                Source = "source",
                SourceLayer = "sourceLayer",
                Base = new ExpressionOrNumber(4),
                FillColor = new ExpressionOrString("fillColor"),
                FillOpacity = 5,
                FillPattern = "fillPattern",
                Height = new ExpressionOrNumber(6),
                Translate = new Pixel(7, 8),
                TranslateAnchor = PitchAlignment.Map,
                VerticalGradient = true
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializePolygonLayerOptions()
        {
            var options = new PolygonLayerOptions {
                Filter = new ExpressionOrNumber(1),
                MaxZoom = 2,
                MinZoom = 3,
                Visible = true,
                Source = "source",
                SourceLayer = "sourceLayer",
                FillColor = new ExpressionOrString("fillColor"),
                FillOpacity = new ExpressionOrNumber(4),
                FillPattern = new ExpressionOrString("fillPattern")
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializeSymbolLayerOptions()
        {
            var options = new SymbolLayerOptions {
                Filter = new ExpressionOrNumber(1),
                MaxZoom = 2,
                MinZoom = 3,
                Visible = true,
                Source = "source",
                SourceLayer = "sourceLayer",
                IconOptions = new IconOptions {
                    AllowOverlap = true,
                    Anchor = new ExpressionOrString("anchor"),
                    IgnorePlacement = true,
                    Image = new ExpressionOrString("image"),
                    Offset = new ExpressionOrNumber(4),
                    Opacity = new ExpressionOrNumber(5),
                    Optional = true,
                    PitchAlignment = PitchAlignment.Map,
                    Rotation = new ExpressionOrNumber(6),
                    RotationAlignment = PitchAlignment.Map,
                    Size = new ExpressionOrNumber(7)
                },
                LineSpacing = new ExpressionOrNumber(8),
                Placement = SymbolLayerPlacement.Line,
                TextOptions = new TextOptions {
                    AllowOverlap = true,
                    Anchor = new ExpressionOrString("anchor"),
                    Color = new ExpressionOrString("color"),
                    Font = new ExpressionOrStringArray(new [] { "font"}),
                    HaloBlur = new ExpressionOrNumber(9),
                    HaloColor = new ExpressionOrString("haloColor"),
                    HaloWidth = new ExpressionOrNumber(10),
                    IgnorePlacement = true,
                    Offset = new ExpressionOrString("offset"),
                    Opacity = new ExpressionOrNumber(11),
                    Optional = true,
                    PitchAlignment = PitchAlignment.Map,
                    Rotation = new ExpressionOrNumber(12),
                    RotationAlignment = PitchAlignment.Map,
                    Size = new ExpressionOrNumber(13),
                    TextField = new ExpressionOrString("textField")
                }
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializeTileLayerOptions()
        {
            var options = new TileLayerOptions("url") {
                Filter = new ExpressionOrNumber(1),
                MaxZoom = 2,
                MinZoom = 3,
                Visible = true,
                Bounds = new BoundingBox(4, 5, 6, 7),
                Contrast = 8,
                FadeDuration = 9,
                HueRotation = 10,
                IsTMS = true,
                MaxBrightness = 11,
                MaxSourceZoom = 12,
                MinBrightness = 13,
                MinSourceZoom = 14,
                Opacity = 15,
                Saturation = 16,
                Subdomains = new[] { "subdomain" },
                TileSize = 17
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializeHtmlMarkerLayerOptions()
        {
            var options = new HtmlMarkerLayerOptions(null) {
                Filter = new ExpressionOrNumber(1),
                MaxZoom = 2,
                MinZoom = 3,
                Visible = true,
                Source = "source",
                SourceLayer = "sourceLayer",
                UpdateWhileMoving = true
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }
    }
}
