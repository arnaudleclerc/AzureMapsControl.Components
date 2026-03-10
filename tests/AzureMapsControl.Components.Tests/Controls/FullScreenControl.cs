namespace AzureMapsControl.Components.Tests.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.FullScreen;
    using AzureMapsControl.Components.Runtime;
    using AzureMapsControl.Components.Tests.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class FullScreenControlJsonConverterTests : JsonConverterTests<FullScreenControl>
    {
        public FullScreenControlJsonConverterTests() : base(new FullScreenControlJsonConverter()) { }

        [Fact]
        public void Should_Write_WithControlStyle()
        {
            var control = new FullScreenControl(new FullScreenControlOptions {
                Container = "container",
                HideIfUnsupported = true,
                Style = new Components.Atlas.Either<ControlStyle, string>(ControlStyle.Auto)
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"container\":\"" + control.Options.Container + "\""
                + ",\"hideIfUnsupported\":" + control.Options.HideIfUnsupported
                + ",\"style\":\"" + control.Options.Style.FirstChoice.ToString() + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

        [Fact]
        public void Should_Write_WithCssStyle()
        {
            var control = new FullScreenControl(new FullScreenControlOptions {
                Container = "container",
                HideIfUnsupported = true,
                Style = new Components.Atlas.Either<ControlStyle, string>("#000000")
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"container\":\"" + control.Options.Container + "\""
                + ",\"hideIfUnsupported\":" + control.Options.HideIfUnsupported
                + ",\"style\":\"" + control.Options.Style.SecondChoice + "\""
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }

    }

    public class FullScreenControlTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();
        private readonly Mock<ILogger> _loggerMock = new();
        private readonly string _testMapId = "test-map-id";

        [Fact]
        public async Task Should_DisposeAsync()
        {
            var control = new FullScreenControl {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            var eventTriggered = false;
            control.OnDisposed += () => eventTriggered = true;

            await control.DisposeAsync();
            Assert.True(control.Disposed);
            Assert.True(eventTriggered);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.Dispose.ToFullScreenControlNamespace(), _testMapId, control.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotDispose_NotAddedToMapCase_Async()
        {
            var control = new FullScreenControl {
                Logger = _loggerMock.Object
            };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.DisposeAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotDisposeTwice_Async()
        {
            var control = new FullScreenControl {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            var eventTriggered = false;
            control.OnDisposed += () => eventTriggered = true;

            await control.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await control.DisposeAsync());

            Assert.True(control.Disposed);
            Assert.True(eventTriggered);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.Dispose.ToFullScreenControlNamespace(), _testMapId, control.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetOptionsAsync()
        {
            var control = new FullScreenControl(new FullScreenControlOptions()) {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.SetOptionsAsync(options => options.Container = "container");

            Assert.Equal("container", control.Options.Container);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.SetOptions.ToFullScreenControlNamespace(), _testMapId, control.Id, control.Options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetOptions_NoOptionsCase_Async()
        {
            var control = new FullScreenControl {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.SetOptionsAsync(options => options.Container = "container");

            Assert.Equal("container", control.Options.Container);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.SetOptions.ToFullScreenControlNamespace(), _testMapId, control.Id, control.Options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetOptions_NotAddedToMapCase_Async()
        {
            var control = new FullScreenControl(new FullScreenControlOptions()) {
                Logger = _loggerMock.Object
            };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.SetOptionsAsync(options => options.Container = "container"));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetOptions_DisposedCase_Async()
        {
            var control = new FullScreenControl(new FullScreenControlOptions()) {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await control.SetOptionsAsync(options => options.Container = "container"));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.Dispose.ToFullScreenControlNamespace(), _testMapId, control.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetIsFullScreenAsync()
        {
            var isFullScreen = true;
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<bool>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(isFullScreen);
            var control = new FullScreenControl() {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            var result = await control.IsFullScreenAsync();
            Assert.Equal(isFullScreen, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<bool>(Constants.JsConstants.Methods.FullScreenControl.IsFullScreen.ToFullScreenControlNamespace(), _testMapId, control.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetIsFullScreen_NotAddedToMapCase_Async()
        {
            var control = new FullScreenControl() {
                Logger = _loggerMock.Object
            };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.IsFullScreenAsync());
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetIsFullScreen_DisposedCase_Async()
        {
            var control = new FullScreenControl() {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await control.IsFullScreenAsync());

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.Dispose.ToFullScreenControlNamespace(), _testMapId, control.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_AddEvents_Async()
        {
            var control = new FullScreenControl(eventFlags: FullScreenEventActivationFlags.All()) {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.AddEventsAsync();

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.FullScreenControl.AddEvents.ToFullScreenControlNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == _testMapId
                && (parameters[1] as Guid?).GetValueOrDefault().ToString() == control.Id.ToString()
                && parameters[2] is IEnumerable<string>
                && (parameters[2] as IEnumerable<string>).Single() == FullScreenEventType.FullScreenChanged.ToString()
       && parameters[3] is DotNetObjectReference<FullScreenEventInvokeHelper>
    )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotAddEvents_NullCase_Async()
        {
            var control = new FullScreenControl() {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };

            await control.AddEventsAsync();
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotAddEvents_EmptyCase_Async()
        {
            var control = new FullScreenControl(eventFlags: FullScreenEventActivationFlags.None()) {
                JsRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object
            };

            await control.AddEventsAsync();
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
