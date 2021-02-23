namespace AzureMapsControl.Components.Tests.Animations
{
    using System;

    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Markers;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class AnimationServiceTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();
        private readonly Mock<ILogger<AnimationService>> _loggerServiceMock = new();

        private readonly AnimationService _animationService;

        public AnimationServiceTests() => _animationService = new AnimationService(_jsRuntimeMock.Object, _loggerServiceMock.Object);

        [Fact]
        public async void Should_Snakeline_Async()
        {
            var line = new LineString();
            var source = new DataSource();
            var options = new SnakeLineAnimationOptions();

            var result = await _animationService.SnakelineAsync(line, source, options);
            Assert.NotNull(result.Id);
            Assert.Equal(options, result.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Snakeline.ToAnimationNamespace(), result.Id, line.Id, source.Id, options), Times.Once);
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
            Assert.NotNull(result.Id);
            Assert.Equal(options, result.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), result.Id, line.Id, lineSource.Id, pin.Id, pinSource.Id, options), Times.Once);
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
            Assert.NotNull(result.Id);
            Assert.Equal(options, result.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), result.Id, line.Id, lineSource.Id, pin.Id, null, options), Times.Once);
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
            Assert.NotNull(result.Id);
            Assert.Equal(options, result.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), result.Id, line, null, pin.Id, pinSource.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_MoveAlongPath_PositionsAndHtmlMarkers_Async()
        {
            var line = Array.Empty<Position>();
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new MoveAlongPathAnimationOptions();

            var result = await _animationService.MoveAlongPathAsync(line, pin, options);
            Assert.NotNull(result.Id);
            Assert.Equal(options, result.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), result.Id, line, null, pin.Id, null, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
