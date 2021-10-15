namespace AzureMapsControl.Components.Tests.Layers
{
    using System.Linq;

    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class ImageLayerOptionsJsonConverterTests : JsonConverterTests<ImageLayerOptions>
    {
        public ImageLayerOptionsJsonConverterTests() : base(new ImageLayerOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new ImageLayerOptions("url", new[] {
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

            var expectedJson = "{"
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

            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");

        [Fact]
        public void Should_Read()
        {
            var json = "{"
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

            var result = Read(json);
            Assert.Null(result.Filter);
            Assert.Equal(8, result.MaxZoom);
            Assert.Equal(9, result.MinZoom);
            Assert.True(result.Visible);
            Assert.Equal(3, result.Contrast);
            Assert.Equal(4, result.FadeDuration);
            Assert.Equal(5, result.HueRotation);
            Assert.Equal(6, result.MaxBrightness);
            Assert.Equal(7, result.MinBrightness);
            Assert.Equal(10, result.Opacity);
            Assert.Equal(11, result.Saturation);
            Assert.Equal("url", result.Url);
            Assert.Equal(2, result.Coordinates.Count());
            Assert.Contains(result.Coordinates, position => position.Longitude == 1 && position.Latitude == 2);
            Assert.Contains(result.Coordinates, position => position.Longitude == 12 && position.Latitude == 13);
        }
    }
}
