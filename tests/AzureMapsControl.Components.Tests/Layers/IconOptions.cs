namespace AzureMapsControl.Components.Tests.Layers
{
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class IconOptionsJsonConverterTests : JsonConverterTests<IconOptions>
    {
        public IconOptionsJsonConverterTests(): base(new IconOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new IconOptions {
                AllowOverlap = true,
                Anchor = new Components.Atlas.ExpressionOrString("anchor"),
                IgnorePlacement = true,
                Image = new Components.Atlas.ExpressionOrString("image"),
                Offset = new Components.Atlas.ExpressionOrNumber(1),
                Opacity = new Components.Atlas.ExpressionOrNumber(2),
                Optional = true,
                PitchAlignment = Components.Atlas.PitchAlignment.Auto,
                Rotation = new Components.Atlas.ExpressionOrNumber(3),
                RotationAlignment = Components.Atlas.PitchAlignment.Map,
                Size = new Components.Atlas.ExpressionOrNumber(4)
            };

            var expectedJson = "{"
                + "\"allowOverlap\":true"
                + ",\"anchor\":\"anchor\""
                + ",\"ignorePlacement\":true"
                + ",\"image\":\"image\""
                + ",\"offset\":1"
                + ",\"opacity\":2"
                + ",\"optional\":true"
                + ",\"pitchAlignment\":\"auto\""
                + ",\"rotation\":3"
                + ",\"rotationAlignment\":\"map\""
                + ",\"size\":4"
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
                + ",\"ignorePlacement\":true"
                + ",\"image\":\"image\""
                + ",\"offset\":1"
                + ",\"opacitity\":2"
                + ",\"optional\":true"
                + ",\"pitchAlignment\":\"auto\""
                + ",\"rotation\":3"
                + ",\"rotationAlignment\":\"map\""
                + ",\"size\":4"
                + "}";

            var result = Read(json);
            Assert.True(result.AllowOverlap);
            Assert.Null(result.Anchor);
            Assert.True(result.IgnorePlacement);
            Assert.Null(result.Image);
            Assert.Null(result.Offset);
            Assert.Null(result.Opacity);
            Assert.True(result.Optional);
            Assert.Equal(Components.Atlas.PitchAlignment.Auto, result.PitchAlignment);
            Assert.Null(result.Rotation);
            Assert.Equal(Components.Atlas.PitchAlignment.Map, result.RotationAlignment);
            Assert.Null(result.Size);
        }
    }
}
