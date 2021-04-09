namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    public abstract class Geometry
    {
        internal string Id { get; set; }

        internal string Type { get; set; }

        public Geometry() { }
    }

    [ExcludeFromCodeCoverage]
    public abstract class Geometry<TPosition> : Geometry
    {
        public TPosition Coordinates { get; set; }

        public Geometry() : base() { }

        internal Geometry(string type) : this() => Type = type;

        internal Geometry(TPosition coordinates, string type) : this(type) => Coordinates = coordinates;
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
                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "type")
                {
                    reader.Read();
                    type = reader.GetString();
                }
            }

            var geometry = new TGeometry {
                Type = type
            };
            return geometry;
        }

        public override void Write(Utf8JsonWriter writer, TGeometry value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", value.Type);
            switch (value.Type)
            {
                case Point.GeometryType:
                    WritePoint(writer, value as Point);
                    break;
                case LineString.GeometryType:
                    WriteLineString(writer, value as LineString);
                    break;
                case MultiLineString.GeometryType:
                    WriteMultiLineString(writer, value as MultiLineString);
                    break;
                case MultiPoint.GeometryType:
                    WriteMultiPoint(writer, value as MultiPoint);
                    break;
                case MultiPolygon.GeometryType:
                    WriteMultiPolygon(writer, value as MultiPolygon);
                    break;
                case Polygon.GeometryType:
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
