namespace AzureMapsControl.Components.Tests.Animations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Animations;
    using AzureMapsControl.Components.Animations.Options;
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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_Snakeline_Async(bool disposeOnComplete)
        {
            var line = new LineString();
            var source = new DataSource();
            var options = new SnakeLineAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };

            var result = await _animationService.SnakelineAsync(line, source, options);
            Assert.IsType<SnakeLineAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Snakeline.ToAnimationNamespace(), result.Id, line.Id, source.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_Snakeline_LineCaseAsync()
        {
            var source = new DataSource();
            var options = new SnakeLineAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.SnakelineAsync(null, source, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_Snakeline_SourceCaseAsync()
        {
            var line = new LineString();
            var source = new DataSource();
            var options = new SnakeLineAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.SnakelineAsync(line, null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_MoveAlongPath_LineStringAndPoint_DisposeOnComplete_Async(bool disposeOnComplete)
        {
            var line = new LineString();
            var lineSource = new DataSource();
            var pin = new Point();
            var pinSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };

            var result = await _animationService.MoveAlongPathAsync(line, lineSource, pin, pinSource, options);
            Assert.IsType<MoveAlongPathAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), result.Id, line.Id, lineSource.Id, pin.Id, pinSource.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_PathCaseAsync()
        {
            var lineSource = new DataSource();
            var pin = new Point();
            var pinSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(null, lineSource, pin, pinSource, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_PathSourceCaseAsync()
        {
            var line = new LineString();
            var pin = new Point();
            var pinSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(line, null, pin, pinSource, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_PinCaseAsync()
        {
            var line = new LineString();
            var lineSource = new DataSource();
            var pinSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(line, lineSource, null, pinSource, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_PinSourceCaseAsync()
        {
            var line = new LineString();
            var lineSource = new DataSource();
            var pin = new Point();
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(line, lineSource, pin, null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_MoveAlongPath_LineStringAndHtmlMarker_Async(bool disposeOnComplete)
        {
            var line = new LineString();
            var lineSource = new DataSource();
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new MoveAlongPathAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };

            var result = await _animationService.MoveAlongPathAsync(line, lineSource, pin, options);
            Assert.IsType<MoveAlongPathAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), result.Id, line.Id, lineSource.Id, pin.Id, null, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_LineStringAndHtmlMarker_PathCaseAsync()
        {
            var lineSource = new DataSource();
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(null, lineSource, pin, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_LineStringAndHtmlMarker_PathSourceCaseAsync()
        {
            var line = new LineString();
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(line, null, pin, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_LineStringAndHtmlMarker_PinCaseAsync()
        {
            var line = new LineString();
            var lineSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(line, lineSource, null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_MoveAlongPath_PositionsAndPoint_Async(bool disposeOnComplete)
        {
            var line = new Position[] { new Position() };
            var pin = new Point();
            var pinSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };

            var result = await _animationService.MoveAlongPathAsync(line, pin, pinSource, options);
            Assert.IsType<MoveAlongPathAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), result.Id, line, null, pin.Id, pinSource.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_PositionsAndPoint_NullLineCaseAsync()
        {
            var pin = new Point();
            var pinSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(null, pin, pinSource, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_PositionsAndPoint_PinCaseAsync()
        {
            var line = new Position[] { new Position() };
            var pinSource = new DataSource();
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(line, null, pinSource, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_PositionsAndPoint_PinSourceAsync()
        {
            var line = new Position[] { new Position() };
            var pin = new Point();
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(line, pin, null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_MoveAlongPath_PositionsAndHtmlMarkers_Async(bool disposeOnComplete)
        {
            var line = new Position[] { new Position() };
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new MoveAlongPathAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };

            var result = await _animationService.MoveAlongPathAsync(line, pin, options);
            Assert.IsType<MoveAlongPathAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongPath.ToAnimationNamespace(), result.Id, line, null, pin.Id, null, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_PositionsAndHtmlMarkers_NullLineCaseAsync()
        {
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(null, pin, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowArgumentNullException_MoveAlongPath_PositionsAndHtmlMarkers_PinCaseAsync()
        {
            var line = new Position[] { new Position() };
            var options = new MoveAlongPathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongPathAsync(line, null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_FlowingDashedLine_Async()
        {
            var layer = new LineLayer();
            var options = new MovingDashLineOptions();

            var result = await _animationService.FlowingDashedLineAsync(layer, options);
            Assert.IsType<FlowingDashedLineAnimation>(result);
            Assert.NotNull(result.Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.FlowingDashedLine.ToAnimationNamespace(), result.Id, layer.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_FlowingDashedLine_ThrowArgumentNullExceptionAsync()
        {
            var options = new MovingDashLineOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.FlowingDashedLineAsync(null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_DropMarkers_Async(bool disposeOnComplete)
        {
            var map = new Map("id", htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(null));
            _mapServiceMock.Setup(mapService => mapService.Map).Returns(map);
            var marker1 = new HtmlMarker(new HtmlMarkerOptions());
            var marker2 = new HtmlMarker(new HtmlMarkerOptions());
            var height = 1m;
            var options = new DropMarkersAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };

            var result = await _animationService.DropMarkersAsync(new[] { marker1, marker2 }, height, options);
            Assert.IsType<DropMarkersAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);
            Assert.Contains(_mapServiceMock.Object.Map.HtmlMarkers, marker => marker.Id == marker1.Id && marker.Options == marker1.Options);
            Assert.Contains(_mapServiceMock.Object.Map.HtmlMarkers, marker => marker.Id == marker2.Id && marker.Options == marker2.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.DropMarkers.ToAnimationNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == result.Id
                && parameters[1] is IEnumerable<HtmlMarkerCreationOptions>
                && (parameters[1] as IEnumerable<HtmlMarkerCreationOptions>).Any(marker => marker.Id == marker1.Id && marker.Options == marker1.Options)
                && (parameters[1] as IEnumerable<HtmlMarkerCreationOptions>).Any(marker => marker.Id == marker2.Id && marker.Options == marker2.Options)
                && parameters[2] as decimal? == height
                && parameters[3] is DropMarkersAnimationOptions
                && parameters[4] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_DropMarkers_ThrowArgumentNullExceptionAsync()
        {
            var height = 1m;
            var options = new DropMarkersAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.DropMarkersAsync(null, height, options));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_DropMarker_Async(bool disposeOnComplete)
        {
            var map = new Map("id", htmlMarkerInvokeHelper: new HtmlMarkerInvokeHelper(null));
            _mapServiceMock.Setup(mapService => mapService.Map).Returns(map);
            var marker1 = new HtmlMarker(new HtmlMarkerOptions());
            var height = 1m;
            var options = new DropMarkersAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };

            var result = await _animationService.DropMarkerAsync(marker1, height, options);
            Assert.IsType<DropMarkersAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            Assert.Contains(_mapServiceMock.Object.Map.HtmlMarkers, marker => marker.Id == marker1.Id && marker.Options == marker1.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.DropMarkers.ToAnimationNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == result.Id
                && parameters[1] is IEnumerable<HtmlMarkerCreationOptions>
                && (parameters[1] as IEnumerable<HtmlMarkerCreationOptions>).Any(marker => marker.Id == marker1.Id && marker.Options == marker1.Options)
                && parameters[2] as decimal? == height
                && parameters[3] is DropMarkersAnimationOptions
                && parameters[4] is DotNetObjectReference<HtmlMarkerInvokeHelper>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_DropMarker_ThrowArgumentNullExceptionAsync()
        {
            var height = 1m;
            var options = new DropMarkersAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.DropMarkerAsync(null, height, options));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GroupAnimations_Async()
        {
            var animation1 = new SnakeLineAnimation("id", _jsRuntimeMock.Object);
            var animation2 = new DropMarkersAnimation("markerAnimation", _jsRuntimeMock.Object);
            var options = new GroupAnimationOptions();

            var result = await _animationService.GroupAnimationAsync(new Animation[] { animation1, animation2 }, options);
            Assert.IsType<GroupAnimation>(result);
            Assert.NotNull((result as GroupAnimation).Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.GroupAnimations.ToAnimationNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == result.Id
                && parameters[1] is IEnumerable<string>
                && (parameters[1] as IEnumerable<string>).Contains(animation1.Id)
                && (parameters[1] as IEnumerable<string>).Contains(animation2.Id)
                && parameters[2] is GroupAnimationOptions
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GroupAnimations_ThrowArgumentNullExceptionAsync()
        {
            var options = new GroupAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.GroupAnimationAsync(null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_DropMultiple_Async(bool disposeOnComplete)
        {
            var point = new Point();
            var point2 = new Point();
            var points = new[] { point, point2 };
            var datasource = new DataSource();
            var options = new DropAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };
            var height = 1m;

            var result = await _animationService.DropAsync(points, datasource, height, options);
            Assert.IsType<DropAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Drop.ToAnimationNamespace(), result.Id, points, datasource.Id, height, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_DropMultiple_ThrowArgumentNullException_PointsCaseAsync()
        {
            IEnumerable<Point> points = null;
            var datasource = new DataSource();
            var options = new DropAnimationOptions();
            var height = 1m;

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.DropAsync(points, datasource, height, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_DropMultiple_ThrowArgumentNullException_SourceCaseAsync()
        {
            var point = new Point();
            var point2 = new Point();
            var points = new[] { point, point2 };
            var options = new DropAnimationOptions();
            var height = 1m;

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.DropAsync(points, null, height, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_Drop_Async(bool disposeOnComplete)
        {
            var point = new Point();
            var datasource = new DataSource();
            var options = new DropAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };
            var height = 1m;

            var result = await _animationService.DropAsync(point, datasource, height, options);
            Assert.IsType<DropAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Drop.ToAnimationNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == result.Id
                && parameters[1] is IEnumerable<Point>
                && (parameters[1] as IEnumerable<Point>).Contains(point)
                && parameters[2] as string == datasource.Id
                && parameters[3] as decimal? == height
                && parameters[4] is DropAnimationOptions
             )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Drop_ThrowArgumentNullException_PointsCaseAsync()
        {
            Point points = null;
            var datasource = new DataSource();
            var options = new DropAnimationOptions();
            var height = 1m;

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.DropAsync(points, datasource, height, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Drop_ThrowArgumentNullException_SourceCaseAsync()
        {
            var point = new Point();
            var options = new DropAnimationOptions();
            var height = 1m;

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.DropAsync(point, null, height, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_SetCoordinatesAsync(bool disposeOnComplete)
        {
            var point = new Point();
            var datasource = new DataSource();
            var options = new SetCoordinatesAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };
            var newCoordinates = new Position();

            var result = await _animationService.SetCoordinatesAsync(point, datasource, newCoordinates, options);
            Assert.IsType<SetCoordinatesAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetCoordinates.ToAnimationNamespace(), result.Id, point.Id, datasource.Id, newCoordinates, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetCoordinates_ThrowArgumentNullException_PointCaseAsync()
        {
            var datasource = new DataSource();
            var options = new SetCoordinatesAnimationOptions();
            var newCoordinates = new Position();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async() => await _animationService.SetCoordinatesAsync(null, datasource, newCoordinates, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetCoordinates_ThrowArgumentNullException_SourceCaseAsync()
        {
            var point = new Point();
            var options = new SetCoordinatesAnimationOptions();
            var newCoordinates = new Position();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.SetCoordinatesAsync(point, null, newCoordinates, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetCoordinates_ThrowArgumentNullException_NewCoordinatesCaseAsync()
        {
            var point = new Point();
            var datasource = new DataSource();
            var options = new SetCoordinatesAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.SetCoordinatesAsync(point, datasource, null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_SetCoordinates_HtmlMarker_Async(bool disposeOnComplete)
        {
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var options = new SetCoordinatesAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };
            var newCoordinates = new Position();

            var result = await _animationService.SetCoordinatesAsync(marker, newCoordinates, options);
            Assert.IsType<SetCoordinatesAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.SetCoordinates.ToAnimationNamespace(), result.Id, marker.Id, null, newCoordinates, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetCoordinates_HtmlMarker_ThrowArgumentNullException_PointCaseAsync()
        {
            var options = new SetCoordinatesAnimationOptions();
            var newCoordinates = new Position();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.SetCoordinatesAsync(null, newCoordinates, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetCoordinates_HtmlMarker_ThrowArgumentNullException_NewCoordinatesCaseAsync()
        {
            var marker = new HtmlMarker(new HtmlMarkerOptions());
            var options = new SetCoordinatesAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.SetCoordinatesAsync(marker, null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_Morph_Async(bool disposeOnComplete)
        {
            var geometry = new Point();
            var dataSource = new DataSource();
            var newGeometry = new Polygon();
            var options = new MorphAnimationOptions {
                DisposeOnComplete = disposeOnComplete
            };


            var result = await _animationService.MorphAsync(geometry, dataSource, newGeometry, options);
            Assert.IsType<MorphAnimation>(result);
            Assert.NotNull(result.Id);
            Assert.Equal(disposeOnComplete, result.Disposed);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.Morph.ToAnimationNamespace(), result.Id, geometry.Id, dataSource.Id, newGeometry, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Morph_ThrowArgumentNullException_GeometryCaseAsync()
        {
            var dataSource = new DataSource();
            var newGeometry = new Polygon();
            var options = new MorphAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async() => await _animationService.MorphAsync(null, dataSource, newGeometry, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Morph_ThrowArgumentNullException_SourceCaseAsync()
        {
            var geometry = new Point();
            var newGeometry = new Polygon();
            var options = new MorphAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MorphAsync(geometry, null, newGeometry, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Morph_ThrowArgumentNullException_NewGeometryCaseAsync()
        {
            var geometry = new Point();
            var dataSource = new DataSource();
            Polygon newGeometry = null;
            var options = new MorphAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MorphAsync(geometry, dataSource, newGeometry, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_MoveAlongRoute_Async()
        {
            var routePoints = new List<RoutePoint>();
            var dataSource = new DataSource();
            var pin = new Point();
            var options = new RoutePathAnimationOptions();

            var result = await _animationService.MoveAlongRouteAsync(routePoints, pin, dataSource, options);
            Assert.IsType<MoveAlongRouteAnimation>(result);
            Assert.NotNull(result.Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongRoute.ToAnimationNamespace(), result.Id, routePoints, dataSource.Id, pin.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_MoveAlongRoute_ThrowArgumentNullException_RoutePointsCaseAsync()
        {
            var dataSource = new DataSource();
            var pin = new Point();
            var options = new RoutePathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async() => await _animationService.MoveAlongRouteAsync(null, pin, dataSource, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_MoveAlongRoute_ThrowArgumentNullException_PinCaseAsync()
        {
            var routePoints = new List<RoutePoint>();
            var dataSource = new DataSource();
            var options = new RoutePathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongRouteAsync(routePoints, null, dataSource, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_MoveAlongRoute_ThrowArgumentNullException_SourceCaseAsync()
        {
            var routePoints = new List<RoutePoint>();
            var pin = new Point();
            var options = new RoutePathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongRouteAsync(routePoints, pin, null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_MoveAlongRoute_HtmlMarker_Async()
        {
            var routePoints = new List<RoutePoint>();
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new RoutePathAnimationOptions();

            var result = await _animationService.MoveAlongRouteAsync(routePoints, pin, options);
            Assert.IsType<MoveAlongRouteAnimation>(result);
            Assert.NotNull(result.Id);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Animation.MoveAlongRoute.ToAnimationNamespace(), result.Id, routePoints, null, pin.Id, options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_MoveAlongRoute_HtmlMarker_ThrowArgumentNullException_RoutePointsCaseAsync()
        {
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new RoutePathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async() => await _animationService.MoveAlongRouteAsync(null, pin, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_MoveAlongRoute_HtmlMarker_ThrowArgumentNullException_PinCaseAsync()
        {
            var routePoints = new List<RoutePoint>();
            var pin = new HtmlMarker(new HtmlMarkerOptions());
            var options = new RoutePathAnimationOptions();

            await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await _animationService.MoveAlongRouteAsync(routePoints, null, options));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
