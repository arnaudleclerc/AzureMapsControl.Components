namespace AzureMapsControl.Components.Tests.FullScreen
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.FullScreen;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class FullScreenServiceTests
    {
        private readonly Mock<ILogger<FullScreenService>> _loggerMock = new();
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();

        [Fact]
        public async Task Should_CheckIsSupported_Async()
        {
            var isSupported = true;
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<bool>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(isSupported);

            var service = new FullScreenService(_jsRuntimeMock.Object, _loggerMock.Object);

            var result = await service.IsSupportedAsync();
            Assert.Equal(isSupported, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<bool>(Constants.JsConstants.Methods.FullScreenControl.IsFullScreenSupported.ToFullScreenControlNamespace(), It.Is<object[]>(parameters => parameters.Length == 0)), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
