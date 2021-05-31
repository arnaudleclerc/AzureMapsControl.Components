namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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

        public override void Write(Utf8JsonWriter writer, Geometry value, JsonSerializerOptions options) => throw new NotSupportedException();

        internal static dynamic ReadCoordinates(ref Utf8JsonReader reader)
        {
            var startDepth = reader.CurrentDepth;
            dynamic result = null;
            dynamic coordinates = null;
            dynamic positions = null;

            while (reader.TokenType != JsonTokenType.EndArray || startDepth != reader.CurrentDepth)
            {
                reader.Read();

                if (reader.TokenType == JsonTokenType.Number)
                {
                    // The depth tells us which kind of object we are dealing with
                    // startDepth => Point
                    // startDepth + 1 => LineString / MultiPoint
                    // startDepth + 2 => MultiLineString / Polygon
                    // startDepth + 3 => Polygon

                    if (reader.CurrentDepth == startDepth + 1)
                    {
                        result = ReadPosition(ref reader);
                    }
                    else if (reader.CurrentDepth == startDepth + 2)
                    {
                        if (result == null)
                        {
                            result = new List<Position>();
                        }

                        result.Add(ReadPosition(ref reader));
                    }
                    else if (reader.CurrentDepth == startDepth + 3)
                    {
                        if (result == null)
                        {
                            result = new List<IEnumerable<Position>>();
                        }

                        if (coordinates == null)
                        {
                            coordinates = new List<Position>();
                        }

                        coordinates.Add(ReadPosition(ref reader));
                    }
                    else if (reader.CurrentDepth == startDepth + 4)
                    {
                        if (result == null)
                        {
                            result = new List<IEnumerable<IEnumerable<Position>>>();
                        }

                        if (coordinates == null)
                        {
                            coordinates = new List<IEnumerable<Position>>();
                        }

                        if (positions == null)
                        {
                            positions = new List<Position>();
                        }

                        positions.Add(ReadPosition(ref reader));
                    }
                }

                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    if (reader.CurrentDepth == startDepth + 2)
                    {
                        if (positions != null)
                        {
                            coordinates.Add(positions);
                            positions = null;
                        }
                    }
                    else if (reader.CurrentDepth == startDepth + 1)
                    {
                        if (coordinates != null)
                        {
                            result.Add(coordinates);
                            coordinates = null;
                        }
                    }
                }
            }

            return result;
        }

        internal static Position ReadPosition(ref Utf8JsonReader reader)
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

    internal class GeometryJsonConverter<TGeometry> : JsonConverter<TGeometry> where TGeometry : Geometry
    {
        public GeometryJsonConverter() { }

        public override TGeometry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                }

                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "coordinates")
                {
                    coordinates = GeometryJsonConverter.ReadCoordinates(ref reader);
                }

            }

            return type switch {
                Point.InnerGeometryType => new Point(coordinates) as TGeometry,
                LineString.InnerGeometryType => new LineString(coordinates) as TGeometry,
                MultiLineString.InnerGeometryType => new MultiLineString(coordinates) as TGeometry,
                MultiPoint.InnerGeometryType => new MultiPoint(coordinates) as TGeometry,
                MultiPolygon.InnerGeometryType => new MultiPolygon(coordinates) as TGeometry,
                Polygon.InnerGeometryType => new Polygon(coordinates) as TGeometry,
                _ => default
            };
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
