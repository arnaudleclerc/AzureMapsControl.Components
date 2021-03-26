namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    public abstract class Feature
    {
        [JsonConverter(typeof(StringConverter))]
        public string Id { get; set; }

        public IDictionary<string, object> Properties { get; set; }
        public BoundingBox BBox { get; set; }

        public Feature() { }

        public Feature(string id) => Id = id;

        public Feature(string id, IDictionary<string, object> properties) : this(id) => Properties = properties;
    }

    [ExcludeFromCodeCoverage]
    public sealed class Feature<TGeometry> : Feature
        where TGeometry : Geometry
    {
        private TGeometry _geometry;

        public TGeometry Geometry
        {
            get 
            {
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

        public Feature() : base() { }

        public Feature(TGeometry geometry) : base(Guid.NewGuid().ToString()) => Geometry = geometry;

        public Feature(string id, TGeometry geometry) : this(id, geometry, null) { }

        public Feature(TGeometry geometry, IDictionary<string, object> properties) : this(Guid.NewGuid().ToString(), geometry, properties) { }

        public Feature(string id, TGeometry geometry, IDictionary<string, object> properties) : base(id, properties) => Geometry = geometry;
    }

    internal sealed class StringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch {
                JsonTokenType.Number => reader.TryGetUInt64(out var value) ? value.ToString() : throw new JsonException($"Invalid number."),
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.None => null,
                JsonTokenType.Null => null,
                _ => throw new JsonException($"Cannot convert from {reader.TokenType}.")
            };
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) => writer.WriteStringValue(value);
    }
}
