namespace AzureMapsControl.Components.Tests.Indoor
{
    using System.Collections.Generic;
    using System.Linq;

    using AzureMapsControl.Components.Indoor;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class IndoorServiceTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new();
        private readonly Mock<ILogger<IndoorService>> _logger = new();

        [Fact]
        public async void Should_CreateIndoorManager_Async()
        {
            var options = new IndoorManagerOptions();
            var service = new IndoorService(_jsRuntime.Object, _logger.Object);
            Assert.NotNull(await service.CreateIndoorManagerAsync(options));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.IsAny<string>(), options, null, It.IsAny<DotNetObjectReference<IndoorManagerEventHelper>>()), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_CreateIndoorManagerWithEvents_Async()
        {
            var options = new IndoorManagerOptions();
            var service = new IndoorService(_jsRuntime.Object, _logger.Object);
            Assert.NotNull(await service.CreateIndoorManagerAsync(options, IndoorManagerEventActivationFlags.None().Enable(IndoorManagerEventType.FacilityChanged)));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.IsAny<string>(), options, It.Is<IEnumerable<string>>(events => events.Single() == "facilitychanged"), It.IsAny<DotNetObjectReference<IndoorManagerEventHelper>>()), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_CreateIndoorManagerWithEvents_NullEventsCase_Async()
        {
            var options = new IndoorManagerOptions();
            var service = new IndoorService(_jsRuntime.Object, _logger.Object);
            Assert.NotNull(await service.CreateIndoorManagerAsync(options, null));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.IsAny<string>(), options, null, It.IsAny<DotNetObjectReference<IndoorManagerEventHelper>>()), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
