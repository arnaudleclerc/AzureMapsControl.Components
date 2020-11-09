namespace AzureMapsControl.Components.Tests.Map
{
    using System.Linq;

    using AzureMapsControl.Components.Map;

    using Xunit;

    public class MapEventsActivationFlags
    {
        [Fact]
        public void Should_HaveEverything_Disabled()
        {
            var mapEventActivationFlags = MapEventActivationFlags.None();
            Assert.Empty(mapEventActivationFlags.EnabledEvents);
        }

        [Fact]
        public void Should_HaveEverything_Enabled()
        {
            var mapEventActivationFlags = MapEventActivationFlags.All();
            Assert.NotEmpty(mapEventActivationFlags.EnabledEvents);
        }

        [Fact]
        public void Should_EnableOne()
        {
            var mapEventActivationFlags = MapEventActivationFlags.None().Enable(MapEventType.Click);
            Assert.Single(mapEventActivationFlags.EnabledEvents);
            Assert.Equal(mapEventActivationFlags.EnabledEvents.First(), MapEventType.Click.ToString());
        }

        [Fact]
        public void Should_DisableOne()
        {
            var mapEventActivationFlags = MapEventActivationFlags.All().Disable(MapEventType.Click);
            Assert.NotEmpty(mapEventActivationFlags.EnabledEvents);
            Assert.DoesNotContain(mapEventActivationFlags.EnabledEvents.First(), MapEventType.Click.ToString());
        }
    }
}
