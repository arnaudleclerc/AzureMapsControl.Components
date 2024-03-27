namespace AzureMapsControl.Components.Tests.Layers
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Layers;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class LayerTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new Mock<IMapJsRuntime>();

        private class DummyLayerOptions : LayerOptions
        {
        }

        private class DummyLayer : Layer<DummyLayerOptions>
        {
            public DummyLayer() : base("id", LayerType.BubbleLayer) { }
        }

        [Fact]
        public async Task SetOptionsAsync_Should_CallInteropAsync()
        {
            var layer = new DummyLayer {
                _mapJsRuntime = _jsRuntimeMock.Object,
                    Options = new DummyLayerOptions {
                }
            };

            await layer.SetOptionsAsync(options => options.MinZoom = 2);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Layer.SetOptions.ToLayerNamespace(), layer.Id, layer.Options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();

            Assert.Equal(2, layer.Options.MinZoom);
        }

        [Fact]
        public async Task SetOptionsAsync_Should_ThrowLayerOptionsNullExceptionAsync()
        {
            var layer = new DummyLayer {
                _mapJsRuntime = _jsRuntimeMock.Object
            };
            await layer.SetOptionsAsync(options => options.MinZoom = 2);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Layer.SetOptions.ToLayerNamespace(), layer.Id, layer.Options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
            Assert.Equal(2, layer.Options.MinZoom);
        }

        [Fact]
        public async Task SetOptionsAsync_Should_ThrowComponentNotAddedToMapExceptionAsync()
        {
            var layer = new DummyLayer { };
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await layer.SetOptionsAsync(options => options.MinZoom = 2));
            _jsRuntimeMock.VerifyNoOtherCalls();
            Assert.Null(layer.Options);
        }
    }
}
