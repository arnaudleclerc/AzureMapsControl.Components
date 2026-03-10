namespace AzureMapsControl.Components.Tests.Drawing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Drawing;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    public class DrawingManagerTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();
        private readonly Mock<ILogger> _loggerMock = new();
        private const string TestMapId = "test-map-id";

        [Fact]
        public void Should_HaveDefaultProperties()
        {
            var drawingManager = new DrawingManager();

            Assert.False(drawingManager.Disposed);
            Assert.Null(drawingManager.JSRuntime);
            Assert.Null(drawingManager.Logger);
        }

        [Fact]
        public void Should_SetJSRuntimeAndLogger()
        {
            var drawingManager = CreateInitializedDrawingManager();

            Assert.Equal(_jsRuntimeMock.Object, drawingManager.JSRuntime);
            Assert.Equal(_loggerMock.Object, drawingManager.Logger);
        }

        [Fact]
        public async Task Should_AddShapes_AllSupportedGeometryTypes_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            var shapes = new List<Shape>
            {
                new Shape<Point>(new Point()),
                new Shape<LineString>(new LineString()),
                new Shape<MultiLineString>(new MultiLineString()),
                new Shape<MultiPoint>(new MultiPoint()),
                new Shape<MultiPolygon>(new MultiPolygon()),
                new Shape<Polygon>(new Polygon()),
                new Shape<RoutePoint>(new RoutePoint()),
            };

            await drawingManager.AddShapesAsync(shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<Point>>
                && (parameters[1] as IEnumerable<Shape<Point>>).Count() == 1
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<LineString>>
                && (parameters[1] as IEnumerable<Shape<LineString>>).Count() == 1
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<MultiLineString>>
                && (parameters[1] as IEnumerable<Shape<MultiLineString>>).Count() == 1
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<MultiPoint>>
                && (parameters[1] as IEnumerable<Shape<MultiPoint>>).Count() == 1
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<MultiPolygon>>
                && (parameters[1] as IEnumerable<Shape<MultiPolygon>>).Count() == 1
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<Polygon>>
                && (parameters[1] as IEnumerable<Shape<Polygon>>).Count() == 1
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<RoutePoint>>
                && (parameters[1] as IEnumerable<Shape<RoutePoint>>).Count() == 1
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.IsAny<object[]>()), Times.Exactly(7));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_AddMultipleShapesOfSameType_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            var shapes = new List<Shape>
            {
                new Shape<Point>(new Point()),
                new Shape<Point>(new Point()),
                new Shape<Point>(new Point())
            };

            await drawingManager.AddShapesAsync(shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<Point>>
                && (parameters[1] as IEnumerable<Shape<Point>>).Count() == 3
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_HandleMixedGeometryTypes_InSingleCall_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            var shapes = new List<Shape>
            {
                new Shape<Point>(new Point()),
                new Shape<Point>(new Point()),
                new Shape<LineString>(new LineString()),
                new Shape<Polygon>(new Polygon()),
                new Shape<Polygon>(new Polygon()),
                new Shape<Polygon>(new Polygon())
            };

            await drawingManager.AddShapesAsync(shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<Point>>
                && (parameters[1] as IEnumerable<Shape<Point>>).Count() == 2
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<LineString>>
                && (parameters[1] as IEnumerable<Shape<LineString>>).Count() == 1
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<Polygon>>
                && (parameters[1] as IEnumerable<Shape<Polygon>>).Count() == 3
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.IsAny<object[]>()), Times.Exactly(3));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_HandleLargeNumberOfShapes_Efficiently_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            // Create a large collection of shapes
            var shapes = new List<Shape>();
            for (int i = 0; i < 1000; i++)
            {
                shapes.Add(new Shape<Point>(new Point()));
            }

            await drawingManager.AddShapesAsync(shapes);

            // Should still only make one call per geometry type
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == TestMapId
                && parameters[1] is IEnumerable<Shape<Point>>
                && (parameters[1] as IEnumerable<Shape<Point>>).Count() == 1000
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotAddShapes_WhenNull_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            await drawingManager.AddShapesAsync(null);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotAddShapes_WhenEmpty_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            await drawingManager.AddShapesAsync(new List<Shape>());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ClearShapes_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            await drawingManager.ClearAsync();

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToDrawingNamespace(), TestMapId), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_AccumulateShapes_InInternalState_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            var firstBatch = new List<Shape> { new Shape<Point>(new Point()) };
            var secondBatch = new List<Shape> { new Shape<LineString>(new LineString()) };
            var thirdBatch = new List<Shape> { new Shape<Point>(new Point()) };

            await drawingManager.AddShapesAsync(firstBatch);
            await drawingManager.AddShapesAsync(secondBatch);
            await drawingManager.AddShapesAsync(thirdBatch);

            // Verify internal state accumulates all shapes
            var sourceShapes = GetInternalSourceShapes(drawingManager);
            Assert.Equal(3, sourceShapes.Count);
            Assert.Contains(firstBatch[0], sourceShapes);
            Assert.Contains(secondBatch[0], sourceShapes);
            Assert.Contains(thirdBatch[0], sourceShapes);

            // Verify correct number of JS calls
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(
                Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(),
                It.IsAny<object[]>()), Times.Exactly(3)); // 1 Point call + 1 LineString call + 1 more Point call
        }

        [Fact]
        public async Task Should_ClearInternalState_WhenCleared_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();
            var shapes = new List<Shape> { new Shape<Point>(new Point()) };

            // Add shapes to initialize and populate internal state
            await drawingManager.AddShapesAsync(shapes);
            var sourceShapes = GetInternalSourceShapes(drawingManager);
            Assert.NotNull(sourceShapes);
            Assert.Single(sourceShapes);

            // Clear should reset internal state
            await drawingManager.ClearAsync();
            sourceShapes = GetInternalSourceShapes(drawingManager);
            Assert.Null(sourceShapes);

            // Adding again should reinitialize state
            await drawingManager.AddShapesAsync(shapes);
            sourceShapes = GetInternalSourceShapes(drawingManager);
            Assert.NotNull(sourceShapes);
            Assert.Single(sourceShapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.IsAny<object[]>()), Times.Exactly(2));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToDrawingNamespace(), TestMapId), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Dispose_Successfully()
        {
            var drawingManager = CreateInitializedDrawingManager();

            Assert.False(drawingManager.Disposed);

            drawingManager.Dispose();

            Assert.True(drawingManager.Disposed);
        }

        [Fact]
        public async Task Should_ThrowComponentNotAddedToMapException_WhenJSRuntimeIsNull_Clear_Async()
        {
            var drawingManager = new DrawingManager();

            await Assert.ThrowsAsync<ComponentNotAddedToMapException>(async () => await drawingManager.ClearAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowComponentNotAddedToMapException_WhenJSRuntimeIsNull_AddShapes_Async()
        {
            var drawingManager = new DrawingManager();
            var shapes = new List<Shape> { new Shape<Point>(new Point()) };

            await Assert.ThrowsAsync<ComponentNotAddedToMapException>(async () => await drawingManager.AddShapesAsync(shapes));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_ThrowComponentNotAddedToMapException_WhenJSRuntimeIsNull_Dispose()
        {
            var drawingManager = new DrawingManager();

            Assert.Throws<ComponentNotAddedToMapException>(() => drawingManager.Dispose());
        }

        [Fact]
        public async Task Should_ThrowComponentDisposedException_WhenDisposed_Clear_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            drawingManager.Dispose();

            await Assert.ThrowsAsync<ComponentDisposedException>(async () => await drawingManager.ClearAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ThrowComponentDisposedException_WhenDisposed_AddShapes_Async()
        {
            var drawingManager = CreateInitializedDrawingManager();

            drawingManager.Dispose();
            var shapes = new List<Shape> { new Shape<Point>(new Point()) };

            await Assert.ThrowsAsync<ComponentDisposedException>(async () => await drawingManager.AddShapesAsync(shapes));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_ThrowComponentDisposedException_WhenAlreadyDisposed_Dispose()
        {
            var drawingManager = CreateInitializedDrawingManager();
            drawingManager.Dispose();

            Assert.Throws<ComponentDisposedException>(() => drawingManager.Dispose());
        }

        private DrawingManager CreateInitializedDrawingManager()
        {
            return new DrawingManager() {
                JSRuntime = _jsRuntimeMock.Object,
                Logger = _loggerMock.Object,
                MapId = TestMapId
            };
        }

        /// <summary>
        /// Uses reflection to access the private _sourceShapes field for testing internal state
        /// </summary>
        private List<Shape> GetInternalSourceShapes(DrawingManager drawingManager)
        {
            var field = typeof(DrawingManager).GetField("_sourceShapes",
                BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(drawingManager) as List<Shape>;
        }
    }
}
