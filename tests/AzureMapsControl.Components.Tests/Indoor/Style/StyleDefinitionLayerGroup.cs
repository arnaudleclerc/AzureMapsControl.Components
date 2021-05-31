namespace AzureMapsControl.Components.Tests.Indoor.Style
{
    using AzureMapsControl.Components.Indoor.Style;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class StyleDefinitionLayerGroupJsonConverterTests : JsonConverterTests<StyleDefinitionLayerGroup>
    {
        public StyleDefinitionLayerGroupJsonConverterTests() : base(new StyleDefinitionLayerGroupJsonConverter()) { }

        [Fact]
        public void Should_Read()
        {
            var json = "{"
                + "\"name\":\"name\""
                + ",\"layerPath\":\"layerPath\""
                + "}";

            var result = Read(json);
            Assert.Equal("name", result.Name);
            Assert.Equal("layerPath", result.LayerPath);
        }
    }
}
