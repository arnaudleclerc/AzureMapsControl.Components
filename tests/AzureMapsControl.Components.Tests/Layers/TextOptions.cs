namespace AzureMapsControl.Components.Tests.Layers
{

    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class TextOptionsJsonConverterTests : JsonConverterTests<TextOptions>
    {
        public TextOptionsJsonConverterTests() : base(new TextOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new TextOptions {
                AllowOverlap = true,
                Anchor = new Components.Atlas.ExpressionOrString("anchor"),
                Color = new Components.Atlas.ExpressionOrString("color"),
                Font = new Components.Atlas.ExpressionOrStringArray(new[] { "font" }),
                HaloBlur = new Components.Atlas.ExpressionOrNumber(1),
                HaloColor = new Components.Atlas.ExpressionOrString("haloColor"),
                HaloWidth = new Components.Atlas.ExpressionOrNumber(2),
                IgnorePlacement = true,
                Offset = new Components.Atlas.ExpressionOrNumber(3),
                Opacity = new Components.Atlas.ExpressionOrNumber(4),
                Optional = true,
                PitchAlignment = Components.Atlas.PitchAlignment.Auto,
                Rotation = new Components.Atlas.ExpressionOrNumber(5),
                RotationAlignment = Components.Atlas.PitchAlignment.Map,
                Size = new Components.Atlas.ExpressionOrNumber(6),
                TextField = new Components.Atlas.ExpressionOrString("textField")
            };

            var expectedJson = "{"
                + "\"allowOverlap\":true"
                + ",\"anchor\":\"anchor\""
                + ",\"color\":\"color\""
                + ",\"font\":[\"font\"]"
                + ",\"haloBlur\":1"
                + ",\"haloColor\":\"haloColor\""
                + ",\"haloWidth\":2"
                + ",\"ignorePlacement\":true"
                + ",\"offset\":3"
                + ",\"opacity\":4"
                + ",\"optional\":true"
                + ",\"pitchAlignment\":\"auto\""
                + ",\"rotation\":5"
                + ",\"rotationAlignment\":\"map\""
                + ",\"size\":6"
                + ",\"textField\":\"textField\""
                + "}";

            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");

        [Fact]
        public void Should_Read()
        {
            var json = "{"
                + "\"allowOverlap\":true"
                + ",\"anchor\":\"anchor\""
                + ",\"color\":\"color\""
                + ",\"font\":[\"font\"]"
                + ",\"haloBlur\":1"
                + ",\"haloColor\":\"haloColor\""
                + ",\"haloWidth\":2"
                + ",\"ignorePlacement\":true"
                + ",\"offset\":3"
                + ",\"opacity\":4"
                + ",\"optional\":true"
                + ",\"pitchAlignment\":\"auto\""
                + ",\"rotation\":5"
                + ",\"rotationAlignment\":\"map\""
                + ",\"size\":6"
                + ",\"textField\":\"textField\""
                + "}";

            var result = Read(json);
            Assert.True(result.AllowOverlap);
            Assert.Null(result.Anchor);
            Assert.Null(result.Color);
            Assert.Null(result.Font);
            Assert.Null(result.HaloBlur);
            Assert.Null(result.HaloColor);
            Assert.Null(result.HaloWidth);
            Assert.True(result.IgnorePlacement);
            Assert.Null(result.Offset);
            Assert.True(result.Optional);
            Assert.Equal(Components.Atlas.PitchAlignment.Auto, result.PitchAlignment);
            Assert.Null(result.Rotation);
            Assert.Equal(Components.Atlas.PitchAlignment.Map, result.RotationAlignment);
            Assert.Null(result.Size);
            Assert.Null(result.TextField);
        }
    }
}
