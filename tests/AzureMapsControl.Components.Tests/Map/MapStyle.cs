namespace AzureMapsControl.Components.Tests.Map
{
    using AzureMapsControl.Components.Map;

    using Xunit;

    public class MapStyleTests
    {
        [Theory]
        [InlineData("blank")]
        [InlineData("blank_accessible")]
        [InlineData("satellite")]
        [InlineData("satellite_road_labels")]
        [InlineData("grayscale_dark")]
        [InlineData("grayscale_light")]
        [InlineData("night")]
        [InlineData("road_shaded_relief")]
        [InlineData("high_contrast_dark")]
        [InlineData("road")]
        public static void Should_ReturnMapStyleFromString(string mapStyleType)
        {
            var mapStyle = MapStyle.FromString(mapStyleType);
            Assert.Equal(mapStyleType, mapStyle.ToString());
        }

        [Fact]
        public static void Should_ReturnDefaultMapStyle_IfStringDoesNotMatch()
        {
            var mapStyleType = "obviouslyNotAValidOne";
            var mapStyle = MapStyle.FromString(mapStyleType);
            Assert.Equal(default, mapStyle);
        }
    }
}
