namespace AzureMapsControl.Components.Tests.Layers
{

    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class TileLayerOptionsJsonConverterTests : JsonConverterTests<TileLayerOptions>
    {
        public TileLayerOptionsJsonConverterTests() : base(new TileLayerOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new TileLayerOptions {
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

            var expectedJson = "{"
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

            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");

        [Fact]
        public void Should_Read()
        {
            var json = "{"
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

            var result = Read(json);

            Assert.Null(result.Filter);
            Assert.Equal(10, result.MaxZoom);
            Assert.Equal(13, result.MinZoom);
            Assert.True(result.Visible);
            Assert.Equal(1, result.Bounds.West);
            Assert.Equal(2, result.Bounds.South);
            Assert.Equal(3, result.Bounds.East);
            Assert.Equal(4, result.Bounds.North);
            Assert.Equal(5, result.Contrast);
            Assert.Equal(6, result.FadeDuration);
            Assert.Equal(7, result.HueRotation);
            Assert.True(result.IsTMS);
            Assert.Equal(8, result.MaxBrightness);
            Assert.Equal(9, result.MaxSourceZoom);
            Assert.Equal(11, result.MinBrightness);
            Assert.Equal(12, result.MinSourceZoom);
            Assert.Equal(14, result.Opacity);
            Assert.Equal(15, result.Saturation);
            Assert.Single(result.Subdomains);
            Assert.Contains("subdomains", result.Subdomains);
            Assert.Equal(16, result.TileSize);
            Assert.Equal("tileUrl", result.TileUrl);
        }
    }
}
