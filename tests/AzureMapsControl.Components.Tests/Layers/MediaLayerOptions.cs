namespace AzureMapsControl.Components.Tests.Layers
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class MediaLayerOptionsJsonConverterTests : JsonConverterTests<MediaLayerOptions>
    {
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
            new object[] {ImageLayerOptions, ImageLayerOptionsExpectedJson},
            new object[] {TileLayerOptions, TileLayerOptionsExpectedJson}
        };

        #endregion

        public MediaLayerOptionsJsonConverterTests() : base(new MediaLayerOptionsJsonConverter()) { }

        [Theory]
        [MemberData(nameof(OptionsWithExpectedJson))]
        public void Should_Write(MediaLayerOptions options, string expectedJson) => TestAndAssertWrite(options, expectedJson);

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");
    }
}
