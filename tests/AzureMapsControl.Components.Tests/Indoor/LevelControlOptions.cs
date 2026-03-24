
using AzureMapsControl.Components.Indoor;
using AzureMapsControl.Components.Tests.Json;

using Xunit;

namespace AzureMapsControl.Components.Tests.Indoor;
public class LevelControlOptionsJsonConverterTests : JsonConverterTests<LevelControlOptions>
{
    public LevelControlOptionsJsonConverterTests(): base(new LevelControlOptionsJsonConverter()) { }

    [Fact]
    public void Should_Write()
    {
        var options = new LevelControlOptions {
            Position = Components.Controls.ControlPosition.BottomLeft,
            Style = Components.Controls.ControlStyle.Auto
        };

        var expectedJson = "{"
            + "\"position\":\"bottom-left\""
            + ",\"style\":\"auto\""
            + "}";

        TestAndAssertWrite(options, expectedJson);
    }
}
