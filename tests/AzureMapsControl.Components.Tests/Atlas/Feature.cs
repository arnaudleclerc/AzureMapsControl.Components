namespace AzureMapsControl.Components.Tests.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Tests.Json;

    using Xunit;
    public class FeaturePolygonJsonConverterTests : JsonConverterTests<Feature<Polygon>>
    {
        JsonSerializerOptions _jsonSerializerOptions;
        public FeaturePolygonJsonConverterTests() : base(new FeatureJsonConverter<Polygon>())
        {
            _jsonSerializerOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [Fact]
        public void Should_ReadFeature()
        {
            var feature = new Feature<Polygon>("b9d9f4b7-e5cc-42da-8b22-7bb36fe9fa23", new Polygon() {
                GeometryType = "Polygon",
                Coordinates = new[] {
                    new [] {
                        new Position(0, 1),
                        new Position(2, 3)
                    }
                },
                Id = "abcd-abcd-abcd-abcd-abcd"
            }, new Dictionary<string, object> { { "_azureMapsShapeId", "efgh-efgh-efgh-efgh-efgh" } });
            var json = JsonSerializer.Serialize(feature, _jsonSerializerOptions);
            var result = Read(json);
            Assert.IsType<Feature<Polygon>>(result);
            Assert.Equal(json, JsonSerializer.Serialize(result, _jsonSerializerOptions));
        }

        [Fact]
        public void Should_Write()
        {
            var geometry = new Feature<Polygon>("b9d9f4b7-e5cc-42da-8b22-7bb36fe9fa23", new Polygon() {
                GeometryType = "Polygon",
                Coordinates = new[] {
                    new [] {
                        new Position(0, 1),
                        new Position(2, 3)
                    }
                },
                Id = "abcd-abcd-abcd-abcd-abcd"
            }, new Dictionary<string, object> { { "_azureMapsShapeId", "efgh-efgh-efgh-efgh-efgh" } });
            TestAndAssertWrite(geometry, JsonSerializer.Serialize(geometry, _jsonSerializerOptions));
        }

    }

    public class FeaturePointJsonConverterTests : JsonConverterTests<Feature<Point>>
    {
        JsonSerializerOptions _jsonSerializerOptions;
        public FeaturePointJsonConverterTests() : base(new FeatureJsonConverter<Point>())
        {
            _jsonSerializerOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [Fact]
        public void Should_ReadFeature()
        {
            var feature = new Feature<Point>("b9d9f4b7-e5cc-42da-8b22-7bb36fe9fa23", new Point() {
                GeometryType = "Point",
                Coordinates = new Position(0, 1),
                Id = "abcd-abcd-abcd-abcd-abcd"
            }, new Dictionary<string, object> { { "_azureMapsShapeId", "efgh-efgh-efgh-efgh-efgh" } });
            var json = JsonSerializer.Serialize(feature, _jsonSerializerOptions);
            var result = Read(json);
            Assert.IsType<Feature<Point>>(result);
            Assert.Equal(json, JsonSerializer.Serialize(result, _jsonSerializerOptions));
        }

        [Fact]
        public void Should_Write()
        {
            var feature = new Feature<Point>("b9d9f4b7-e5cc-42da-8b22-7bb36fe9fa23", new Point() {
                GeometryType = "Point",
                Coordinates = new Position(0, 1),
                Id = "abcd-abcd-abcd-abcd-abcd"
            }, new Dictionary<string, object> { { "_azureMapsShapeId", "efgh-efgh-efgh-efgh-efgh" } });
            TestAndAssertWrite(feature, JsonSerializer.Serialize(feature, _jsonSerializerOptions));
        }

    }
}
