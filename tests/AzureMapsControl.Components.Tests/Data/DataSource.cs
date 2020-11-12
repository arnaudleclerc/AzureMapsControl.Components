namespace AzureMapsControl.Components.Tests.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Data;

    using Xunit;

    public class DataSourceTests
    {
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
            var dataSource = new DataSource();
            var geometries = new List<Geometry> {
                new Point(new Position(0, 0)),
                new Point(new Position(1, 1))
            };

            var geometriesAssertResult = false;
            var idAssertResult = false;

            dataSource.AddCallback = async (idCallback, geometriesCallback) => {
                geometriesAssertResult = geometries == geometriesCallback;
                idAssertResult = dataSource.Id == idCallback;
            };

            await dataSource.AddAsync(geometries);

            Assert.True(geometriesAssertResult);
            Assert.True(idAssertResult);
            Assert.Contains(geometries[0], dataSource.Geometries);
            Assert.Contains(geometries[1], dataSource.Geometries);
        }

        [Fact]
        public async void Should_AddGeometries_Params_Async()
        {
            var dataSource = new DataSource();

            var geometriesAssertResult = false;
            var idAssertResult = false;

            var point0 = new Point(new Position(0, 0));
            var point1 = new Point(new Position(1, 1));

            dataSource.AddCallback = async (idCallback, geometriesCallback) => {
                geometriesAssertResult = geometriesCallback.Count() == 2 && geometriesCallback.Contains(point0) && geometriesCallback.Contains(point1);
                idAssertResult = dataSource.Id == idCallback;
            };

            await dataSource.AddAsync(point0, point1);

            Assert.True(geometriesAssertResult);
            Assert.True(idAssertResult);
            Assert.Contains(point0, dataSource.Geometries);
            Assert.Contains(point1, dataSource.Geometries);
        }

        [Fact]
        public async void Should_NotCallAddCallbackIfGeometriesAreEmpty_Async()
        {
            var dataSource = new DataSource();
            var callbackCalled = false;
            dataSource.AddCallback = async (idCallback, geometriesCallback) => {
                callbackCalled = true;
            };

            await dataSource.AddAsync(new List<Geometry>());
            Assert.False(callbackCalled);
        }

        [Fact]
        public async void Should_NotCallAddCallbackIfGeometriesAreNull_Async()
        {
            var dataSource = new DataSource();
            var callbackCalled = false;
            dataSource.AddCallback = async (idCallback, geometriesCallback) => {
                callbackCalled = true;
            };

            await dataSource.AddAsync(null);
            Assert.False(callbackCalled);
        }

        [Fact]
        public async void Should_ImportDataFromUrl_Async()
        {
            var dataSource = new DataSource();
            var urlAssertResult = false;
            var idAssertResult = false;

            const string url = "someurl";

            dataSource.ImportDataFromUrlCallback = async (idCallback, urlCallback) => {
                idAssertResult = idCallback == dataSource.Id;
                urlAssertResult = urlCallback == url;
            };

            await dataSource.ImportDataFromUrlAsync(url);

            Assert.True(idAssertResult);
            Assert.True(urlAssertResult);
        }

        [Fact]
        public async void Should_RemoveGeometries_Async()
        {
            var assertResult = false;
            var dataSource = new DataSource();
            var point1 = new Point("point1");
            var point2 = new Point("point2");
            var geometries = new List<Geometry> { point1, point2 };

            dataSource.AddCallback = async (id, geometriesToAdd) => { };
            dataSource.RemoveCallback = async (id, geometryIds) => {
                assertResult = id == dataSource.Id && geometryIds.Single() == point1.Id;
            };

            await dataSource.AddAsync(geometries);
            await dataSource.RemoveAsync(point1);

            Assert.True(assertResult);
            Assert.DoesNotContain(point1, dataSource.Geometries);
            Assert.Contains(point2, dataSource.Geometries);
        }

        [Fact]
        public async void Should_RemoveGeometries_EnumerableVersion_Async()
        {
            var assertResult = false;
            var dataSource = new DataSource();
            var point1 = new Point("point1");
            var point2 = new Point("point2");
            var geometries = new List<Geometry> { point1, point2 };

            dataSource.AddCallback = async (id, geometriesToAdd) => { };
            dataSource.RemoveCallback = async (id, geometryIds) => {
                assertResult = id == dataSource.Id && geometryIds.Single() == point1.Id;
            };

            await dataSource.AddAsync(geometries);
            await dataSource.RemoveAsync(new[] { point1 });

            Assert.True(assertResult);
            Assert.DoesNotContain(point1, dataSource.Geometries);
            Assert.Contains(point2, dataSource.Geometries);
        }

        [Fact]
        public async void Should_RemoveGeometries_IdsVersion_Async()
        {
            var assertResult = false;
            var dataSource = new DataSource();
            var point1 = new Point("point1");
            var point2 = new Point("point2");
            var geometries = new List<Geometry> { point1, point2 };

            dataSource.AddCallback = async (id, geometriesToAdd) => { };
            dataSource.RemoveCallback = async (id, geometryIds) => {
                assertResult = id == dataSource.Id && geometryIds.Single() == point1.Id;
            };

            await dataSource.AddAsync(geometries);
            await dataSource.RemoveAsync(point1.Id);

            Assert.True(assertResult);
            Assert.DoesNotContain(point1, dataSource.Geometries);
            Assert.Contains(point2, dataSource.Geometries);
        }

        [Fact]
        public async void Should_RemoveGeometries_IdsEnumerableVersion_Async()
        {
            var assertResult = false;
            var dataSource = new DataSource();
            var point1 = new Point("point1");
            var point2 = new Point("point2");
            var geometries = new List<Geometry> { point1, point2 };

            dataSource.AddCallback = async (id, geometriesToAdd) => { };
            dataSource.RemoveCallback = async (id, geometryIds) => {
                assertResult = id == dataSource.Id && geometryIds.Single() == point1.Id;
            };

            await dataSource.AddAsync(geometries);
            await dataSource.RemoveAsync(new List<string> { point1.Id });

            Assert.True(assertResult);
            Assert.DoesNotContain(point1, dataSource.Geometries);
            Assert.Contains(point2, dataSource.Geometries);
        }

        [Fact]
        public async void Should_NotRemoveGeometries_Async()
        {
            var assertResult = false;
            var dataSource = new DataSource();
            var point1 = new Point("point1");
            var point2 = new Point("point2");

            dataSource.AddCallback = async (id, geometriesToAdd) => { };
            dataSource.RemoveCallback = async (id, geometryIds) => {
                assertResult = false;
            };

            await dataSource.AddAsync(point2);
            await dataSource.RemoveAsync(point1);

            Assert.False(assertResult);
        }

        [Fact]
        public async void Should_NotRemoveGeometries_NullCheck_Async()
        {
            var assertResult = false;
            var dataSource = new DataSource();
            var point1 = new Point("point1");

            dataSource.AddCallback = async (id, geometriesToAdd) => { };
            dataSource.RemoveCallback = async (id, geometryIds) => {
                assertResult = false;
            };

            await dataSource.RemoveAsync(point1);

            Assert.False(assertResult);
        }

        [Fact]
        public async void Should_ClearDataSource_Async()
        {
            var assertResult = false;
            var dataSource = new DataSource();
            var point1 = new Point("point1");
            var point2 = new Point("point2");

            dataSource.AddCallback = async (id, geometriesToAdd) => { };
            dataSource.ClearCallback = async (id) => assertResult = id == dataSource.Id;

            await dataSource.AddAsync(point2);
            await dataSource.ClearAsync();

            Assert.True(assertResult);
            Assert.Null(dataSource.Geometries);
        }
    }
}
