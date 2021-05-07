namespace AzureMapsControl.Components.Tests.Controls
{

    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class CompassControlJsonConverterTests : JsonConverterTests<CompassControl>
    {
        public CompassControlJsonConverterTests() : base(new CompassControlJsonConverter())
        {
        }

        [Fact]
        public void Should_Write()
        {
            var control = new CompassControl(new CompassControlOptions {
                RotationDegreesDelta = 1,
                Style = ControlStyle.Auto
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"rotationDegreesDelta\":" + control.Options.RotationDegreesDelta.Value
                + ",\"style\":\"" + control.Options.Style.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }
    }
}
