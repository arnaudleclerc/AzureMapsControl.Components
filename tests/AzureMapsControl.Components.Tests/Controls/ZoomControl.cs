namespace AzureMapsControl.Components.Tests.Controls
{
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class ZoomControlJsonConverterTests : JsonConverterTests<ZoomControl>
    {
        public ZoomControlJsonConverterTests() : base(new ZoomControlJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var control = new ZoomControl(new ZoomControlOptions {
                Style = ControlStyle.Auto,
                ZoomDelta = 1
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"style\":\"" + control.Options.Style.ToString() + "\""
                + ",\"zoomDelta\":" + control.Options.ZoomDelta.Value
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }
    }
}
