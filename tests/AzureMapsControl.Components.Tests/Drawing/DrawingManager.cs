namespace AzureMapsControl.Components.Tests.Drawing
{
    using System.Collections.Generic;
    using System.Linq;
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

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<Point>>>(s => s.Count() == 1)), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<LineString>>>(s => s.Count() == 1)), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<MultiLineString>>>(s => s.Count() == 1)), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<MultiPoint>>>(s => s.Count() == 1)), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<MultiPolygon>>>(s => s.Count() == 1)), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<Polygon>>>(s => s.Count() == 1)), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<RoutePoint>>>(s => s.Count() == 1)), Times.Once);
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

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<Point>>>(s => s.Count() == 3)), Times.Once);
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

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<Point>>>(s => s.Count() == 2)), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<LineString>>>(s => s.Count() == 1)), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<Polygon>>>(s => s.Count() == 3)), Times.Once);
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
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.Is<IEnumerable<Shape<Point>>>(s => s.Count() == 1000)), Times.Once);
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

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToDrawingNamespace()), Times.Once);
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

            // Add shapes then clear
            await drawingManager.AddShapesAsync(shapes);
            await drawingManager.ClearAsync();

            // Adding again should work without errors
            await drawingManager.AddShapesAsync(shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToDrawingNamespace(), It.IsAny<object[]>()), Times.Exactly(2));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToDrawingNamespace()), Times.Once);
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
                Logger = _loggerMock.Object
            };
        }
    }
}
