namespace AzureMapsControl.Components.Tests.Indoor
{
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Indoor;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class IndoorManagerOptionsJsonConverterTests : JsonConverterTests<IndoorManagerOptions>
    {
        public IndoorManagerOptionsJsonConverterTests() : base(new IndoorManagerOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var levelControl = new LevelControl(new LevelControlOptions {
                Position = ControlPosition.BottomLeft,
                Style = ControlStyle.Auto
            });

            var options = new IndoorManagerOptions {
                LevelControl = levelControl,
                StatesetId = "statesetId",
                Theme = IndoorLayerTheme.Auto,
                TilesetId = "tilesetId",
                Geography = "us"
            };

            var expectedJson = "{"
                + "\"levelControl\":{"
                + "\"options\":{"
                + "\"position\":\"bottom-left\""
                + ",\"style\":\"auto\""
                + "}"
                + "}"
                + ",\"statesetId\":\"statesetId\""
                + ",\"theme\":\"auto\""
                + ",\"tilesetId\":\"tilesetId\""
                + ",\"geography\":\"us\""
                + "}";

            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_Read()
        {
            var json = "{"
                + "\"statesetId\":\"statesetId\""
                + ",\"theme\":\"auto\""
                + ",\"tilesetId\":\"tilesetId\""
                + "}";

            var result = Read(json);
            Assert.Equal("statesetId", result.StatesetId);
            Assert.Equal(IndoorLayerTheme.Auto, result.Theme);
            Assert.Equal("tilesetId", result.TilesetId);
        }
    }
}
