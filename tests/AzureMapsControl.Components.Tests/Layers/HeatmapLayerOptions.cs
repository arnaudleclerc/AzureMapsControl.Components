namespace AzureMapsControl.Components.Tests.Layers
{

    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class HeatmapLayerOptionsJsonConverterTests : JsonConverterTests<HeatmapLayerOptions>
    {
        public HeatmapLayerOptionsJsonConverterTests() : base(new HeatmapLayerOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new HeatmapLayerOptions {
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

            var expectedJson = "{"
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

            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");

        [Fact]
        public void Should_Read()
        {
            var json = "{"
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

            var result = Read(json);
            Assert.Null(result.Filter);
            Assert.Equal(2, result.MaxZoom);
            Assert.Equal(3, result.MinZoom);
            Assert.True(result.Visible);
            Assert.Equal("source", result.Source);
            Assert.Equal("sourceLayer", result.SourceLayer);
            Assert.Null(result.Color);
            Assert.Null(result.Intensity);
            Assert.Null(result.Opacity);
            Assert.Null(result.Radius);
            Assert.Null(result.Weight);
        }
    }
}
