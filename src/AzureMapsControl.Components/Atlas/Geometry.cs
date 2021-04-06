namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter))]
    public abstract class Geometry
    {
        private string _type;

        internal string Id { get; set; }

        public string Type
        {
            get => string.IsNullOrEmpty(_type) ? GetGeometryType() : _type;
            set => _type = value;
        }

        public Geometry() { }

        internal abstract string GetGeometryType();
    }

    [ExcludeFromCodeCoverage]
    public abstract class Geometry<TPosition> : Geometry
    {
        public TPosition Coordinates { get; set; }

        public Geometry() { }

        public Geometry(TPosition coordinates) : base() => Coordinates = coordinates;
    }

    internal class GeometryJsonConverter : JsonConverter<Geometry>
    {
        public override Geometry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

            switch (type)
            {
                case "Point":
                    return new Point();
                case "LineString":
                    return new LineString();
                case "MultiLineString":
                    return new MultiLineString();
                case "MultiPoint":
                    return new MultiPoint();
                case "MultiPolygon":
                    return new MultiPolygon();
                case "Polygon":
                    return new Polygon();
                default:
                    return null;
            }
        }
        public override void Write(Utf8JsonWriter writer, Geometry value, JsonSerializerOptions options) => throw new NotImplementedException();
    }
}
