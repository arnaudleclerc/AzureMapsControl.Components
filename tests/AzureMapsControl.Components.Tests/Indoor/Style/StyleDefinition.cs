namespace AzureMapsControl.Components.Tests.Indoor.Style
{
    using System.Linq;

    using AzureMapsControl.Components.Indoor.Style;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class StyleDefinitionJsonConverterTests : JsonConverterTests<StyleDefinition>
    {
        public StyleDefinitionJsonConverterTests() : base(new StyleDefinitionJsonConverter()) { }

        [Fact]
        public void Should_Read()
        {
            var json = "{"
                + "\"domain\":\"domain\""
                + ",\"version\":\"version\""
                + ",\"styles\":["
                + "{"
                + "\"name\":\"name\""
                + ",\"theme\":\"dark\""
                + ",\"spritePath\":\"spritePath\""
                + ",\"layerGroups\":[{"
                + "\"name\":\"layerGroup1Name\""
                + ",\"layerPath\":\"layerPath\""
                + "}]"
                + "}]"
                + "}";

            var result = Read(json);

            Assert.Equal("domain", result.Domain);
            Assert.Equal("version", result.Version);
            Assert.Single(result.Styles);

            var style = result.Styles.Single();
            Assert.Equal("name", style.Name);
            Assert.Equal(StyleDefinitionStyleTheme.Dark, style.Theme);
            Assert.Equal("spritePath", style.SpritePath);
            Assert.Single(style.LayerGroups);

            var layerGroup = style.LayerGroups.Single();
            Assert.Equal("layerGroup1Name", layerGroup.Name);
            Assert.Equal("layerPath", layerGroup.LayerPath);
        }
    }
}
