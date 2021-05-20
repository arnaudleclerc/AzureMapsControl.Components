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
                TilesetId = "tilesetId"
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
                + "}";

            TestAndAssertWrite(options, expectedJson);
        }
    }
}
