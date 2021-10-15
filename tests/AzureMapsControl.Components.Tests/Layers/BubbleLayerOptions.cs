namespace AzureMapsControl.Components.Tests.Layers
{
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class BubbleLayerOptionsJsonConverterTests : JsonConverterTests<BubbleLayerOptions>
    {
        public BubbleLayerOptionsJsonConverterTests() : base(new BubbleLayerOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new BubbleLayerOptions {
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

            var expectedJson = "{"
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
                + ",\"blur\":1"
                + ",\"color\":\"color\""
                + ",\"opacity\":4"
                + ",\"pitchAlignment\":\"auto\""
                + ",\"radius\":5"
                + ",\"strokeColor\":\"strokeColor\""
                + ",\"strokeOpacity\":6"
                + ",\"strokeWidth\":7"
                + "}";

            var result = Read(json);
            Assert.Null(result.Filter);
            Assert.Equal(2, result.MaxZoom);
            Assert.Equal(3, result.MinZoom);
            Assert.True(result.Visible);
            Assert.Equal("source", result.Source);
            Assert.Equal("sourceLayer", result.SourceLayer);
            Assert.Null(result.Blur);
            Assert.Null(result.Color);
            Assert.Null(result.Opacity);
            Assert.Equal(Components.Atlas.PitchAlignment.Auto, result.PitchAlignment);
            Assert.Null(result.Radius);
            Assert.Null(result.StrokeColor);
            Assert.Null(result.StrokeOpacity);
            Assert.Null(result.StrokeWidth);
        }
    }
}
