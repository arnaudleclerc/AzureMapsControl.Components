namespace AzureMapsControl.Components.Tests.Indoor
{
    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Indoor;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class LevelControlJsonConverterTests : JsonConverterTests<LevelControl>
    {
        public LevelControlJsonConverterTests(): base(new LevelControlJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var levelControl = new LevelControl(new LevelControlOptions {
                Position = ControlPosition.BottomLeft,
                Style = ControlStyle.Auto
            });

            var expectedJson = "{"
                + "\"options\":{"
                + "\"position\":\"bottom-left\""
                + ",\"stype\":\"auto\""
                + "}"
                + "}";

            TestAndAssertWrite(levelControl, expectedJson);
        }
    }
}
