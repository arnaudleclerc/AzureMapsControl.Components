namespace AzureMapsControl.Components.Tests.Map
{
    using AzureMapsControl.Components.Map;

    using Xunit;

    public class MapServiceTests
    {
        [Fact]
        public async void Should_AddMap_Async()
        {
            var map = new Map();
            var service = new MapService();
            await service.AddMapAsync(map);
            Assert.Equal(map, service.Map);
        }
    }
}
