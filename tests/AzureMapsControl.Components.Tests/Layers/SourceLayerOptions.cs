namespace AzureMapsControl.Components.Tests.Layers
{
    using System;
    using System.Collections.Generic;

    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class SourceLayerOptionsJsonConverterTests : JsonConverterTests<SourceLayerOptions>
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

        #region Member Data

        public static IEnumerable<object[]> OptionsWithExpectedJson => new List<object[]> {
            new object[] {BubbleLayerOptions, BubbleLayerOptionsExpectedJson},
            new object[] {HeatmapLayerOptions, HeatmapLayerOptionsExpectedJson},
            new object[] {PolygonExtrusionLayerOptions, PolygonExtrusionLayerOptionsExpectedJson},
            new object[] {SymbolLayerOptions, SymbolLayerOptionsExpectedJson}
        };

        #endregion

        public SourceLayerOptionsJsonConverterTests() : base(new SourceLayerOptionsJsonConverter()) { }

        [Theory]
        [MemberData(nameof(OptionsWithExpectedJson))]
        public void Should_Write(SourceLayerOptions options, string expectedJson) => TestAndAssertWrite(options, expectedJson);

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");
    }
}
