namespace AzureMapsControl.Components.Tests.Markers
{
    using System.Linq;

    using AzureMapsControl.Components.Markers;

    using Xunit;

    public class HtmlMarkerEventActivationFlagsTests
    {
        [Fact]
        public void Should_HaveEverything_Disabled()
        {
            var htmlMarkerEventActivationFlags = HtmlMarkerEventActivationFlags.None();
            Assert.Empty(htmlMarkerEventActivationFlags.EnabledEvents);
        }

        [Fact]
        public void Should_HaveEverything_Enabled()
        {
            var htmlMarkerEventActivationFlags = HtmlMarkerEventActivationFlags.All();
            Assert.NotEmpty(htmlMarkerEventActivationFlags.EnabledEvents);
        }

        [Fact]
        public void Should_EnableOne()
        {
            var htmlMarkerEventActivationFlags = HtmlMarkerEventActivationFlags.None().Enable(HtmlMarkerEventType.Click);
            Assert.Single(htmlMarkerEventActivationFlags.EnabledEvents);
            Assert.Equal(htmlMarkerEventActivationFlags.EnabledEvents.First(), HtmlMarkerEventType.Click.ToString());
        }

        [Fact]
        public void Should_DisableOne()
        {
            var htmlMarkerEventActivationFlags = HtmlMarkerEventActivationFlags.All().Disable(HtmlMarkerEventType.Click);
            Assert.NotEmpty(htmlMarkerEventActivationFlags.EnabledEvents);
            Assert.DoesNotContain(HtmlMarkerEventType.Click.ToString(), htmlMarkerEventActivationFlags.EnabledEvents);
        }
    }
}
