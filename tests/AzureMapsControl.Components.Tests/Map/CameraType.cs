namespace AzureMapsControl.Components.Tests.Map
{
    using AzureMapsControl.Components.Map;

    using Xunit;

    public class CameraTypeTests
    {
        [Theory]
        [InlineData("ease")]
        [InlineData("fly")]
        [InlineData("jump")]
        public static void Should_ReturnCameraTypeFromString(string cameraType)
        {
            var camera = CameraType.FromString(cameraType);
            Assert.Equal(cameraType, camera.ToString());
        }

        [Fact]
        public static void Should_ReturnDefaultCameraType_IfStringDoesNotMatch()
        {
            var cameraType = "obviouslyNotAValidOne";
            var camera = CameraType.FromString(cameraType);
            Assert.Equal(default, camera);
        }
    }
}
