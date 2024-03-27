namespace AzureMapsControl.Components.Tests.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Exceptions;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Options;

    using Moq;

    using Xunit;

    public class DataSourceTests
    {
        private readonly Mock<IMapJsRuntime> _jsRuntimeMock = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_HaveADefaultIdIfInvalid(string id)
        {
            var dataSource = new DataSource(id);
            Assert.False(string.IsNullOrWhiteSpace(dataSource.Id));
        }

        [Fact]
        public void Should_HaveADefaultId()
        {
            var dataSource = new DataSource();
            Assert.False(string.IsNullOrWhiteSpace(dataSource.Id));
            Assert.Null(dataSource.Shapes);
        }

        [Fact]
        public void Should_HaveIdAffected()
        {
            const string id = "id";
            var dataSource = new DataSource(id);
            Assert.Equal(id, dataSource.Id);
            Assert.Null(dataSource.Shapes);
        }

        [Fact]
        public async void Should_AddShapes_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            var shapes = new List<Shape> {
                new Shape<Point>(new Point()),
                new Shape<LineString>(new LineString()),
                new Shape<MultiLineString>(new MultiLineString()),
                new Shape<MultiPoint>(new MultiPoint()),
                new Shape<MultiPolygon>(new MultiPolygon()),
                new Shape<Polygon>(new Polygon()),
                new Shape<RoutePoint>(new RoutePoint()),
            };

            await dataSource.AddAsync(shapes);

            Assert.Contains(shapes[0], dataSource.Shapes);
            Assert.Contains(shapes[1], dataSource.Shapes);
            Assert.Contains(shapes[2], dataSource.Shapes);
            Assert.Contains(shapes[3], dataSource.Shapes);
            Assert.Contains(shapes[4], dataSource.Shapes);
            Assert.Contains(shapes[5], dataSource.Shapes);
            Assert.Contains(shapes[6], dataSource.Shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<Point>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<LineString>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<MultiLineString>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<MultiPoint>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<MultiPolygon>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<Polygon>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<RoutePoint>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Exactly(7));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }


        [Fact]
        public async void Should_NotAddShapes_NotAddedToMapCase_Async()
        {
            var dataSource = new DataSource();

            var shapes = new List<Shape> {
                new Shape<Point>(new Point())
            };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.AddAsync(shapes));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddShapes_DisposedCase_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();

            var shapes = new List<Shape> {
                new Shape<Point>(new Point())
            };

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.AddAsync(shapes));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }


        [Fact]
        public async void Should_AddLineStringsShapes_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            var shapes = new List<Shape> {
                new Shape<LineString>(new LineString()),
                new Shape<LineString>(new LineString())
            };

            await dataSource.AddAsync(shapes);

            Assert.Contains(shapes[0], dataSource.Shapes);
            Assert.Contains(shapes[1], dataSource.Shapes);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Shape<LineString>>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddFeatures_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            var features = new List<Feature> {
                new Feature<Point>(),
                new Feature<LineString>(),
                new Feature<MultiLineString>(),
                new Feature<MultiPoint>(),
                new Feature<MultiPolygon>(),
                new Feature<Polygon>(),
                new Feature<RoutePoint>()
            };

            await dataSource.AddAsync(features);

            Assert.Contains(features[0], dataSource.Features);
            Assert.Contains(features[1], dataSource.Features);
            Assert.Contains(features[2], dataSource.Features);
            Assert.Contains(features[3], dataSource.Features);
            Assert.Contains(features[4], dataSource.Features);
            Assert.Contains(features[5], dataSource.Features);
            Assert.Contains(features[6], dataSource.Features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<Point>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<LineString>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<MultiLineString>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<MultiPoint>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<MultiPolygon>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<Polygon>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<RoutePoint>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Exactly(7));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddFeatures_NotAddedToMapCase_Async()
        {
            var dataSource = new DataSource();

            var features = new List<Feature> {
                new Feature<Point>(),
            };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.AddAsync(features));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddFeatures_DisposedCase_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();

            var features = new List<Feature> {
                new Feature<Point>(),
            };

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.AddAsync(features));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddFeatures_ParamsVersionAsync()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            var point = new Feature<Point>();
            var lineString = new Feature<LineString>();
            var multiLineString = new Feature<MultiLineString>();
            var multiPoint = new Feature<MultiPoint>();
            var multiPolygon = new Feature<MultiPolygon>();
            var polygon = new Feature<Polygon>();
            var routePoint = new Feature<RoutePoint>();

            await dataSource.AddAsync(point, lineString, multiLineString, multiPoint, multiPolygon, polygon, routePoint);

            Assert.Contains(point, dataSource.Features);
            Assert.Contains(lineString, dataSource.Features);
            Assert.Contains(multiLineString, dataSource.Features);
            Assert.Contains(multiPoint, dataSource.Features);
            Assert.Contains(multiPolygon, dataSource.Features);
            Assert.Contains(polygon, dataSource.Features);
            Assert.Contains(routePoint, dataSource.Features);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<Point>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<LineString>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<MultiLineString>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<MultiPoint>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<MultiPolygon>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<Polygon>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Feature<RoutePoint>>
            )), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Exactly(7));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddShapes_Params_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

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
        public async void Should_NotCallAddCallbackIfGeometriesAreEmpty_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            await dataSource.AddAsync(new List<Shape>());
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotCallAddCallbackIfGeometriesAreNull_Async()
        {
            IEnumerable<Shape> shapes = null;
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            await dataSource.AddAsync(shapes);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ImportDataFromUrl_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            const string url = "someurl";

            await dataSource.ImportDataFromUrlAsync(url);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.ImportDataFromUrl.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] as string == url
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotImportDataFromUrl_NotAddedToMapCase_Async()
        {
            var dataSource = new DataSource();

            const string url = "someurl";

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.ImportDataFromUrlAsync(url));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotImportDataFromUrl_DisposedCase_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();

            const string url = "someurl";

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.ImportDataFromUrlAsync(url));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveShapes_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());
            var shapes = new List<Shape> { point1, point2 };

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
        public async void Should_RemoveFeatures_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var features = new List<Feature> { point1, point2 };

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
        public async void Should_RemoveShapes_EnumerableVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());
            var shapes = new List<Shape> { point1, point2 };

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
        public async void Should_RemoveFeatures_EnumerableVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var features = new List<Feature> { point1, point2 };

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
        public async void Should_RemoveShapes_IdsVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());
            var shapes = new List<Shape> { point1, point2 };

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
        public async void Should_RemoveFeatures_IdsVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var features = new List<Feature> { point1, point2 };

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
        public async void Should_RemoveShapesAndFeatures_IdsVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var point3 = new Shape<Point>("point3", new Point());
            var point4 = new Shape<Point>("point4", new Point());
            var features = new List<Feature> { point1, point2 };
            var shapes = new List<Shape> { point3, point4 };

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
        public async void Should_RemoveShapes_IdsEnumerableVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());
            var shapes = new List<Shape> { point1, point2 };

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
        public async void Should_RemoveFeatures_IdsEnumerableVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());
            var features = new List<Feature> { point1, point2 };

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
        public async void Should_NotRemoveShapes_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());
            var point2 = new Shape<Point>("point2", new Point());

            await dataSource.AddAsync(point2);
            await dataSource.RemoveAsync(point1);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddShapes.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemoveFeatures_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Feature<Point>("point1", new Point());
            var point2 = new Feature<Point>("point2", new Point());

            await dataSource.AddAsync(point2);
            await dataSource.RemoveAsync(point1);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemoveShape_NullCheck_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Shape<Point>("point1", new Point());

            await dataSource.RemoveAsync(point1);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemoveShapesNorFeatures_NullCheck_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
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
        public async void Should_NotRemoveShapesButOnlyFeatures_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
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
        public async void Should_NotRemoveFeaturesButOnlyShapes_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
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
        public async void Should_RemoveShapesAndFeatures_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
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
        public async void Should_ClearDataSource_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
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
        public async void Should_ClearDataSource_NotAddedToMapCase_Async()
        {
            var dataSource = new DataSource();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.ClearAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ClearDataSource_DisposedCase_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.ClearAsync());

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_DisposeAsync()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();
            Assert.True(dataSource.Disposed);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotDispose_NotAddedToMapCase_Async()
        {
            var dataSource = new DataSource();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.DisposeAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotDispose_DisposedCase_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.DisposeAsync());
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_GetOptionsAsync()
        {
            var options = new DataSourceOptions();
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<DataSourceOptions>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(options);
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            var result = await dataSource.GetOptionsAsync();
            Assert.Equal(options, result);
            Assert.Equal(options, dataSource.Options);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<DataSourceOptions>(Constants.JsConstants.Methods.Source.GetOptions.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotGetOptions_NotAddedToMapCase_Async()
        {
            var dataSource = new DataSource() { };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.GetOptionsAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotGetOptions_DisposedCase_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.GetOptionsAsync());
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_GetShapesAsync()
        {
            var shapes = Array.Empty<Shape<Geometry>>();
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<IEnumerable<Shape<Geometry>>>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(shapes);
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            var result = await dataSource.GetShapesAsync();
            Assert.Equal(shapes, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<IEnumerable<Shape<Geometry>>>(Constants.JsConstants.Methods.Datasource.GetShapes.ToDatasourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotGetShapes_NotAddedToMapCase_Async()
        {
            var dataSource = new DataSource() { };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.GetShapesAsync());

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotGetShapes_DisposedCase_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.GetShapesAsync());
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Dispatch_DataAdded()
        {
            var dataSource = new DataSource();
            var eventTriggered = false;
            var eventArgs = new DataSourceEventArgs {
                Type = DataSourceEventType.DataAdded.ToString()
            };
            dataSource.OnDataAdded += _ => eventTriggered = true;
            dataSource.DispatchEvent(eventArgs);
            Assert.True(eventTriggered);
        }

        [Fact]
        public void Should_Dispatch_DataRemoved()
        {
            var dataSource = new DataSource();
            var eventTriggered = false;
            var eventArgs = new DataSourceEventArgs {
                Type = DataSourceEventType.DataRemoved.ToString()
            };
            dataSource.OnDataRemoved += _ => eventTriggered = true;
            dataSource.DispatchEvent(eventArgs);
            Assert.True(eventTriggered);
        }

        [Fact]
        public void Should_Dispatch_DataSourceUpdated()
        {
            var dataSource = new DataSource();
            var eventTriggered = false;
            var eventArgs = new DataSourceEventArgs {
                Type = DataSourceEventType.DataSourceUpdated.ToString()
            };
            dataSource.OnDataSourceUpdated += () => eventTriggered = true;
            dataSource.DispatchEvent(eventArgs);
            Assert.True(eventTriggered);
        }

        [Fact]
        public void Should_Dispatch_SourceAdded()
        {
            var dataSource = new DataSource();
            var eventTriggered = false;
            var eventArgs = new DataSourceEventArgs {
                Type = DataSourceEventType.SourceAdded.ToString()
            };
            dataSource.OnSourceAdded += () => eventTriggered = true;
            dataSource.DispatchEvent(eventArgs);
            Assert.True(eventTriggered);
        }

        [Fact]
        public void Should_Dispatch_SourceRemoved()
        {
            var dataSource = new DataSource();
            var eventTriggered = false;
            var eventArgs = new DataSourceEventArgs {
                Type = DataSourceEventType.SourceRemoved.ToString()
            };
            dataSource.OnSourceRemoved += () => eventTriggered = true;
            dataSource.DispatchEvent(eventArgs);
            Assert.True(eventTriggered);
        }

        [Fact]
        public async void Should_AddJson_Async()
        {
            var shapes = Array.Empty<Shape<Geometry>>();
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            await dataSource.AddAsync("{}");

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatureCollection.ToSourceNamespace(), dataSource.Id, It.IsAny<JsonDocument>()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddInvalidJson_Async()
        {
            var shapes = Array.Empty<Shape<Geometry>>();
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            await Assert.ThrowsAnyAsync<JsonException>(async () => await dataSource.AddAsync("{"));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddJson_NotAddedToMapCase_Async()
        {
            var dataSource = new DataSource() { };

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.AddAsync("{}"));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddJson_DisposedCase_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.AddAsync("{}"));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddJson_JsonVersion_Async()
        {
            var shapes = Array.Empty<Shape<Geometry>>();
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            var jsonDocument = JsonDocument.Parse("{}");

            await dataSource.AddAsync(jsonDocument);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatureCollection.ToSourceNamespace(), dataSource.Id, jsonDocument), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddJson_NotAddedToMapCase_JsonVersion_Async()
        {
            var dataSource = new DataSource() { };
            var jsonDocument = JsonDocument.Parse("{}");

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await dataSource.AddAsync(jsonDocument));
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotAddJson_DisposedCase_JsonVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            await dataSource.DisposeAsync();
            var jsonDocument = JsonDocument.Parse("{}");
            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await dataSource.AddAsync(jsonDocument));
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOptionsAsync()
        {
            var datasource = new DataSource {
                JSRuntime = _jsRuntimeMock.Object,
                Options = new DataSourceOptions {
                    Buffer = 1
                }
            };

            await datasource.SetOptionsAsync(options => options.ClusterMaxZoom = 2);
            Assert.Equal(1, datasource.Options.Buffer);
            Assert.Equal(2, datasource.Options.ClusterMaxZoom);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.SetOptions.ToSourceNamespace(), datasource.Id, datasource.Options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_SetOptions_NoInitialValueAsync()
        {
            var datasource = new DataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.SetOptionsAsync(options => options.ClusterMaxZoom = 2);
            Assert.Equal(2, datasource.Options.ClusterMaxZoom);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.SetOptions.ToSourceNamespace(), datasource.Id, datasource.Options), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetOptions_NotAddedToMapCase_Async()
        {
            var datasource = new DataSource();

            await Assert.ThrowsAnyAsync<ComponentNotAddedToMapException>(async () => await datasource.SetOptionsAsync(options => options.ClusterMaxZoom = 2));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotSetOptions_DisposedCase_Async()
        {
            var datasource = new DataSource {
                JSRuntime = _jsRuntimeMock.Object
            };
            await datasource.DisposeAsync();

            await Assert.ThrowsAnyAsync<ComponentDisposedException>(async () => await datasource.SetOptionsAsync(options => options.ClusterMaxZoom = 2));

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Dispose.ToSourceNamespace(), datasource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetClusterLeaves()
        {
            var datasource = new DataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            var expected = Enumerable.Empty<Feature<Geometry>>();
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<IEnumerable<Feature<Geometry>>>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(expected);

            var clusterId = 1;
            var limit = 2;
            var offset = 3;
            var result = await datasource.GetClusterLeavesAsync(clusterId, limit, offset);

            Assert.Equal(expected, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<IEnumerable<Feature<Geometry>>>(Constants.JsConstants.Methods.Datasource.GetClusterLeaves.ToDatasourceNamespace(), datasource.Id, clusterId, limit, offset), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetClusterLeaves_NotAddedToMapCase()
        {
            var datasource = new DataSource();

            await Assert.ThrowsAsync<ComponentNotAddedToMapException>(async () => await datasource.GetClusterLeavesAsync(1, 2, 3));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetClusterLeaves_DisposedCase()
        {
            var datasource = new DataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.DisposeAsync();

            await Assert.ThrowsAsync<ComponentDisposedException>(async () => await datasource.GetClusterLeavesAsync(1, 2, 3));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_GetClusterExpansionZoom()
        {
            var datasource = new DataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            var expected = 2;
            _jsRuntimeMock.Setup(runtime => runtime.InvokeAsync<int>(It.IsAny<string>(), It.IsAny<object[]>())).ReturnsAsync(expected);

            var clusterId = 1;
            var result = await datasource.GetClusterExpansionZoomAsync(clusterId);

            Assert.Equal(expected, result);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeAsync<int>(Constants.JsConstants.Methods.Datasource.GetClusterExpansionZoom.ToDatasourceNamespace(), datasource.Id, clusterId), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetClusterExpansionZoom_NotAddedToMapCase()
        {
            var datasource = new DataSource();

            await Assert.ThrowsAsync<ComponentNotAddedToMapException>(async () => await datasource.GetClusterExpansionZoomAsync(1));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_NotGetClusterExpansionZoom_DisposedCase()
        {
            var datasource = new DataSource {
                JSRuntime = _jsRuntimeMock.Object
            };

            await datasource.DisposeAsync();

            await Assert.ThrowsAsync<ComponentDisposedException>(async () => await datasource.GetClusterExpansionZoomAsync(1));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
