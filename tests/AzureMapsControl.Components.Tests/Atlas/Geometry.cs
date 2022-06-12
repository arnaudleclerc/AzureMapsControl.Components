namespace AzureMapsControl.Components.Tests.Atlas
{
    using System.Text.Json;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;


    public class FeatureJsonConverterTests : JsonConverterTests<Feature>
    {
        public FeatureJsonConverterTests() : base(new FeatureJsonConverter()) { }

        [Fact]
        public void Should_ReadFeature()
        {
            var feature = new Feature<Polygon>("adsf-asfda", new Polygon() {
                GeometryType = "Polygon",
                Coordinates = new[] {
                    new [] {
                        new Position(0, 1),
                        new Position(2, 3)
                    }
                },
                Id = "d93d484e-b60e-4ef9-afec-977c09344370",


            });
            var json = JsonSerializer.Serialize(feature);
            var result = Read(json);
            Assert.IsType<Feature<Polygon>>(result);
            Assert.Equal(json, JsonSerializer.Serialize(result));
        }

    }



    public class GeometryJsonConverterTests : JsonConverterTests<Geometry>
    {
        public GeometryJsonConverterTests() : base(new GeometryJsonConverter()) { }

        [Fact]
        public void Should_ReadPoint()
        {
            var point = new Point(new Position(0, 1));
            var json = JsonSerializer.Serialize(point);
            var result = Read(json);
            Assert.IsType<Point>(result);
            Assert.Equal(json, JsonSerializer.Serialize(result as Point));
        }

        [Fact]
        public void Should_ReadLineString()
        {
            var lineString = new LineString(new[] { new Position(0, 1), new Position(2, 3) });
            var json = JsonSerializer.Serialize(lineString);
            var result = Read(json);
            Assert.IsType<LineString>(result);
            Assert.Equal(json, JsonSerializer.Serialize(result as LineString));
        }

        [Fact]
        public void Should_ReadMultiLineString()
        {
            var multiLineString = new MultiLineString(new[] {
                new [] {
                    new Position(0, 1),
                    new Position(2,3)
                },
                new [] {
                    new Position(4,5),
                    new Position(6,7)
                }
            });
            var json = JsonSerializer.Serialize(multiLineString);
            var result = Read(json);
            Assert.IsType<MultiLineString>(result);
            Assert.Equal(json, JsonSerializer.Serialize(result as MultiLineString));
        }

        [Fact]
        public void Should_ReadMultiPoint()
        {
            var multiPoint = new MultiPoint(new[] { new Position(0, 1), new Position(2, 3) });
            var json = JsonSerializer.Serialize(multiPoint);
            var result = Read(json);
            Assert.IsType<MultiPoint>(result);
            Assert.Equal(json, JsonSerializer.Serialize(result as MultiPoint));
        }

        [Fact]
        public void Should_ReadMultiPolygon()
        {
            var multiPolygon = new MultiPolygon(new[] {
                new [] {
                    new [] {
                        new Position(0, 1),
                        new Position(2, 3)
                    }
                }
            });
            var json = JsonSerializer.Serialize(multiPolygon);
            var result = Read(json);
            Assert.IsType<MultiPolygon>(result);
            Assert.Equal(json, JsonSerializer.Serialize(result as MultiPolygon));
        }

        [Fact]
        public void Should_ReadPolygon()
        {
            var polygon = new Polygon(new[] {
                    new [] {
                        new Position(0, 1),
                        new Position(2, 3)
                    }
                });
            var json = JsonSerializer.Serialize(polygon);
            var result = Read(json);
            Assert.IsType<Polygon>(result);
            Assert.Equal(json, JsonSerializer.Serialize(result as Polygon));
        }
    }

    public class GeometryPointJsonConverterTests : JsonConverterTests<Point>
    {
        public GeometryPointJsonConverterTests() : base(new GeometryJsonConverter<Point>()) { }

        [Fact]
        public void Should_Read()
        {
            var geometry = new Point(new Position(0, 1));
            var json = JsonSerializer.Serialize(geometry);
            var result = Read(json);
            Assert.Equal(json, JsonSerializer.Serialize(result));
        }

        [Fact]
        public void Should_Write()
        {
            var geometry = new Point(new Position(0, 1));
            TestAndAssertWrite(geometry, JsonSerializer.Serialize(geometry));
        }
    }

    public class GeometryLineStringJsonConverterTests : JsonConverterTests<LineString>
    {
        public GeometryLineStringJsonConverterTests() : base(new GeometryJsonConverter<LineString>()) { }

        [Fact]
        public void Should_Read()
        {
            var geometry = new LineString(new[] { new Position(0, 1), new Position(2, 3) });
            var json = JsonSerializer.Serialize(geometry);
            var result = Read(json);
            Assert.Equal(json, JsonSerializer.Serialize(result));
        }

        [Fact]
        public void Should_Write()
        {
            var geometry = new LineString(new[] { new Position(0, 1), new Position(2, 3) });
            TestAndAssertWrite(geometry, JsonSerializer.Serialize(geometry));
        }
    }

    public class GeometryMultiLineStringJsonConverterTests : JsonConverterTests<MultiLineString>
    {
        public GeometryMultiLineStringJsonConverterTests() : base(new GeometryJsonConverter<MultiLineString>()) { }

        [Fact]
        public void Should_Read()
        {
            var geometry = new MultiLineString(new[] {
                new [] {
                    new Position(0, 1),
                    new Position(2,3)
                },
                new [] {
                    new Position(4,5),
                    new Position(6,7)
                }
            });
            var json = JsonSerializer.Serialize(geometry);
            var result = Read(json);
            Assert.Equal(json, JsonSerializer.Serialize(result));
        }

        [Fact]
        public void Should_Write()
        {
            var geometry = new MultiLineString(new[] {
                new [] {
                    new Position(0, 1),
                    new Position(2,3)
                },
                new [] {
                    new Position(4,5),
                    new Position(6,7)
                }
            });
            TestAndAssertWrite(geometry, JsonSerializer.Serialize(geometry));
        }
    }

    public class GeometryMultiPointJsonConverterTests : JsonConverterTests<MultiPoint>
    {
        public GeometryMultiPointJsonConverterTests() : base(new GeometryJsonConverter<MultiPoint>()) { }

        [Fact]
        public void Should_Read()
        {
            var geometry = new MultiPoint(new[] { new Position(0, 1), new Position(2, 3) });
            var json = JsonSerializer.Serialize(geometry);
            var result = Read(json);
            Assert.Equal(json, JsonSerializer.Serialize(result));
        }

        [Fact]
        public void Should_Write()
        {
            var geometry = new MultiPoint(new[] { new Position(0, 1), new Position(2, 3) });
            TestAndAssertWrite(geometry, JsonSerializer.Serialize(geometry));
        }
    }

    public class GeometryMultiPolygonJsonConverterTests : JsonConverterTests<MultiPolygon>
    {
        public GeometryMultiPolygonJsonConverterTests() : base(new GeometryJsonConverter<MultiPolygon>()) { }

        [Fact]
        public void Should_Read()
        {
            var geometry = new MultiPolygon(new[] {
                new [] {
                    new [] {
                        new Position(0, 1),
                        new Position(2, 3)
                    }
                }
            });
            var json = JsonSerializer.Serialize(geometry);
            var result = Read(json);
            Assert.Equal(json, JsonSerializer.Serialize(result));
        }

        [Fact]
        public void Should_Write()
        {
            var geometry = new MultiPolygon(new[] {
                new [] {
                    new [] {
                        new Position(0, 1),
                        new Position(2, 3)
                    }
                }
            });
            TestAndAssertWrite(geometry, JsonSerializer.Serialize(geometry));
        }
    }

    public class GeometryPolygonJsonConverterTests : JsonConverterTests<Polygon>
    {
        public GeometryPolygonJsonConverterTests() : base(new GeometryJsonConverter<Polygon>()) { }

        [Fact]
        public void Should_Read()
        {
            var geometry = new Polygon(new[] {
                    new [] {
                        new Position(0, 1),
                        new Position(2, 3)
                    }
                });
            var json = JsonSerializer.Serialize(geometry);
            var result = Read(json);
            Assert.Equal(json, JsonSerializer.Serialize(result));
        }

        [Fact]
        public void Should_Write()
        {
            var geometry = new Polygon(new[] {
                    new [] {
                        new Position(0, 1),
                        new Position(2, 3)
                    }
                });
            TestAndAssertWrite(geometry, JsonSerializer.Serialize(geometry));
        }
    }
}
