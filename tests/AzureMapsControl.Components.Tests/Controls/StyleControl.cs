namespace AzureMapsControl.Components.Tests.Controls
{
    using System.Linq;

    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class StyleControlJsonConverterTests : JsonConverterTests<StyleControl>
    {
        public StyleControlJsonConverterTests() : base(new StyleControlJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var control = new StyleControl(new StyleControlOptions {
                MapStyles = new[] { MapStyle.Blank, MapStyle.GrayscaleDark },
                Style = ControlStyle.Auto,
                StyleControlLayout = StyleControlLayout.Icons
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"mapStyles\":[\"" + control.Options.MapStyles.First().ToString() + "\",\"" + control.Options.MapStyles.ElementAt(1).ToString() + "\"]"
                + ",\"style\":\"" + control.Options.Style.ToString() + "\""
                + ",\"styleControlLayout\":\"" + control.Options.StyleControlLayout.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }
    }
}
