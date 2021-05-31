namespace AzureMapsControl.Components.Tests.Indoor
{

    using AzureMapsControl.Components.Indoor;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class IndoorServiceTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new Mock<IMapJsRuntime>();
        private readonly Mock<ILogger<IndoorService>> _logger = new Mock<ILogger<IndoorService>>();

        [Fact]
        public async void Should_CreateIndoorManager_Async()
        {
            var options = new IndoorManagerOptions();
            var service = new IndoorService(_jsRuntime.Object, _logger.Object);
            Assert.NotNull(await service.CreateIndoorManagerAsync(options));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.IsAny<string>(), options), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
