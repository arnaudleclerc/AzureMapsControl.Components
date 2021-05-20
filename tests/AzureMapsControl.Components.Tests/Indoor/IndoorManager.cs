namespace AzureMapsControl.Components.Tests.Indoor
{
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Indoor;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class IndoorManagerTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new Mock<IMapJsRuntime>();
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();

        [Fact]
        public async void Should_InitializeAsync()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.InitializeAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Initialize.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotInitialize_DisposedCase_Async()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async() => await indoorManager.InitializeAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_DisposeAsync()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.DisposeAsync();
            Assert.True(indoorManager.Disposed);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotDisposeTwice_Async()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await indoorManager.DisposeAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
