namespace AzureMapsControl.Components.Tests.Data
{
    using System.Text.Json;

    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Data.Grid;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class SourceOptionsJsonConverterTests : JsonConverterTests<SourceOptions>
    {
        public SourceOptionsJsonConverterTests() : base(new SourceOptionsJsonConverter()) { }

        [Fact]
        public void Should_SerializeDataSourceOptions()
        {
            var options = new DataSourceOptions {
                Buffer = 1,
                Cluster = true,
                ClusterMaxZoom = 3,
                ClusterRadius = 4,
                LineMetrics = true,
                MaxZoom = 5,
                Tolerance = 6
            };

            var expectedJson = JsonSerializer.Serialize(options);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializeVectorTileSourceOptions()
        {
            var options = new VectorTileSourceOptions {
                Bounds = new Components.Atlas.BoundingBox(1, 2, 3, 4),
                IsTMS = true,
                MaxZoom = 5,
                MinZoom = 6,
                Tiles = new[] {
                    "tile"
                },
                TileSize = 7,
                Url = "url"
            };

            var expectedJson = JsonSerializer.Serialize(options);
            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_SerializeGriddedDataSourceOptions()
        {
            var options = new GriddedDataSourceOptions {
                CellWidth = 1
            };

            var expectedJson = JsonSerializer.Serialize(options, null);
            TestAndAssertWrite(options, expectedJson);
        }
    }
}
