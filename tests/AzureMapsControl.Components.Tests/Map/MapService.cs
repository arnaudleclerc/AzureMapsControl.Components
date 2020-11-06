namespace AzureMapsControl.Components.Tests.Map
{
    using AzureMapsControl.Components.Map;

    using Xunit;

    public class MapServiceTests
    {
        [Fact]
        public async void Should_AddMap_Async()
        {
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var service = new MapService();
            await service.AddMapAsync(map);
            Assert.Equal(map, service.Map);
        }

        [Fact]
        public async void Should_AddMapAndTriggerOnReady_Async()
        {
            var map = new Map(null, null, null, null, null, null, null, null, null, null);
            var service = new MapService();

            var eventReceived = false;

            service.OnMapReadyAsync += async () => eventReceived = true;

            await service.AddMapAsync(map);

            Assert.True(eventReceived);
        }
    }
}
