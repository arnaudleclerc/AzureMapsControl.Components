namespace AzureMapsControl.Components.Tests.Data.Grid
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data.Grid;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Runtime;

    using Moq;

    using Xunit;

    public class GriddedDataSourceTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_HaveADefaultIdIfInvalid(string id)
        {
            var dataSource = new GriddedDataSource(id);
            Assert.False(string.IsNullOrWhiteSpace(dataSource.Id));
        }

        [Fact]
        public void Should_HaveADefaultId()
        {
            var dataSource = new GriddedDataSource();
            Assert.False(string.IsNullOrWhiteSpace(dataSource.Id));
            Assert.Null(dataSource.Shapes);
        }

        [Fact]
        public void Should_HaveIdAffected()
        {
            const string id = "id";
            var dataSource = new GriddedDataSource(id);
            Assert.Equal(id, dataSource.Id);
            Assert.Null(dataSource.Shapes);
        }

        [Fact]
        public async Task Should_AddShapes_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };

            var shapes = new List<Shape<Point>> {
                new Shape<Point>(new Point())
            };

            await dataSource.AddAsync(shapes);

            Assert.Contains(shapes[0], dataSource.Shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<Point>>
            )), Times.Once);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }


        [Fact]
        public async Task Should_NotAddShapes_NotAddedToMapCase_Async()
        {
            var dataSource = new GriddedDataSource();

            var shapes = new List<Shape<Point>> {
                new Shape<Point>(new Point())
            };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.AddAsync(shapes));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotAddShapes_DisposedCase_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();

            var shapes = new List<Shape<Point>> {
                new Shape<Point>(new Point())
            };

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.AddAsync(shapes));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_AddFeatures_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };

            var features = new List<Feature<Point>> {
                new Feature<Point>()
            };

            await dataSource.AddAsync(features);

            Assert.Contains(features[0], dataSource.Features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<Point>>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotAddFeatures_NotAddedToMapCase_Async()
        {
            var dataSource = new GriddedDataSource();

            var features = new List<Feature<Point>> {
                new Feature<Point>(),
            };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.AddAsync(features));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotAddFeatures_DisposedCase_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();

            var features = new List<Feature<Point>> {
                new Feature<Point>(),
            };

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.AddAsync(features));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_AddFeatures_ParamsVersionAsync()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };

            var point = new Feature<Point>();

            await dataSource.AddAsync(point);

            Assert.Contains(point, dataSource.Features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<Point>>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_AddShapes_Params_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };

            var point0 = new Shape<Point>(new Point());
            var point1 = new Shape<Point>(new Point());

            await dataSource.AddAsync(point0, point1);

            Assert.Contains(point0, dataSource.Shapes);
            Assert.Contains(point1, dataSource.Shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<Point>>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotCallAddCallbackIfGeometriesAreEmpty_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };

            await dataSource.AddAsync(new List<Shape<Point>>());
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotCallAddCallbackIfGeometriesAreNull_Async()
        {
            IEnumerable<Shape<Point>> shapes = null;
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };

            await dataSource.AddAsync(shapes);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }



        [Fact]
        public async Task Should_NotImportDataFromUrl_NotAddedToMapCase_Async()
        {
            var dataSource = new GriddedDataSource();

            const string url = "someurl";

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.ImportDataFromUrlAsync(url));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotImportDataFromUrl_DisposedCase_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();

            const string url = "someurl";

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.ImportDataFromUrlAsync(url));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveShapes_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());
            var shapes = new List<Shape<Point>> { point1, point2 };

            await dataSource.AddAsync(shapes);
            await dataSource.RemoveAsync(point1);

            Assert.DoesNotContain(point1, dataSource.Shapes);
            Assert.Contains(point2, dataSource.Shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveFeatures_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var features = new List<Feature<Point>> { point1, point2 };

            await dataSource.AddAsync(features);
            await dataSource.RemoveAsync(point1);

            Assert.DoesNotContain(point1, dataSource.Features);
            Assert.Contains(point2, dataSource.Features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveShapes_EnumerableVersion_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());
            var shapes = new List<Shape<Point>> { point1, point2 };

            await dataSource.AddAsync(shapes);
            await dataSource.RemoveAsync(new List<Shape> { point1 });

            Assert.DoesNotContain(point1, dataSource.Shapes);
            Assert.Contains(point2, dataSource.Shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveFeatures_EnumerableVersion_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var features = new List<Feature<Point>> { point1, point2 };

            await dataSource.AddAsync(features);
            await dataSource.RemoveAsync(point1);

            Assert.DoesNotContain(point1, dataSource.Features);
            Assert.Contains(point2, dataSource.Features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveShapes_IdsVersion_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());
            var shapes = new List<Shape<Point>> { point1, point2 };

            await dataSource.AddAsync(shapes);
            await dataSource.RemoveAsync(point1.Id);

            Assert.DoesNotContain(point1, dataSource.Shapes);
            Assert.Contains(point2, dataSource.Shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveFeatures_IdsVersion_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var features = new List<Feature<Point>> { point1, point2 };

            await dataSource.AddAsync(features);
            await dataSource.RemoveAsync(point1.Id);

            Assert.DoesNotContain(point1, dataSource.Features);
            Assert.Contains(point2, dataSource.Features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveShapesAndFeatures_IdsVersion_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var point3 = new Shape<Point>("point3", new Point());
            var point4 = new Shape<Point>("point4", new Point());
            var features = new List<Feature<Point>> { point1, point2 };
            var shapes = new List<Shape<Point>> { point3, point4 };

            await dataSource.AddAsync(features);
            await dataSource.AddAsync(shapes);

            await dataSource.RemoveAsync(point1.Id, point3.Id);

            Assert.DoesNotContain(point1, dataSource.Features);
            Assert.Contains(point2, dataSource.Features);
            Assert.DoesNotContain(point3, dataSource.Shapes);
            Assert.Contains(point4, dataSource.Shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Contains(point1.Id)
                && (parameters[1] as IEnumerable<string>).Contains(point3.Id)
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveShapes_IdsEnumerableVersion_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());
            var shapes = new List<Shape<Point>> { point1, point2 };

            await dataSource.AddAsync(shapes);
            await dataSource.RemoveAsync(new List<string> { point1.Id });

            Assert.DoesNotContain(point1, dataSource.Shapes);
            Assert.Contains(point2, dataSource.Shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveFeatures_IdsEnumerableVersion_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var features = new List<Feature<Point>> { point1, point2 };

            await dataSource.AddAsync(features);
            await dataSource.RemoveAsync(new List<string> { point1.Id });

            Assert.DoesNotContain(point1, dataSource.Features);
            Assert.Contains(point2, dataSource.Features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotRemoveShapes_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());

            await dataSource.AddAsync(point2);
            await dataSource.RemoveAsync(point1);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotRemoveFeatures_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());

            await dataSource.AddAsync(point2);
            await dataSource.RemoveAsync(point1);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotRemoveShape_NullCheck_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());

            await dataSource.RemoveAsync(point1);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotRemoveShapesNorFeatures_NullCheck_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var shape = new Shape<Point>("point1", new Point());
            var feature = new Feature<Point>("point2", new Point());

            IEnumerable<Shape> shapes = null;
            IEnumerable<Feature> features = null;

            await dataSource.AddAsync(shape);
            await dataSource.AddAsync(feature);
            await dataSource.RemoveAsync(shapes, features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotRemoveShapesButOnlyFeatures_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var shape = new Shape<Point>("point1", new Point());
            var feature = new Feature<Point>("point2", new Point());

            IEnumerable<Shape> shapes = null;
            Feature[] features = new[] { feature };

            await dataSource.AddAsync(shape);
            await dataSource.AddAsync(feature);
            await dataSource.RemoveAsync(shapes, features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == feature.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotRemoveFeaturesButOnlyShapes_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var shape = new Shape<Point>("point1", new Point());
            var feature = new Feature<Point>("point2", new Point());

            Shape[] shapes = new[] { shape };
            Feature[] features = null;

            await dataSource.AddAsync(shape);
            await dataSource.AddAsync(feature);
            await dataSource.RemoveAsync(shapes, features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == shape.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_RemoveShapesAndFeatures_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var shape = new Shape<Point>("point1", new Point());
            var feature = new Feature<Point>("point2", new Point());

            Shape[] shapes = new[] { shape };
            Feature[] features = new[] { feature };

            await dataSource.AddAsync(shape);
            await dataSource.AddAsync(feature);
            await dataSource.RemoveAsync(shapes, features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Contains(shape.Id)
                && (parameters[1] as IEnumerable<string>).Contains(feature.Id)
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ClearDataSource_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point2 = new Shape<Point>("point2", new Point());
            var feature = new Feature<Point>("point1", new Point());

            await dataSource.AddAsync(point2);
            await dataSource.AddAsync(feature);
            await dataSource.ClearAsync();

            Assert.Null(dataSource.Shapes);
            Assert.Null(dataSource.Features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ClearDataSource_NotAddedToMapCase_Async()
        {
            var dataSource = new GriddedDataSource();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.ClearAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_ClearDataSource_DisposedCase_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.ClearAsync());

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_DisposeAsync()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();
            Assert.True(dataSource.Disposed);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotDispose_NotAddedToMapCase_Async()
        {
            var dataSource = new GriddedDataSource();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.DisposeAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotDispose_DisposedCase_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.DisposeAsync());
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetOptionsAsync()
        {
            var options = new GriddedDataSourceOptions();
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<GriddedDataSourceOptions>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(options);
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };

            var result = await dataSource.GetOptionsAsync();
            Assert.Equal(options, result);
            Assert.Equal(options, dataSource.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<GriddedDataSourceOptions>(Constants.JsConstants.Methods.Source.GetOptions.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetOptions_NotAddedToMapCase_Async()
        {
            var dataSource = new GriddedDataSource() { };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.GetOptionsAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetOptions_DisposedCase_Async()
        {
            var dataSource = new GriddedDataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.GetOptionsAsync());
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetOptionsAsync()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object,
                Options = new GriddedDataSourceOptions {
                    CellWidth = 1
                }
            };

            await datasource.SetOptionsAsync(options => options.CenterLatitude = 2);
            Assert.Equal(1, datasource.Options.CellWidth);
            Assert.Equal(2, datasource.Options.CenterLatitude);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.SetOptions.ToSourceNamespace(), datasource.Id, datasource.Options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetOptions_NoInitialValueAsync()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.SetOptionsAsync(options => options.CenterLatitude = 2);
            Assert.Equal(2, datasource.Options.CenterLatitude);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.SetOptions.ToSourceNamespace(), datasource.Id, datasource.Options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetOptions_NotAddedToMapCase_Async()
        {
            var datasource = new GriddedDataSource();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await datasource.SetOptionsAsync(options => options.CenterLatitude = 2));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetOptions_DisposedCase_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };
            await datasource.DisposeAsync();

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await datasource.SetOptionsAsync(options => options.CenterLatitude = 2));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetCellChildren_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            var cellChildren = System.ArraySegment<Feature<Point>>.Empty;
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<IEnumerable<Feature<Point>>>(It.IsAny<string>(), It.IsAny<object[]>()))
                .ReturnsAsync(cellChildren);

            var cellId = "cellId";
            var result = await datasource.GetCellChildrenAsync(cellId);

            Assert.Equal(cellChildren, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<IEnumerable<Feature<Point>>>(Constants.JsConstants.Methods.GriddedDatasource.GetCellChildren.ToGriddedDatasourceNamespace(), datasource.Id, cellId), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetCellChildren_NotAddedToMapCase_Async()
        {
            var datasource = new GriddedDataSource();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await datasource.GetCellChildrenAsync("cellId"));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetCellChildren_DisposedCase_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await datasource.GetCellChildrenAsync("cellId"));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetGridCells_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            var gridCells = System.ArraySegment<Feature<Polygon>>.Empty;
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<IEnumerable<Feature<Polygon>>>(It.IsAny<string>(), It.IsAny<object[]>()))
                .ReturnsAsync(gridCells);

            var result = await datasource.GetGridCellsAsync();
            Assert.Equal(gridCells, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<IEnumerable<Feature<Polygon>>>(Constants.JsConstants.Methods.GriddedDatasource.GetGridCells.ToGriddedDatasourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetGridCells_NotAddedToMapCase_Async()
        {
            var datasource = new GriddedDataSource();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await datasource.GetGridCellsAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetGridCells_DisposedCase_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await datasource.GetGridCellsAsync());

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetPoints_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            var points = System.ArraySegment<Feature<Point>>.Empty;
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<IEnumerable<Feature<Point>>>(It.IsAny<string>(), It.IsAny<object[]>()))
                .ReturnsAsync(points);

            var result = await datasource.GetPointsAsync();
            Assert.Equal(points, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<IEnumerable<Feature<Point>>>(Constants.JsConstants.Methods.GriddedDatasource.GetPoints.ToGriddedDatasourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetPoint_NotAddedToMapCase_Async()
        {
            var datasource = new GriddedDataSource();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await datasource.GetPointsAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetPoints_DisposedCase_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await datasource.GetPointsAsync());

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetPoints_WithFeatureCollection_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            var featureCollection = JsonDocument.Parse("{}");
            await datasource.SetPointsAsync(featureCollection);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GriddedDatasource.SetFeatureCollectionPoints.ToGriddedDatasourceNamespace(), datasource.Id, featureCollection), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetPoints_WithFeatureCollection_NotAddedToMapCase_Async()
        {
            var datasource = new GriddedDataSource();

            var featureCollection = JsonDocument.Parse("{}");
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await datasource.SetPointsAsync(featureCollection));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetPoints_WithFeatureCollection_DisposedCase_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.DisposeAsync();
            var featureCollection = JsonDocument.Parse("{}");
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await datasource.SetPointsAsync(featureCollection));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetPoints_WithFeaturePoints_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            var features = new Feature<Point>[] { new Feature<Point>() };
            await datasource.SetPointsAsync(features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GriddedDatasource.SetFeaturePoints.ToGriddedDatasourceNamespace(), datasource.Id, features), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetPoints_WithFeaturePoints_NotAddedToMapCase_Async()
        {
            var datasource = new GriddedDataSource();

            var features = new Feature<Point>[] { new Feature<Point>() };
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await datasource.SetPointsAsync(features));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetPoints_WithFeaturePoints_DisposedCase_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.DisposeAsync();
            var features = new Feature<Point>[] { new Feature<Point>() };
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await datasource.SetPointsAsync(features));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetPoints_WithPoints_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            var points = new Point[] { new Point() };
            await datasource.SetPointsAsync(points);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GriddedDatasource.SetPoints.ToGriddedDatasourceNamespace(), datasource.Id, points), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetPoints_WithPoints_NotAddedToMapCase_Async()
        {
            var datasource = new GriddedDataSource();

            var points = new Point[] { new Point() };
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await datasource.SetPointsAsync(points));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetPoints_WithPoints_DisposedCase_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.DisposeAsync();
            var points = new Point[] { new Point() };
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await datasource.SetPointsAsync(points));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_SetPoints_WithShapes_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            var shapes = new Shape<Point>[] { new Shape<Point>() };
            await datasource.SetPointsAsync(shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.GriddedDatasource.SetShapePoints.ToGriddedDatasourceNamespace(), datasource.Id, shapes), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetPoints_WithShapes_NotAddedToMapCase_Async()
        {
            var datasource = new GriddedDataSource();

            var shapes = new Shape<Point>[] { new Shape<Point>() };
            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await datasource.SetPointsAsync(shapes));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotSetPoints_WithShapes_DisposedCase_Async()
        {
            var datasource = new GriddedDataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.DisposeAsync();
            var shapes = new Shape<Point>[] { new Shape<Point>() };
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await datasource.SetPointsAsync(shapes));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
