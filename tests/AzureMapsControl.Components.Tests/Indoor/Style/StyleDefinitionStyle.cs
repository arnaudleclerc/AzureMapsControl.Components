namespace AzureMapsControl.Components.Tests.Indoor.Style
{
    using System.Linq;

    using AzureMapsControl.Components.Indoor.Style;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class StyleDefinitionStyleJsonConverterTests : JsonConverterTests<StyleDefinitionStyle>
    {
        public StyleDefinitionStyleJsonConverterTests() : base(new StyleDefinitionStyleJsonConverter()) { }

        [Fact]
        public void Should_Read()
        {
            var json = "{"
                + "\"name\":\"name\""
                + ",\"theme\":\"dark\""
                + ",\"spritePath\":\"spritePath\""
                + ",\"layerGroups\":[{"
                + "\"name\":\"layerGroup1Name\""
                + ",\"layerPath\":\"layerPath\""
                + "}]"
                + "}";

            var result = Read(json);
            Assert.Equal("name", result.Name);
            Assert.Equal(StyleDefinitionStyleTheme.Dark, result.Theme);
            Assert.Equal("spritePath", result.SpritePath);
            Assert.Single(result.LayerGroups);
            Assert.Equal("layerGroup1Name", result.LayerGroups.Single().Name);
            Assert.Equal("layerPath", result.LayerGroups.Single().LayerPath);
        }
    }
}
