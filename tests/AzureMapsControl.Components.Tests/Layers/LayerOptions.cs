namespace AzureMapsControl.Components.Tests.Layers
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class LayerOptionsJsonConverterTests : JsonConverterTests<LayerOptions>
    {
        #region BubbleLayerOptions
        public static object BubbleLayerOptions = new BubbleLayerOptions {
            Blur = new Components.Atlas.ExpressionOrNumber(1),
            Color = new Components.Atlas.ExpressionOrString("color"),
            Filter = new Components.Atlas.ExpressionOrString("filter"),
            MaxZoom = 2,
            MinZoom = 3,
            Opacity = new Components.Atlas.ExpressionOrNumber(4),
            PitchAlignment = Components.Atlas.PitchAlignment.Auto,
            Radius = new Components.Atlas.ExpressionOrNumber(5),
            Source = "source",
            SourceLayer = "sourceLayer",
            StrokeColor = new Components.Atlas.ExpressionOrString("strokeColor"),
            StrokeOpacity = new Components.Atlas.ExpressionOrNumber(6),
            StrokeWidth = new Components.Atlas.ExpressionOrNumber(7),
            Visible = true
        };

        public static object BubbleLayerOptionsExpectedJson = "{"
                + "\"filter\":\"filter\""
                + ",\"maxZoom\":2"
                + ",\"minZoom\":3"
                + ",\"visible\":true"
                + ",\"source\":\"source\""
                + ",\"sourceLayer\":\"sourceLayer\""
                + ",\"blur\":1"
                + ",\"color\":\"color\""
                + ",\"opacity\":4"
                + ",\"pitchAlignment\":\"auto\""
                + ",\"radius\":5"
                + ",\"strokeColor\":\"strokeColor\""
                + ",\"strokeOpacity\":6"
                + ",\"strokeWidth\":7"
                + "}";

        #endregion

        #region HeatmapLayerOptions

        public static object HeatmapLayerOptions = new HeatmapLayerOptions {
            Color = new Components.Atlas.ExpressionOrString("color"),
            Filter = new Components.Atlas.ExpressionOrString("filter"),
            Intensity = new Components.Atlas.ExpressionOrNumber(1),
            MaxZoom = 2,
            MinZoom = 3,
            Opacity = new Components.Atlas.ExpressionOrNumber(4),
            Radius = new Components.Atlas.ExpressionOrNumber(5),
            Source = "source",
            SourceLayer = "sourceLayer",
            Visible = true,
            Weight = new Components.Atlas.ExpressionOrNumber(6)
        };

        public static object HeatmapLayerOptionsExpectedJson = "{"
                + "\"filter\":\"filter\""
                + ",\"maxZoom\":2"
                + ",\"minZoom\":3"
                + ",\"visible\":true"
                + ",\"source\":\"source\""
                + ",\"sourceLayer\":\"sourceLayer\""
                + ",\"color\":\"color\""
                + ",\"intensity\":1"
                + ",\"opacity\":4"
                + ",\"radius\":5"
                + ",\"weight\":6"
                + "}";

        #endregion

        #region ImageLayerOptions
        public static object ImageLayerOptions = new ImageLayerOptions("url", new[] {
                    new Components.Atlas.Position(1,2),
                    new Components.Atlas.Position(12,13),
                }) {
            Contrast = 3,
            FadeDuration = 4,
            Filter = new Components.Atlas.ExpressionOrString("filter"),
            HueRotation = 5,
            MaxBrightness = 6,
            MinBrightness = 7,
            MaxZoom = 8,
            MinZoom = 9,
            Opacity = 10,
            Saturation = 11,
            Visible = true
        };

        public static object ImageLayerOptionsExpectedJson = "{"
                + "\"filter\":\"filter\""
                + ",\"maxZoom\":8"
                + ",\"minZoom\":9"
                + ",\"visible\":true"
                + ",\"contrast\":3"
                + ",\"fadeDuration\":4"
                + ",\"hueRotation\":5"
                + ",\"maxBrightness\":6"
                + ",\"minBrightness\":7"
                + ",\"opacity\":10"
                + ",\"saturation\":11"
                + ",\"url\":\"url\""
                + ",\"coordinates\":[[1,2],[12,13]]"
                + "}";

        #endregion

        #region HeatmapLayerOptions

        public static object PolygonExtrusionLayerOptions = new PolygonExtrusionLayerOptions {
            Base = new Components.Atlas.ExpressionOrNumber(1),
            FillColor = new Components.Atlas.ExpressionOrString("fillColor"),
            FillOpacity = 2,
            FillPattern = "fillPattern",
            Filter = new Components.Atlas.ExpressionOrString("filter"),
            Height = new Components.Atlas.ExpressionOrNumber(3),
            MaxZoom = 4,
            MinZoom = 5,
            Source = "source",
            SourceLayer = "sourceLayer",
            Translate = new Components.Atlas.Pixel(6, 7),
            TranslateAnchor = Components.Atlas.PitchAlignment.Auto,
            VerticalGradient = true,
            Visible = true
        };

        public static object PolygonExtrusionLayerOptionsExpectedJson = "{"
                + "\"filter\":\"filter\""
                + ",\"maxZoom\":4"
                + ",\"minZoom\":5"
                + ",\"visible\":true"
                + ",\"source\":\"source\""
                + ",\"sourceLayer\":\"sourceLayer\""
                + ",\"base\":1"
                + ",\"fillColor\":\"fillColor\""
                + ",\"fillOpacity\":2"
                + ",\"fillPattern\":\"fillPattern\""
                + ",\"height\":3"
                + ",\"translate\":[6,7]"
                + ",\"translateAnchor\":\"auto\""
                + ",\"verticalGradient\":true"
                + "}";

        #endregion

        #region SourceLayerOptions

        public static object SymbolLayerOptions = new SymbolLayerOptions {
            Filter = new Components.Atlas.ExpressionOrString("filter"),
            IconOptions = new IconOptions {
                AllowOverlap = true
            },
            LineSpacing = new Components.Atlas.ExpressionOrNumber(1),
            MaxZoom = 2,
            MinZoom = 3,
            Placement = SymbolLayerPlacement.Line,
            Source = "source",
            SourceLayer = "sourceLayer",
            TextOptions = new TextOptions {
                AllowOverlap = true
            },
            Visible = true
        };

        public static object SymbolLayerOptionsExpectedJson = "{"
                + "\"filter\":\"filter\""
                + ",\"maxZoom\":2"
                + ",\"minZoom\":3"
                + ",\"visible\":true"
                + ",\"source\":\"source\""
                + ",\"sourceLayer\":\"sourceLayer\""
                + ",\"iconOptions\":{"
                + "\"allowOverlap\":true"
                + "}"
                + ",\"lineSpacing\":1"
                + ",\"placement\":\"line\""
                + ",\"textOptions\":{"
                + "\"allowOverlap\":true"
                + "}"
                + "}";

        #endregion

        #region TileLayerOptions
        public static object TileLayerOptions = new TileLayerOptions {
            Bounds = new Components.Atlas.BoundingBox(1, 2, 3, 4),
            Contrast = 5,
            FadeDuration = 6,
            Filter = new Components.Atlas.ExpressionOrString("filter"),
            HueRotation = 7,
            IsTMS = true,
            MaxBrightness = 8,
            MaxSourceZoom = 9,
            MaxZoom = 10,
            MinBrightness = 11,
            MinSourceZoom = 12,
            MinZoom = 13,
            Opacity = 14,
            Saturation = 15,
            Subdomains = new[] { "subdomains" },
            TileSize = 16,
            TileUrl = "tileUrl",
            Visible = true
        };

        public static object TileLayerOptionsExpectedJson = "{"
                + "\"filter\":\"filter\""
                + ",\"maxZoom\":10"
                + ",\"minZoom\":13"
                + ",\"visible\":true"
                + ",\"bounds\":[1,2,3,4]"
                + ",\"contrast\":5"
                + ",\"fadeDuration\":6"
                + ",\"hueRotation\":7"
                + ",\"isTMS\":true"
                + ",\"maxBrightness\":8"
                + ",\"maxSourceZoom\":9"
                + ",\"minBrightness\":11"
                + ",\"minSourceZoom\":12"
                + ",\"opacity\":14"
                + ",\"saturation\":15"
                + ",\"subdomains\":[\"subdomains\"]"
                + ",\"tileSize\":16"
                + ",\"tileUrl\":\"tileUrl\""
                + "}";

        #endregion

        #region Member Data
        public static IEnumerable<object[]> OptionsWithExpectedJson => new List<object[]> {
            new object[] {BubbleLayerOptions, BubbleLayerOptionsExpectedJson},
            new object[] {HeatmapLayerOptions, HeatmapLayerOptionsExpectedJson},
            new object[] {ImageLayerOptions, ImageLayerOptionsExpectedJson},
            new object[] {PolygonExtrusionLayerOptions, PolygonExtrusionLayerOptionsExpectedJson},
            new object[] {SymbolLayerOptions, SymbolLayerOptionsExpectedJson},
            new object[] {TileLayerOptions, TileLayerOptionsExpectedJson}
        };

        #endregion

        public LayerOptionsJsonConverterTests() : base(new LayerOptionsJsonConverter()) { }

        [Theory]
        [MemberData(nameof(OptionsWithExpectedJson))]
        public void Should_Write(LayerOptions options, string expectedJson) => TestAndAssertWrite(options, expectedJson);

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");
    }
}
