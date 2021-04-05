namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    public abstract class Shape
    {
        public string Id { get; set; }
        public IDictionary<string, object> Properties { get; set; }

        public Shape() { }

        public Shape(string id) => Id = id;

        public Shape(string id, IDictionary<string, object> properties) : this(id) => Properties = properties;
    }

    [ExcludeFromCodeCoverage]
    public class Shape<TGeometry> : Shape
        where TGeometry : Geometry
    {
        private TGeometry _geometry;

        public TGeometry Geometry
        {
            get {
                if (_geometry != null && _geometry.Id != Id)
                {
                    _geometry.Id = Id;
                }
                return _geometry;
            }
            set {
                _geometry = value;
                _geometry.Id = Id;
            }
        }

        public Shape(string id, TGeometry geometry) : base(id) => Geometry = geometry;
        public Shape(TGeometry geometry) : this(Guid.NewGuid().ToString(), geometry) { }
        public Shape(TGeometry geometry, IDictionary<string, object> properties) : this(Guid.NewGuid().ToString(), geometry, properties) { }
        public Shape(string id, TGeometry geometry, IDictionary<string, object> properties) : base(id, properties) => Geometry = geometry;
    }

    internal class ShapeJsonConverter : JsonConverter<Shape>
    {
        public override Shape Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //TODO : Implement deserialization of shapes here
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Shape value, JsonSerializerOptions options) => JsonSerializer.Serialize(writer, value, value.GetType());
    }
}
