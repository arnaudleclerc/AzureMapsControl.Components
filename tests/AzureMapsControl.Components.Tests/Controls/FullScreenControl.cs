namespace AzureMapsControl.Components.Tests.Controls
{
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class FullScreenControlJsonConverterTests : JsonConverterTests<FullScreenControl>
    {
        public FullScreenControlJsonConverterTests() : base(new FullScreenControlJsonConverter()) { }

        [Fact]
        public void Should_Write_WithControlStyle()
        {
            var control = new FullScreenControl(new FullScreenControlOptions {
                Container = "container",
                HideIfUnsupported = true,
                Style = new Components.Atlas.Either<ControlStyle, string>(ControlStyle.Auto)
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"container\":\"" + control.Options.Container + "\""
                + ",\"hideIfUnsupported\":" + control.Options.HideIfUnsupported
                + ",\"style\":\"" + control.Options.Style.FirstChoice.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

        [Fact]
        public void Should_Write_WithCssStyle()
        {
            var control = new FullScreenControl(new FullScreenControlOptions {
                Container = "container",
                HideIfUnsupported = true,
                Style = new Components.Atlas.Either<ControlStyle, string>("#000000")
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"container\":\"" + control.Options.Container + "\""
                + ",\"hideIfUnsupported\":" + control.Options.HideIfUnsupported
                + ",\"style\":\"" + control.Options.Style.SecondChoice + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

    }
}
