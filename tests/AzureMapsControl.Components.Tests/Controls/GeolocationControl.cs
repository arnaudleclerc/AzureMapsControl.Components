namespace AzureMapsControl.Components.Tests.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Controls;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Geolocation;
    using AzureMapsControl.Components.Runtime;
    using AzureMapsControl.Components.Tests.Json;

    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class GeolocationControlTests
    {
        private readonly Mock<IMapJsRuntime> _mapJsRuntimeMock = new Mock<IMapJsRuntime>();
        private readonly string _testMapId = "test-map-id";

        [Fact]
        public async Task Should_GetLastKnowPositionAsync()
        {
            var feature = new Feature<Point>();
            _mapJsRuntimeMock.Setup(runtime => runtime.InvokeAsync<Feature<Point>>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(feature);

            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            var result = await control.GetLastKnownPositionAsync();
            Assert.Equal(feature, result);

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeAsync<Feature<Point>>(Constants.JsConstants.Methods.GeolocationControl.GetLastKnownPosition.ToGeolocationControlNamespace(), _testMapId, control.Id.ToString()), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetLastKnowPosition_IfAlreadyDisposed_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.DisposeAsync();
            await Assert.ThrowsAnyAsync<ControlDisposedException>(async () => await control.GetLastKnownPositionAsync());

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.Dispose.ToGeolocationControlNamespace(), _testMapId, control.Id.ToString()), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetLastKnowPosition_NotAddedToMapCase_Async()
        {
            var control = new GeolocationControl();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.GetLastKnownPositionAsync());

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_DisposeAsync()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            var disposedCalled = false;
            control.OnDisposed += () => disposedCalled = true;

            await control.DisposeAsync();

            Assert.True(control.Disposed);
            Assert.True(disposedCalled);
            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.Dispose.ToGeolocationControlNamespace(), _testMapId, control.Id.ToString()), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotDispose_IfAlreadyDisposed_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.DisposeAsync();
            await Assert.ThrowsAnyAsync<ControlDisposedException>(async () => await control.DisposeAsync());

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.Dispose.ToGeolocationControlNamespace(), _testMapId, control.Id.ToString()), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotDispose_NotAddedToMapCase_Async()
        {
            var control = new GeolocationControl();
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.DisposeAsync());

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetOptions_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.SetOptionsAsync(options => options.CalculateMissingValues = true);

            Assert.True(control.Options.CalculateMissingValues);

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.SetOptions.ToGeolocationControlNamespace(), _testMapId, control.Id.ToString(), control.Options), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetOptions_DisposedCase_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.DisposeAsync();
            await Assert.ThrowsAnyAsync<ControlDisposedException>(async () => await control.SetOptionsAsync(options => options.CalculateMissingValues = true));

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.Dispose.ToGeolocationControlNamespace(), _testMapId, control.Id.ToString()), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetOptions_NotAddedToMapCase_Async()
        {
            var control = new GeolocationControl();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await control.SetOptionsAsync(options => options.CalculateMissingValues = true));

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_AddEvents_Async()
        {
            var control = new GeolocationControl(eventFlags: GeolocationEventActivationFlags.None().Enable(GeolocationEventType.GeolocationError)) {
                JsRuntime = _mapJsRuntimeMock.Object
            };
            // Set the MapId on the control as would happen when added to map
            control.GetType().GetProperty("MapId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, _testMapId);

            await control.AddEventsAsync();

            _mapJsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GeolocationControl.AddEvents.ToGeolocationControlNamespace(), It.Is<object[]>(parameters =>
               parameters[0] as string == _testMapId
       && parameters[1] as string == control.Id.ToString()
         && parameters[2] is IEnumerable<string>
       && (parameters[2] as IEnumerable<string>).Single() == GeolocationEventType.GeolocationError.ToString()
         && parameters[3] is DotNetObjectReference<GeolocationEventInvokeHelper>
)), Times.Once);
            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotAddEvents_NoEvents_Async()
        {
            var control = new GeolocationControl(eventFlags: GeolocationEventActivationFlags.None()) {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            await control.AddEventsAsync();

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotAddEvents_NullCase_Async()
        {
            var control = new GeolocationControl() {
                JsRuntime = _mapJsRuntimeMock.Object
            };

            await control.AddEventsAsync();

            _mapJsRuntimeMock.VerifyNoOtherCalls();
        }
    }

    public class GeolocationControlJsonConverterTests : JsonConverterTests<GeolocationControl>
    {
        public GeolocationControlJsonConverterTests() : base(new GeolocationControlJsonConverter()) { }

        [Fact]
        public void Should_Write()
        {
            var control = new GeolocationControl(new GeolocationControlOptions {
                CalculateMissingValues = true,
                MarkerColor = "markerColor",
                MaxZoom = 1,
                PositionOptions = new PositionOptions {
                    EnableHighAccuracy = false,
                    MaximumAge = 2,
                    Timeout = 3,
                },
                ShowUserLocation = true,
                TrackUserLocation = false,
                UpdateMapCamera = true,
                Style = ControlStyle.Auto
            }, ControlPosition.BottomLeft);

            var expectedJson = "{"
                + "\"id\":\"" + control.Id + "\""
                + ",\"type\":\"" + control.Type + "\""
                + ",\"position\":\"" + control.Position.ToString() + "\""
                + ",\"options\":{"
                + "\"calculateMissingValues\":" + control.Options.CalculateMissingValues.Value
                + ",\"markerColor\":\"" + control.Options.MarkerColor.ToString() + "\""
                + ",\"maxZoom\":" + control.Options.MaxZoom.Value
                + ",\"positionOptions\":{"
                + "\"enableHighAccuracy\":" + control.Options.PositionOptions.EnableHighAccuracy.Value
                + ",\"maximumAge\":" + control.Options.PositionOptions.MaximumAge.Value
                + ",\"timeout\":" + control.Options.PositionOptions.Timeout.Value
                + "}"
                + ",\"showUserLocation\":" + control.Options.ShowUserLocation.Value
                + ",\"style\":\"" + control.Options.Style.ToString() + "\""
                + ",\"trackUserLocation\":" + control.Options.TrackUserLocation.Value
                + ",\"updateMapCamera\":" + control.Options.UpdateMapCamera.Value
                + "}"
                + "}";

            TestAndAssertWrite(control, expectedJson);
        }
    }
}
