namespace AzureMapsControl.Components.Tests.Indoor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Indoor;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class IndoorServiceTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new();
        private readonly Mock<ILogger<IndoorService>> _logger = new();
        private readonly Mock<IMapService> _mapService = new();
        private const string TestMapId = "test-map-id";

        [Fact]
        public async Task Should_CreateIndoorManager_WithMapId_Async()
        {
            var options = new IndoorManagerOptions();

            var service = new IndoorService(_jsRuntime.Object, _logger.Object, _mapService.Object);
            var indoorManager = await service.CreateIndoorManagerAsync(TestMapId, options);

            Assert.NotNull(indoorManager);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as string) == TestMapId
                    && (parameters[1] as string) == indoorManager.Id
                && object.Equals(parameters[2], options)
                        && parameters[3] == null
                && parameters[4] is DotNetObjectReference<IndoorManagerEventHelper>
            )), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_CreateIndoorManagerWithEvents_WithMapId_Async()
        {
            var options = new IndoorManagerOptions();
            var eventFlags = IndoorManagerEventActivationFlags.None().Enable(IndoorManagerEventType.FacilityChanged);

            var service = new IndoorService(_jsRuntime.Object, _logger.Object, _mapService.Object);
            var indoorManager = await service.CreateIndoorManagerAsync(TestMapId, options, eventFlags);

            Assert.NotNull(indoorManager);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as string) == TestMapId
                && (parameters[1] as string) == indoorManager.Id
                && object.Equals(parameters[2], options)
                && parameters[3] is IEnumerable<string>
                && (parameters[3] as IEnumerable<string>).Single() == "facilitychanged"
                && parameters[4] is DotNetObjectReference<IndoorManagerEventHelper>
            )), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_CreateIndoorManagerWithEvents_NullEventsCase_WithMapId_Async()
        {
            var options = new IndoorManagerOptions();

            var service = new IndoorService(_jsRuntime.Object, _logger.Object, _mapService.Object);
            var indoorManager = await service.CreateIndoorManagerAsync(TestMapId, options, null);

            Assert.NotNull(indoorManager);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.Is<object[]>(parameters =>
                (parameters[0] as string) == TestMapId
                && (parameters[1] as string) == indoorManager.Id
                && object.Equals(parameters[2], options)
                && parameters[3] == null
                && parameters[4] is DotNetObjectReference<IndoorManagerEventHelper>
            )), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        // Legacy method tests (for backward compatibility)
        [Fact]
        public async Task Should_CreateIndoorManager_Async()
        {
            var options = new IndoorManagerOptions();
            // Create a real Map instance instead of mocking it since Map is sealed
            var map = new AzureMapsControl.Components.Map.Map("test-map");
            _mapService.Setup(x => x.Map).Returns(map);

            var service = new IndoorService(_jsRuntime.Object, _logger.Object, _mapService.Object);
            Assert.NotNull(await service.CreateIndoorManagerAsync(options));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.IsAny<string>(), It.IsAny<string>(), options, null, It.IsAny<DotNetObjectReference<IndoorManagerEventHelper>>()), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_CreateIndoorManagerWithEvents_Async()
        {
            var options = new IndoorManagerOptions();
            // Create a real Map instance instead of mocking it since Map is sealed
            var map = new AzureMapsControl.Components.Map.Map("test-map");
            _mapService.Setup(x => x.Map).Returns(map);

            var service = new IndoorService(_jsRuntime.Object, _logger.Object, _mapService.Object);
            Assert.NotNull(await service.CreateIndoorManagerAsync(options, IndoorManagerEventActivationFlags.None().Enable(IndoorManagerEventType.FacilityChanged)));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.IsAny<string>(), It.IsAny<string>(), options, It.Is<IEnumerable<string>>(events => events.Single() == "facilitychanged"), It.IsAny<DotNetObjectReference<IndoorManagerEventHelper>>()), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_CreateIndoorManagerWithEvents_NullEventsCase_Async()
        {
            var options = new IndoorManagerOptions();
            // Create a real Map instance instead of mocking it since Map is sealed
            var map = new AzureMapsControl.Components.Map.Map("test-map");
            _mapService.Setup(x => x.Map).Returns(map);

            var service = new IndoorService(_jsRuntime.Object, _logger.Object, _mapService.Object);
            Assert.NotNull(await service.CreateIndoorManagerAsync(options, null));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.CreateIndoorManager.ToIndoorNamespace(), It.IsAny<string>(), It.IsAny<string>(), options, null, It.IsAny<DotNetObjectReference<IndoorManagerEventHelper>>()), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
