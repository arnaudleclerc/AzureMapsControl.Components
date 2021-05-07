namespace AzureMapsControl.Components.Tests.Controls
{
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class ScalebarControlJsonConverterTests : JsonConverterTests<ScaleBarControl>
    {
        public ScalebarControlJsonConverterTests(): base(new ScaleBarControlJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var control = new ScaleBarControl(new ScaleBarControlOptions {
                MaxBarLength = 1,
                Units = ScaleBarControlUnits.Metric
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"maxBarLength\":" + control.Options.MaxBarLength.Value
                + ",\"units\":\"" + control.Options.Units.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }
    }
}
