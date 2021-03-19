namespace AzureMapsControl.Components.Tests.Animations.Options
{
    using AzureMapsControl.Components.Animations.Options;

    using Xunit;

    public class InterpolationTests
    {
        [Theory]
        [InlineData("linear")]
        [InlineData("nearest")]
        [InlineData("min")]
        [InlineData("max")]
        [InlineData("avg")]
        public static void Should_ReturnInterpolationFromString(string interpolationType)
        {
            var interpolation = Interpolation.FromString(interpolationType);
            Assert.Equal(interpolationType, interpolation.ToString());
        }

        [Fact]
        public static void Should_ReturnDefaultInterpolation_IfStringDoesNotMatch()
        {
            var interpolationType = "obviouslyNotAValidOne";
            var interpolation = Interpolation.FromString(interpolationType);
            Assert.Equal(default, interpolation);
        }
    }
}
