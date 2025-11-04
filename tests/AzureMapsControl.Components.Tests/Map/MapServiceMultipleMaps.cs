namespace AzureMapsControl.Components.Tests.Map
{
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Map;

    using Xunit;

    public class MapServiceMultipleMapsTests
    {
        [Fact]
        public async Task Should_AddMultipleMaps_Async()
        {
            var map1 = new Map("map1");
            var map2 = new Map("map2");
            var map3 = new Map("map3");
            var service = new MapService(null);

            await service.AddMapAsync(map1);
            await service.AddMapAsync(map2);
            await service.AddMapAsync(map3);

            Assert.Equal(3, service.Maps.Count);
            Assert.Equal(map1, service.GetMap("map1"));
            Assert.Equal(map2, service.GetMap("map2"));
            Assert.Equal(map3, service.GetMap("map3"));
            Assert.Equal(map3, service.Map); // Latest added map
        }

        [Fact]
        public async Task Should_HandleAddingMapWithSameId_Async()
        {
            var map1 = new Map("map-id");
            var map2 = new Map("map-id"); // Same ID
            var service = new MapService(null);

            await service.AddMapAsync(map1);
            await service.AddMapAsync(map2);

            Assert.Single(service.Maps);
            Assert.Equal(map2, service.GetMap("map-id")); // Should replace first map
            Assert.Equal(map2, service.Map);
        }

        [Fact]
        public async Task Should_RemoveMap_Async()
        {
            var map1 = new Map("map1");
            var map2 = new Map("map2");
            var service = new MapService(null);

            await service.AddMapAsync(map1);
            await service.AddMapAsync(map2);
            await service.RemoveMapAsync("map1");

            Assert.Single(service.Maps);
            Assert.Null(service.GetMap("map1"));
            Assert.Equal(map2, service.GetMap("map2"));
        }

        [Fact]
        public async Task Should_RemoveLatestMap_AndUpdateLatestMapPointer_Async()
        {
            var map1 = new Map("map1");
            var map2 = new Map("map2");
            var service = new MapService(null);

            await service.AddMapAsync(map1);
            await service.AddMapAsync(map2);

            Assert.Equal(map2, service.Map); // map2 is latest

            await service.RemoveMapAsync("map2");

            Assert.Single(service.Maps);
            Assert.Null(service.GetMap("map2"));
            // After removing latest, service.Map should fall back to remaining map
            Assert.Equal(map1, service.Map);
        }

        [Fact]
        public async Task Should_RemoveNonExistentMap_WithoutError_Async()
        {
            var map1 = new Map("map1");
            var service = new MapService(null);

            await service.AddMapAsync(map1);
            await service.RemoveMapAsync("non-existent-id");

            Assert.Single(service.Maps);
            Assert.Equal(map1, service.GetMap("map1"));
        }

        [Fact]
        public async Task Should_HandleRemoveAllMaps_Async()
        {
            var map1 = new Map("map1");
            var map2 = new Map("map2");
            var service = new MapService(null);

            await service.AddMapAsync(map1);
            await service.AddMapAsync(map2);
            await service.RemoveMapAsync("map1");
            await service.RemoveMapAsync("map2");

            Assert.Empty(service.Maps);
            Assert.Null(service.Map);
        }

        [Fact]
        public async Task Should_AddMapAfterRemoval_Async()
        {
            var map1 = new Map("map1");
            var map2 = new Map("map2");
            var map3 = new Map("map1"); // Same ID as removed map
            var service = new MapService(null);

            await service.AddMapAsync(map1);
            await service.AddMapAsync(map2);
            await service.RemoveMapAsync("map1");
            await service.AddMapAsync(map3);

            Assert.Equal(2, service.Maps.Count);
            Assert.Equal(map3, service.GetMap("map1"));
            Assert.Equal(map2, service.GetMap("map2"));
            Assert.Equal(map3, service.Map); // Latest added
        }

        [Fact]
        public async Task Should_HandleConcurrentAdditions_Async()
        {
            var service = new MapService(null);
            var maps = Enumerable.Range(1, 10).Select(i => new Map($"map{i}")).ToList();

            var tasks = maps.Select(map => service.AddMapAsync(map));
            await Task.WhenAll(tasks.Select(t => t.AsTask()));

            Assert.Equal(10, service.Maps.Count);
            foreach (var map in maps)
            {
                Assert.Equal(map, service.GetMap(map.Id));
            }
        }

        [Fact]
        public async Task Should_TriggerOnMapReadyForEachMap_Async()
        {
            var map1 = new Map("map1");
            var map2 = new Map("map2");
            var service = new MapService(null);

            var readyMaps = new System.Collections.Generic.List<string>();

            service.OnMapReadyAsync += async (map) => {
                readyMaps.Add(map.Id);
                await ValueTask.CompletedTask;
            };

            await service.AddMapAsync(map1);
            await service.AddMapAsync(map2);

            Assert.Equal(2, readyMaps.Count);
            Assert.Contains("map1", readyMaps);
            Assert.Contains("map2", readyMaps);
        }

        [Fact]
        public void Should_ReturnNull_WhenNoMapsExist()
        {
            var service = new MapService(null);

            Assert.Null(service.Map);
            Assert.Empty(service.Maps);
        }

        [Fact]
        public async Task Should_ReturnNull_ForNonExistentMapId()
        {
            var service = new MapService(null);
            await service.AddMapAsync(new Map("map1"));

            var result = service.GetMap("non-existent-id");

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_HandleRapidAddRemoveSequence_Async()
        {
            var service = new MapService(null);

            for (int i = 0; i < 5; i++)
            {
                var map = new Map($"map{i}");
                await service.AddMapAsync(map);
                await service.RemoveMapAsync($"map{i}");
            }

            Assert.Empty(service.Maps);
            Assert.Null(service.Map);
        }
    }
}
