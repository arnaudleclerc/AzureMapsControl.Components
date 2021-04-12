namespace AzureMapsControl.Components.Tests.Controls
{
    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class GeolocationControlTests
    {
        private readonly Mock<IMapJsRuntime> _mapJsRuntimeMock = new Mock<IMapJsRuntime>();

        [Fact]
        public async void Should_GetLastKnowPositionAsync()
        {
            var feature = new Feature<Point>();
            _mapJsRuntimeMock.Setup(runtime => runtime.InvokeAsync<Feature<Point>>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(feature);

            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            var result = await control.GetLastKnownPositionAsync();
            Assert.Equal(feature, result);

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeAsync<Feature<Point>>(Constants.JsConstants.Methods.GeolocationControl.GetLastKnownPosition.ToGeolocationControlNamespace(), control.Id), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
