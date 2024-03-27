namespace AzureMapsControl.Components.Tests.Indoor
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Indoor;
    using AzureMapsControl.Components.Indoor.Style;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class IndoorManagerTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntime = new Mock<IMapJsRuntime>();
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();

        [Fact]
        public async Task Should_InitializeAsync()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.InitializeAsync();

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Initialize.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotInitialize_DisposedCase_Async()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await indoorManager.InitializeAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_DisposeAsync()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.DisposeAsync();
            Assert.True(indoorManager.Disposed);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotDisposeTwice_Async()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await indoorManager.DisposeAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetCurrentFacilityAsync()
        {
            var currentFacility = new IndoorFacility {
                FacilityId = "facilityId",
                LevelOrdinal = 1
            };
            _jsRuntime.Setup(runtime => runtime.InvokeAsync<IndoorFacility>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(currentFacility);

            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);
            var result = await indoorManager.GetCurrentFacilityAsync();

            Assert.Equal(currentFacility.FacilityId, result.FacilityId);
            Assert.Equal(currentFacility.LevelOrdinal, result.LevelOrdinal);

            _jsRuntime.Verify(runtime => runtime.InvokeAsync<IndoorFacility>(Constants.JsConstants.Methods.Indoor.GetCurrentFacility.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetCurrentFacility_DisposedCase_Async()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await indoorManager.GetCurrentFacilityAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetOptionsAsync()
        {
            var options = new IndoorManagerOptions();
            _jsRuntime.Setup(runtime => runtime.InvokeAsync<IndoorManagerOptions>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(options);

            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);
            var result = await indoorManager.GetOptionsAsync();
            Assert.Equal(options, result);

            _jsRuntime.Verify(runtime => runtime.InvokeAsync<IndoorManagerOptions>(Constants.JsConstants.Methods.Indoor.GetOptions.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetOptions_DisposedCase_Async()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await indoorManager.GetOptionsAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetStyleDefinition_Async()
        {
            var styleDefinition = new StyleDefinition();
            _jsRuntime.Setup(runtime => runtime.InvokeAsync<StyleDefinition>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(styleDefinition);

            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);
            var result = await indoorManager.GetStyleDefinitionAsync();

            Assert.Equal(styleDefinition, result);

            _jsRuntime.Verify(runtime => runtime.InvokeAsync<StyleDefinition>(Constants.JsConstants.Methods.Indoor.GetStyleDefinition.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetStyleDefinition_DisposedCase_Async()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await indoorManager.GetStyleDefinitionAsync());

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetDynamicStylingAsync()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);
            await indoorManager.SetDynamicStylingAsync(true);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.SetDynamicStyling.ToIndoorNamespace(), indoorManager.Id, true), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetDynamicStyling_DisposedCase_Async()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);
            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await indoorManager.SetDynamicStylingAsync(true));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetFacilityAsync()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);
            await indoorManager.SetFacilityAsync("facilityId", 1);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.SetFacility.ToIndoorNamespace(), indoorManager.Id, "facilityId", 1), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetFacility_DisposedCase_Async()
        {
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);
            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await indoorManager.SetFacilityAsync("facilityId", 1));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetOptions_Async()
        {
            var options = new IndoorManagerOptions();
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);

            await indoorManager.SetOptionsAsync(options);

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.SetOptions.ToIndoorNamespace(), indoorManager.Id, options), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetOptions_DisposedCase_Async()
        {
            var options = new IndoorManagerOptions();
            var indoorManager = new IndoorManager(_jsRuntime.Object, _logger.Object);
            await indoorManager.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await indoorManager.SetOptionsAsync(options));

            _jsRuntime.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Indoor.Dispose.ToIndoorNamespace(), indoorManager.Id), Times.Once);
            _jsRuntime.VerifyNoOtherCalls();
        }
    }
}
