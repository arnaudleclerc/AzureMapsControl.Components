namespace AzureMapsControl.Components.Tests.Map
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Map;

    using Xunit;

    public class MapServiceTests
    {
        [Fact]
        public async Task Should_AddMap_Async()
        {
            var map = new Map("id");
            var service = new MapService(null);
            await service.AddMapAsync(map);
            Assert.Equal(map, service.Map);
        }

        [Fact]
        public async Task Should_AddMapAndTriggerOnReady_Async()
        {
            var map = new Map("id");
            var service = new MapService(null);

            var eventReceived = false;

            service.OnMapReadyAsync += async () => eventReceived = true;

            await service.AddMapAsync(map);

            Assert.True(eventReceived);
        }
    }
}
