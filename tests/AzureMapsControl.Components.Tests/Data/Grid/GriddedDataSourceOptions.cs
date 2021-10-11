namespace AzureMapsControl.Components.Tests.Data.Grid
{
    using AzureMapsControl.Components.Data.Grid;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;

    public class GriddedDataSourceOptionsJsonConverterTests : JsonConverterTests<GriddedDataSourceOptions>
    {
        public GriddedDataSourceOptionsJsonConverterTests() : base(new GriddedDataSourceOptionsJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var options = new GriddedDataSourceOptions {
                CellWidth = 1,
                CenterLatitude = 2,
                Coverage = 3,
                DistanceUnits = Components.Atlas.Math.DistanceUnits.Meters,
                GridType = GridType.Circle,
                MaxZoom = 4,
                MinCellWidth = 5,
                ScaleExpression = new Components.Atlas.Expression(System.Text.Json.JsonDocument.Parse("[\"==\", [\"get\", \"cell_id\"], \"\"]")),
                ScaleProperty = "scaleProperty"
            };

            var expectedJson = "{"
                + "\"cellWidth\":1"
                + ",\"centerLatitude\":2"
                + ",\"coverage\":3"
                + ",\"distanceUnits\":\"meters\""
                + ",\"gridType\":\"circle\""
                + ",\"maxZoom\":4"
                + ",\"minCellWidth\":5"
                + ",\"scaleExpression\":[\"==\",[\"get\",\"cell_id\"],\"\"]"
                + ",\"scaleProperty\":\"scaleProperty\""
                + "}";

            TestAndAssertWrite(options, expectedJson);
        }

        [Fact]
        public void Should_WriteNull() => TestAndAssertWrite(null, "null");

        [Fact]
        public void Should_Read()
        {
            var json = "{"
                + "\"cellWidth\":1"
                + ",\"centerLatitude\":2"
                + ",\"coverage\":3"
                + ",\"distanceUnits\":\"meters\""
                + ",\"gridType\":\"circle\""
                + ",\"maxZoom\":4"
                + ",\"minCellWidth\":5"
                + ",\"scaleExpression\":[\"==\", [\"get\", \"cell_id\"], \"\"]"
                + ",\"scaleProperty\":\"scaleProperty\""
                + "}";

            var options = Read(json);

            Assert.Equal(1, options.CellWidth);
            Assert.Equal(2, options.CenterLatitude);
            Assert.Equal(3, options.Coverage);
            Assert.Equal(Components.Atlas.Math.DistanceUnits.Meters, options.DistanceUnits);
            Assert.Equal(GridType.Circle, options.GridType);
            Assert.Equal(4, options.MaxZoom);
            Assert.Equal(5, options.MinCellWidth);
            Assert.Null(options.ScaleExpression);
            Assert.Equal("scaleProperty", options.ScaleProperty);
        }
    }
}
