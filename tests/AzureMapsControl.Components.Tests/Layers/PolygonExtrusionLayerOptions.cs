namespace AzureMapsControl.Components.Tests.Layers
{

    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class PolygonExtrusionLayerOptionsJsonConverterTests : JsonConverterTests<PolygonExtrusionLayerOptions>
    {
        public PolygonExtrusionLayerOptionsJsonConverterTests() : base(new PolygonExtrusionLayerOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new PolygonExtrusionLayerOptions {
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

            var expectedJson = "{"
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

            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");

        [Fact]
        public void Should_Read()
        {
            var json = "{"
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

            var result = Read(json);

            Assert.Null(result.Base);
            Assert.Null(result.FillColor);
            Assert.Equal(2, result.FillOpacity);
            Assert.Equal("fillPattern", result.FillPattern);
            Assert.Null(result.Filter);
            Assert.Null(result.Height);
            Assert.Equal(4, result.MaxZoom);
            Assert.Equal(5, result.MinZoom);
            Assert.Equal("source", result.Source);
            Assert.Equal("sourceLayer", result.SourceLayer);
            Assert.Equal(6, result.Translate.X);
            Assert.Equal(7, result.Translate.Y);
            Assert.Equal(Components.Atlas.PitchAlignment.Auto, result.TranslateAnchor);
            Assert.True(result.VerticalGradient);
            Assert.True(result.Visible);
        }
    }
}
