namespace AzureMapsControl.Components.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;
    using AzureMapsControl.Components.Runtime;

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
            Assert.Null(dataSource.Geometries);
        }

        [Fact]
        public void Should_HaveIdAffected()
        {
            const string id = "id";
            var dataSource = new DataSource(id);
            Assert.Equal(id, dataSource.Id);
            Assert.Null(dataSource.Geometries);
        }

        [Fact]
        public async void Should_AddGeometries_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            var geometries = new List<Geometry> {
                new Point(new Position(0, 0)),
                new Point(new Position(1, 1))
            };

            await dataSource.AddAsync(geometries);

            Assert.Contains(geometries[0], dataSource.Geometries);
            Assert.Contains(geometries[1], dataSource.Geometries);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Geometry>
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
                new Feature<Polygon>()
            };

            await dataSource.AddAsync(features);

            Assert.Contains(features[0], dataSource.Features);
            Assert.Contains(features[1], dataSource.Features);
            Assert.Contains(features[2], dataSource.Features);
            Assert.Contains(features[3], dataSource.Features);
            Assert.Contains(features[4], dataSource.Features);
            Assert.Contains(features[5], dataSource.Features);

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
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Exactly(6));

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

            await dataSource.AddAsync(point, lineString, multiLineString, multiPoint, multiPolygon, polygon);

            Assert.Contains(point, dataSource.Features);
            Assert.Contains(lineString, dataSource.Features);
            Assert.Contains(multiLineString, dataSource.Features);
            Assert.Contains(multiPoint, dataSource.Features);
            Assert.Contains(multiPolygon, dataSource.Features);
            Assert.Contains(polygon, dataSource.Features);

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
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.AddFeatures.ToSourceNamespace(), It.IsAny<object[]>()), Times.Exactly(6));

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_AddGeometries_Params_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            var point0 = new Point(new Position(0, 0));
            var point1 = new Point(new Position(1, 1));

            await dataSource.AddAsync(point0, point1);

            Assert.Contains(point0, dataSource.Geometries);
            Assert.Contains(point1, dataSource.Geometries);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), It.Is<object[]>(parameters =>
                parameters[0] as string == dataSource.Id
                && parameters[1] is IEnumerable<Point>
            )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotCallAddCallbackIfGeometriesAreEmpty_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            await dataSource.AddAsync(new List<Geometry>());
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotCallAddCallbackIfGeometriesAreNull_Async()
        {
            IEnumerable<Geometry> geometries = null;
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };

            await dataSource.AddAsync(geometries);
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
        public async void Should_RemoveGeometries_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Point("point1");
            var point2 = new Point("point2");
            var geometries = new List<Geometry> { point1, point2 };

            await dataSource.AddAsync(geometries);
            await dataSource.RemoveAsync(point1);

            Assert.DoesNotContain(point1, dataSource.Geometries);
            Assert.Contains(point2, dataSource.Geometries);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveGeometries_EnumerableVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Point("point1");
            var point2 = new Point("point2");
            var geometries = new List<Geometry> { point1, point2 };

            await dataSource.AddAsync(geometries);
            await dataSource.RemoveAsync(new[] { point1 });

            Assert.DoesNotContain(point1, dataSource.Geometries);
            Assert.Contains(point2, dataSource.Geometries);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveGeometries_IdsVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Point("point1");
            var point2 = new Point("point2");
            var geometries = new List<Geometry> { point1, point2 };

            await dataSource.AddAsync(geometries);
            await dataSource.RemoveAsync(point1.Id);

            Assert.DoesNotContain(point1, dataSource.Geometries);
            Assert.Contains(point2, dataSource.Geometries);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_RemoveGeometries_IdsEnumerableVersion_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Point("point1");
            var point2 = new Point("point2");
            var geometries = new List<Geometry> { point1, point2 };

            await dataSource.AddAsync(geometries);
            await dataSource.RemoveAsync(new List<string> { point1.Id });

            Assert.DoesNotContain(point1, dataSource.Geometries);
            Assert.Contains(point2, dataSource.Geometries);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Remove.ToSourceNamespace(), It.Is<object[]>(
                parameters => parameters[0] as string == dataSource.Id
                && (parameters[1] as IEnumerable<string>).Single() == point1.Id
                )), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemoveGeometries_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Point("point1");
            var point2 = new Point("point2");

            await dataSource.AddAsync(point2);
            await dataSource.RemoveAsync(point1);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_NotRemoveGeometries_NullCheck_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point1 = new Point("point1");

            await dataSource.RemoveAsync(point1);

            _jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void Should_ClearDataSource_Async()
        {
            var dataSource = new DataSource() { JSRuntime = _jsRuntimeMock.Object };
            var point2 = new Point("point2");

            await dataSource.AddAsync(point2);
            await dataSource.ClearAsync();

            Assert.Null(dataSource.Geometries);

            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Add.ToSourceNamespace(), It.IsAny<object[]>()), Times.Once);
            _jsRuntimeMock.Verify(runtime => runtime.InvokeVoidAsync(Constants.JsConstants.Methods.Source.Clear.ToSourceNamespace(), dataSource.Id), Times.Once);
            _jsRuntimeMock.VerifyNoOtherCalls();
        }
    }
}
