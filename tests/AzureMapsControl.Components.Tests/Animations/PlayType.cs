namespace AzureMapsControl.Components.Tests.Animations
{
    using AzureMapsControl.Components.Animations.Options;

    using Xunit;

    public class PlayTypeTests
    {
        [Theory]
        [InlineData("together")]
        [InlineData("interval")]
        [InlineData("sequential")]
        public static void Should_ReturnPlayTypeFromString(string type)
        {
            var playType = PlayType.FromString(type);
            Assert.Equal(type, playType.ToString());
        }

        [Fact]
        public static void Should_ReturnDefaultPlayType_IfStringDoesNotMatch()
        {
            var type = "obviouslyNotAValidOne";
            var playType = PlayType.FromString(type);
            Assert.Equal(default, playType);
        }
    }
}
