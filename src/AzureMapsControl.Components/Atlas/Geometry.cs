namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter))]
    public abstract class Geometry
    {
        internal string Id { get; set; }

        internal string GeometryType { get; set; }

        public string Type => GeometryType;

        public Geometry() { }
    }

    [ExcludeFromCodeCoverage]
    public abstract class Geometry<TPosition> : Geometry
    {
        public TPosition Coordinates { get; set; }

        public Geometry() : base() { }

        internal Geometry(string type) : this() => GeometryType = type;

        internal Geometry(TPosition coordinates, string type) : this(type) => Coordinates = coordinates;
    }

    internal class GeometryJsonConverter : JsonConverter<Geometry>
    {
        public override Geometry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var originalDepth = reader.CurrentDepth;

            string type = null;
            dynamic coordinates = null;

            while (reader.TokenType != JsonTokenType.EndObject || originalDepth != reader.CurrentDepth)
            {
                reader.Read();

                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "type")
                {
                    reader.Read();
                    type = reader.GetString();
                    Console.WriteLine(type);
                }

                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "coordinates")
                {
                    coordinates = ReadCoordinates(ref reader);
                }

            }

            return type switch {
                Point.InnerGeometryType => new Point(coordinates),
                LineString.InnerGeometryType => new LineString(coordinates),
                MultiLineString.InnerGeometryType => new MultiLineString(coordinates),
                MultiPoint.InnerGeometryType => new MultiPoint(coordinates),
                MultiPolygon.InnerGeometryType => new MultiPolygon(coordinates),
                Polygon.InnerGeometryType => new Polygon(coordinates),
                _ => default
            };
        }

        public override void Write(Utf8JsonWriter writer, Geometry value, JsonSerializerOptions options) => throw new NotImplementedException();

        internal dynamic ReadCoordinates(ref Utf8JsonReader reader)
        {
            var startDepth = reader.CurrentDepth;
            Console.WriteLine($"1 - Reading coordinates at depth {startDepth}");
            dynamic result = null;
            do
            {
                Console.WriteLine($"2 - Reading depth {reader.CurrentDepth}");
                reader.Read();
                Console.WriteLine(reader.TokenType);
                Console.WriteLine(reader.CurrentDepth);

                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    result = ReadCoordinatesArray(ref reader, startDepth);
                }
            }
            while (reader.TokenType != JsonTokenType.EndArray && startDepth != reader.CurrentDepth);
            return result;
        }

        private dynamic ReadCoordinatesArray(ref Utf8JsonReader reader, int startDepth)
        {
            dynamic result = null;
            Console.WriteLine($"3 - Found array at depth {reader.CurrentDepth}");
            reader.Read();
            Console.WriteLine(reader.TokenType);
            Console.WriteLine(reader.CurrentDepth);

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                result = ReadCoordinatesArray(ref reader, startDepth);
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                Console.WriteLine($"4 - Found number at depth {reader.CurrentDepth}");
                if (reader.CurrentDepth == startDepth + 1)
                {
                    Console.WriteLine("Point case");
                    result = ReadPosition(ref reader);
                }
                else if (reader.CurrentDepth == startDepth + 2)
                {
                    Console.WriteLine("LineString and multipoint case");
                    result = new List<Position>();

                    do
                    {
                        Console.WriteLine("Adding position to line string or multi point");

                        if (reader.TokenType == JsonTokenType.StartArray)
                        {
                            Console.WriteLine("5");
                            reader.Read();
                            Console.WriteLine(reader.TokenType);
                            Console.WriteLine(reader.CurrentDepth);
                        }

                        result.Add(ReadPosition(ref reader));

                        Console.WriteLine("6");
                        reader.Read();
                        Console.WriteLine(reader.TokenType);
                        Console.WriteLine(reader.CurrentDepth);
                    } while (reader.TokenType == JsonTokenType.StartArray && reader.CurrentDepth == startDepth + 1);
                }
            }

            return result;
        }

        private Position ReadPosition(ref Utf8JsonReader reader)
        {
            var result = new Position();
            result.Longitude = reader.GetDouble();
            reader.Read();
            result.Latitude = reader.GetDouble();
            reader.Read();
            if (reader.TokenType == JsonTokenType.Number)
            {
                result.Elevation = reader.GetInt32();
                reader.Read();
            }
            return result;
        }
    }

    internal class GeometryJsonConverter<TGeometry> : JsonConverter<TGeometry> where TGeometry : Geometry, new()
    {
        public GeometryJsonConverter() { }

        public override TGeometry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var originalDepth = reader.CurrentDepth;

            string type = null;

            while (reader.TokenType != JsonTokenType.EndObject || originalDepth != reader.CurrentDepth)
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "type")
                {
                    reader.Read();
                    type = reader.GetString();
                    Console.WriteLine(type);
                }
            }

            var geometry = new TGeometry {
                GeometryType = type
            };
            return geometry;
        }

        public override void Write(Utf8JsonWriter writer, TGeometry value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", value.Type);
            switch (value.Type)
            {
                case Point.InnerGeometryType:
                    WritePoint(writer, value as Point);
                    break;
                case LineString.InnerGeometryType:
                    WriteLineString(writer, value as LineString);
                    break;
                case MultiLineString.InnerGeometryType:
                    WriteMultiLineString(writer, value as MultiLineString);
                    break;
                case MultiPoint.InnerGeometryType:
                    WriteMultiPoint(writer, value as MultiPoint);
                    break;
                case MultiPolygon.InnerGeometryType:
                    WriteMultiPolygon(writer, value as MultiPolygon);
                    break;
                case Polygon.InnerGeometryType:
                    WritePolygon(writer, value as Polygon);
                    break;
            }
            writer.WriteEndObject();
        }

        private void WritePoint(Utf8JsonWriter writer, Point value)
        {
            writer.WritePropertyName("coordinates");
            JsonSerializer.Serialize(writer, value.Coordinates);
            if (value is RoutePoint routePoint)
            {
                writer.WritePropertyName("timestamp");
                JsonSerializer.Serialize(writer, routePoint.Timestamp);
            }
        }

        private void WriteLineString(Utf8JsonWriter writer, LineString value)
        {
            writer.WritePropertyName("coordinates");
            JsonSerializer.Serialize(writer, value.Coordinates);
            WriteBoundingBox(writer, value.BBox);
        }

        private void WriteMultiLineString(Utf8JsonWriter writer, MultiLineString value)
        {
            writer.WritePropertyName("coordinates");
            JsonSerializer.Serialize(writer, value.Coordinates);
            WriteBoundingBox(writer, value.BBox);
        }

        private void WriteMultiPoint(Utf8JsonWriter writer, MultiPoint value)
        {
            writer.WritePropertyName("coordinates");
            JsonSerializer.Serialize(writer, value.Coordinates);
            WriteBoundingBox(writer, value.BBox);
        }

        private void WriteMultiPolygon(Utf8JsonWriter writer, MultiPolygon value)
        {
            writer.WritePropertyName("coordinates");
            JsonSerializer.Serialize(writer, value.Coordinates);
            WriteBoundingBox(writer, value.BBox);
        }

        private void WritePolygon(Utf8JsonWriter writer, Polygon value)
        {
            writer.WritePropertyName("coordinates");
            JsonSerializer.Serialize(writer, value.Coordinates);
            WriteBoundingBox(writer, value.BBox);
        }

        private void WriteBoundingBox(Utf8JsonWriter writer, BoundingBox bbox)
        {
            writer.WritePropertyName("bbox");
            JsonSerializer.Serialize(writer, bbox);
        }
    }
}
