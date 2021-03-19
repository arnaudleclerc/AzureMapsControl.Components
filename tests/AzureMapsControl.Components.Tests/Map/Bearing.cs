namespace AzureMapsControl.Components.Tests.Map
{
    using AzureMapsControl.Components.Map;

    using Xunit;

    public class BearingTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(90)]
        [InlineData(180)]
        [InlineData(270)]
        public static void Should_ReturnBearingFromDegrees(int degrees)
        {
            var bearing = Bearing.FromDegrees(degrees);
            Assert.Equal(degrees, bearing.Degrees);
        }

        [Fact]
        public static void Should_ReturnDefaultBearing_IfDegreesDoesNotMatch()
        {
            var degrees = 12;
            var bearing = Bearing.FromDegrees(degrees);
            Assert.Equal(default, bearing);
        }
    }
}
