namespace AzureMapsControl.Components.Tests.Atlas.Math
{
    using AzureMapsControl.Components.Atlas.Math;

    using Xunit;

    public class DistanceUnitsTests
    {
        [Theory]
        [InlineData("meters")]
        [InlineData("kilometers")]
        [InlineData("feet")]
        [InlineData("miles")]
        [InlineData("nauticalMiles")]
        [InlineData("yards")]
        public void Should_ReturnDistanceUnitFromString(string gridType)
        {
            var result = DistanceUnits.FromString(gridType);
            Assert.Equal(result.ToString(), gridType);
        }

        [Fact]
        public void Should_ReturnDefaultDistanceUnits_IfStringDoesNotMatch()
        {
            var unit = "obviouslyNotAValidOne";
            var result = DistanceUnits.FromString(unit);
            Assert.Equal(default, result);
        }
    }
}
