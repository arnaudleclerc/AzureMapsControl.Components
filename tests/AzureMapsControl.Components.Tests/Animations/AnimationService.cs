namespace AzureMapsControl.Components.Tests.Animations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Map;
    using AzureMapsControl.Components.Markers;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;
    using Microsoft.JSInterop;

    using Moq;

    using Xunit;

    public class AnimationServiceTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();
        private readonly Mock<ILogger<AnimationService>> _loggerServiceMock = new();
        private readonly Mock<IMapService> _mapServiceMock = new();

        private readonly AnimationService _animationService;

        public AnimationServiceTests() => _animationService = new AnimationService(_jsRuntimeMock.Object, _loggerServiceMock.Object, _mapServiceMock.Object);

        [Fact]
        public async void Should_Snakeline_Async()
        {
            var line = new LineString();
            var source = new DataSource();
            var options = new SnakeLineAnimationOptions();

            var result = await _animationService.SnakelineAsync(line, source, options);
            Assert.IsType<UpdatableAnimation>(result);
            Assert.NotNull((result as UpdatableAnimation).Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Snakeline.ToAnimationNamespace(), (result as UpdatableAnimation).Id, line.Id, source.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_MoveAlongPath_LineStringAndPoint_Async()
        {
            var line = new LineString();
            var lineSource = new DataSource();
            var pin = new Point();
            var pinSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions();

            var result = await _animationService.MoveAlongPathAsync(line, lineSource, pin, pinSource, options);
            Assert.IsType<UpdatableAnimation>(result);
            Assert.NotNull((result as UpdatableAnimation).Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), (result as UpdatableAnimation).Id, line.Id, lineSource.Id, pin.Id, pinSource.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_MoveAlongPath_LineStringAndHtmlMarker_Async()
        {
            var line = new LineString();
            var lineSource = new DataSource();
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new MoveAlongPathAnimationOptions();

            var result = await _animationService.MoveAlongPathAsync(line, lineSource, pin, options);
            Assert.IsType<UpdatableAnimation>(result);
            Assert.NotNull((result as UpdatableAnimation).Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), (result as UpdatableAnimation).Id, line.Id, lineSource.Id, pin.Id, null, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_MoveAlongPath_PositionsAndPoint_Async()
        {
            var line = Array.Empty<Position>();
            var pin = new Point();
            var pinSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions();

            var result = await _animationService.MoveAlongPathAsync(line, pin, pinSource, options);
            Assert.IsType<UpdatableAnimation>(result);
            Assert.NotNull((result as UpdatableAnimation).Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), (result as UpdatableAnimation).Id, line, null, pin.Id, pinSource.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_MoveAlongPath_PositionsAndHtmlMarkers_Async()
        {
            var line = Array.Empty<Position>();
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new MoveAlongPathAnimationOptions();

            var result = await _animationService.MoveAlongPathAsync(line, pin, options);
            Assert.IsType<UpdatableAnimation>(result);
            Assert.NotNull((result as UpdatableAnimation).Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), (result as UpdatableAnimation).Id, line, null, pin.Id, null, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_FlowingDashedLine_Async()
        {
            var layer = new LineLayer();
            var options = new MovingDashLineOptions();

            var result = await _animationService.FlowingDashedLineAsync(layer, options);
            Assert.IsType<Animation>(result);
            Assert.NotNull((result as Animation).Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.FlowingDashedLine.ToAnimationNamespace(), (result as Animation).Id, layer.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_DropMarkers_Async()
        {
            var map = new Map("id", htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(null));
            _mapServiceMock.Setup(mapService => mapService.Map).Returns(map);
            var marker1 = new HtmlMarker(new HtmlMarkerOptions());
            var marker2 = new HtmlMarker(new HtmlMarkerOptions());
            var height = 1m;
            var options = new AnimationOptions();

            var result = await _animationService.DropMarkersAsync(new[] { marker1, marker2 }, height, options);
            Assert.IsType<UpdatableAnimation>(result);
            Assert.NotNull((result as Animation).Id);
            Assert.Contains(_mapServiceMock.Object.Map.HtmlMarkers, marker => marker.Id == marker1.Id && marker.Options == marker1.Options);
            Assert.Contains(_mapServiceMock.Object.Map.HtmlMarkers, marker => marker.Id == marker2.Id && marker.Options == marker2.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.DropMarkers.ToAnimationNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == (result as Animation).Id
                && parameters[1] is IEnumerable<HtmlMarkerCreationOptions>
                && (parameters[1] as IEnumerable<HtmlMarkerCreationOptions>).Any(marker => marker.Id == marker1.Id && marker.Options == marker1.Options)
                && (parameters[1] as IEnumerable<HtmlMarkerCreationOptions>).Any(marker => marker.Id == marker2.Id && marker.Options == marker2.Options)
                && parameters[2] as decimal? == height
                && parameters[3] is AnimationOptions
                && parameters[4] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_DropMarker_Async()
        {
            var map = new Map("id", htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(null));
            _mapServiceMock.Setup(mapService => mapService.Map).Returns(map);
            var marker1 = new HtmlMarker(new HtmlMarkerOptions());
            var height = 1m;
            var options = new AnimationOptions();

            var result = await _animationService.DropMarkerAsync(marker1, height, options);
            Assert.IsType<UpdatableAnimation>(result);
            Assert.NotNull((result as Animation).Id);

            Assert.Contains(_mapServiceMock.Object.Map.HtmlMarkers, marker => marker.Id == marker1.Id && marker.Options == marker1.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.DropMarkers.ToAnimationNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == (result as Animation).Id
                && parameters[1] is IEnumerable<HtmlMarkerCreationOptions>
                && (parameters[1] as IEnumerable<HtmlMarkerCreationOptions>).Any(marker => marker.Id == marker1.Id && marker.Options == marker1.Options)
                && parameters[2] as decimal? == height
                && parameters[3] is AnimationOptions
                && parameters[4] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
