namespace AzureMapsControl.Components.Tests.Runtime
{
    using System.Threading.Tasks;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class MapJsRuntimeTests
    {
        private readonly Mock<IJSRuntime> _jsRuntimeMock = new Mock<IJSRuntime>();
        private readonly Mock<ILogger<MapJsRuntime>> _loggerMock = new Mock<ILogger<MapJsRuntime>>();

        private readonly MapJsRuntime _sut;

        public MapJsRuntimeTests() => _sut = new(_jsRuntimeMock.Object, _loggerMock.Object);

        [Fact]
        public async Task Should_InvokeVoidAsync()
        {
            var identifier = "identifier";
            var args = new object[] { 1, 2 };

            await _sut.InvokeVoidAsync(identifier, args);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>(identifier, args), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_InvokeAsync()
        {
            var identifier = "identifier";
            var args = new object[] { 1, 2 };
            var expected = "expected";
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<string>(identifier, args)).ReturnsAsync(expected).Verifiable();

            var result = await _sut.InvokeAsync<string>(identifier, args);

            Assert.Equal(expected, result);
            _jsRuntimeMock.Verify();
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
