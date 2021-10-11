namespace AzureMapsControl.Components.Tests.Data.Grid
{

    using AzureMapsControl.Components.Data.Grid;

    using Xunit;

    public class GridTypeTests
    {
        [Theory]
        [InlineData("circle")]
        [InlineData("hexagon")]
        [InlineData("hexCircle")]
        [InlineData("pointyHexagon")]
        [InlineData("square")]
        [InlineData("triangle")]
        public void Should_ReturnGridTypeFromString(string gridType)
        {
            var result = GridType.FromString(gridType);
            Assert.Equal(result.ToString(), gridType.ToString());
        }

        [Fact]
        public void Should_ReturnDefaultGridType_IfStringDoesNotMatch()
        {
            var gridType = "obviouslyNotAValidOne";
            var result = Components.Data.Grid.GridType.FromString(gridType);
            Assert.Equal(default, result);
        }
    }
}
