namespace AzureMapsControl.Components.Tests.Controls
{
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class PitchControlJsonConverterTests : JsonConverterTests<PitchControl>
    {
        public PitchControlJsonConverterTests() : base(new PitchControlJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var control = new PitchControl(new PitchControlOptions{
                PitchDegreesDelta = 1,
                Style = ControlStyle.Auto
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"pitchDegreesDelta\":" + control.Options.PitchDegreesDelta.Value
                + ",\"style\":\"" + control.Options.Style.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }
    }
}
