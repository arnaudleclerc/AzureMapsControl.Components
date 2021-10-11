namespace AzureMapsControl.Components.Tests.Data
{
    using System.Linq;

    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class VectorTileSourceOptionsJsonConverterTests : JsonConverterTests<VectorTileSourceOptions>
    {
        public VectorTileSourceOptionsJsonConverterTests() : base(new VectorTileSourceOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new VectorTileSourceOptions {
                Bounds = new Components.Atlas.BoundingBox(1, 2, 3, 4),
                IsTMS = true,
                MaxZoom = 5,
                MinZoom = 6,
                Tiles = new[] { "tile1", "tile2" },
                TileSize = 7,
                Url = "url"
            };

            var expectedJson = "{"
                + "\"bounds\":[1,2,3,4]"
                + ",\"isTMS\":true"
                + ",\"maxZoom\":5"
                + ",\"minZoom\":6"
                + ",\"tiles\":[\"tile1\",\"tile2\"]"
                + ",\"tileSize\":7"
                + ",\"url\":\"url\""
                + "}";

            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");

        [Fact]
        public void Should_Read()
        {
            var json = "{"
                + "\"bounds\":[1,2,3,4]"
                + ",\"isTMS\":true"
                + ",\"maxZoom\":5"
                + ",\"minZoom\":6"
                + ",\"tiles\":[\"tile1\",\"tile2\"]"
                + ",\"tileSize\":7"
                + ",\"url\":\"url\""
                + "}";

            var options = Read(json);
            Assert.Equal(1, options.Bounds.West);
            Assert.Equal(2, options.Bounds.South);
            Assert.Equal(3, options.Bounds.East);
            Assert.Equal(4, options.Bounds.North);
            Assert.True(options.IsTMS);
            Assert.Equal(5, options.MaxZoom);
            Assert.Equal(6, options.MinZoom);
            Assert.Equal(2, options.Tiles.Count());
            Assert.Contains("tile1", options.Tiles);
            Assert.Contains("tile2", options.Tiles);
            Assert.Equal(7, options.TileSize);
            Assert.Equal("url", options.Url);
        }

    }
}
