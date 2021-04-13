namespace AzureMapsControl.Components.Tests.Geolocation
{
    using System.Linq;

    using AzureMapsControl.Components.Geolocation;

    using Xunit;
    public class GeolocationEventActivationFlagsTests
    {
        [Fact]
        public void Should_HaveEverything_Disabled()
        {
            var events = GeolocationEventActivationFlags.None();
            Assert.Empty(events.EnabledEvents);
        }

        [Fact]
        public void Should_HaveEverything_Enabled()
        {
            var events = GeolocationEventActivationFlags.All();
            Assert.NotEmpty(events.EnabledEvents);
        }

        [Fact]
        public void Should_EnableOne()
        {
            var events = GeolocationEventActivationFlags.None().Enable(GeolocationEventType.GeolocationError);
            Assert.Single(events.EnabledEvents);
            Assert.Equal(events.EnabledEvents.First(), GeolocationEventType.GeolocationError.ToString());
        }

        [Fact]
        public void Should_DisableOne()
        {
            var events = GeolocationEventActivationFlags.All().Disable(GeolocationEventType.GeolocationError);
            Assert.NotEmpty(events.EnabledEvents);
            Assert.DoesNotContain(GeolocationEventType.GeolocationError.ToString(), events.EnabledEvents);
        }
    }
}
