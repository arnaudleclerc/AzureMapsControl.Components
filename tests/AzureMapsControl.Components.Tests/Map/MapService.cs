
using System.Threading.Tasks;

using AzureMapsControl.Components.Map;
using AzureMap = AzureMapsControl.Components.Map.Map;

using Xunit;

namespace AzureMapsControl.Components.Tests.Map;
public class MapServiceTests
{
    [Fact]
    public async Task Should_AddMap_Async()
    {
        var map = new AzureMap("id");
        var service = new MapService(null);
        await service.AddMapAsync(map);
        Assert.Equal(map, service.Map);
    }

    [Fact]
    public async Task Should_AddMapAndTriggerOnReady_Async()
    {
        var map = new AzureMap("id");
        var service = new MapService(null);

        var eventReceived = false;

        service.OnMapReadyAsync += async () => eventReceived = true;

        await service.AddMapAsync(map);

        Assert.True(eventReceived);
    }
}

