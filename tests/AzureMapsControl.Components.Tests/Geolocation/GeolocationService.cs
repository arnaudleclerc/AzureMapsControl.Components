namespace AzureMapsControl.Components.Tests.Geolocation
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Geolocation;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class GeolocationServiceTests
    {
        private readonly Mock<ILogger<GeolocationService>> _loggerMock = new Mock<ILogger<GeolocationService>>();
        private readonly Mock<IMapJsRuntime> _mapJsRuntimeMock = new Mock<IMapJsRuntime>();

        [Fact]
        public async Task Should_CheckIfGeolocationIsSupportedAsync()
        {
            var isGeolocationSupported = true;
            _mapJsRuntimeMock.Setup(runtime => runtime.InvokeAsync<bool>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(isGeolocationSupported);
            var service = new GeolocationService(_mapJsRuntimeMock.Object, _loggerMock.Object);

            var result = await service.IsGeolocationSupportedAsync();
            Assert.Equal(isGeolocationSupported, result);

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeAsync<bool>(Constants.JsConstants.Methods.GeolocationControl.IsGeolocationSupported.ToGeolocationControlNamespace(), It.Is<object[]>(parameters => parameters.Length == 0)), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
