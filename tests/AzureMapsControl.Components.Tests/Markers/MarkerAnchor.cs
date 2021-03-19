namespace AzureMapsControl.Components.Tests.Markers
{
    using AzureMapsControl.Components.Markers;

    using Xunit;

    public class MarkerAnchorTests
    {
        [Theory]
        [InlineData("bottom")]
        [InlineData("bottom-left")]
        [InlineData("bottom-right")]
        [InlineData("center")]
        [InlineData("left")]
        [InlineData("right")]
        [InlineData("top")]
        [InlineData("top-left")]
        [InlineData("top-right")]
        public static void Should_ReturnMarkerAnchorFromString(string markerAnchorType)
        {
            var markerAnchor = MarkerAnchor.FromString(markerAnchorType);
            Assert.Equal(markerAnchorType, markerAnchor.ToString());
        }

        [Fact]
        public static void Should_ReturnDefaultMarkerAnchor_IfStringDoesNotMatch()
        {
            var markerAnchorType = "obviouslyNotAValidOne";
            var markerAnchor = MarkerAnchor.FromString(markerAnchorType);
            Assert.Equal(default, markerAnchor);
        }
    }
}
