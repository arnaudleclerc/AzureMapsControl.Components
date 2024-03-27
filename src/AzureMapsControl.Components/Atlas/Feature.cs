﻿namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(FeatureJsonConverter))]
    public abstract class Feature
    {
        [JsonConverter(typeof(FeatureIdConverter))]
        public string Id { get; set; }

        [JsonConverter(typeof(FeaturePropertiesConverter))]
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
            get {
                if (_geometry != null && _geometry.Id != Id)
                {
                    _geometry.Id = Id;
                }
                return _geometry;
            }
            set {
                _geometry = value;
                if (_geometry != null)
                {
                    _geometry.Id = Id;
                }
            }
        }

        public Feature() : base() { }

        public Feature(TGeometry geometry) : base(Guid.NewGuid().ToString()) => Geometry = geometry;

        public Feature(string id, TGeometry geometry) : this(id, geometry, null) { }

        public Feature(TGeometry geometry, IDictionary<string, object> properties) : this(Guid.NewGuid().ToString(), geometry, properties) { }

        public Feature(string id, TGeometry geometry, IDictionary<string, object> properties) : base(id, properties) => Geometry = geometry;
    }

    internal class FeatureJsonConverter : JsonConverter<Feature>
    {
        public override Feature Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var originalDepth = reader.CurrentDepth;

            string Id = null;
            IDictionary<string, object> Properties = null;
            Geometry geometry = null;

            while (reader.TokenType != JsonTokenType.EndObject || originalDepth != reader.CurrentDepth)
            {
                reader.Read();

                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "id")
                {
                    reader.Read();
                    var converter = new FeatureIdConverter();
                    Id = converter.Read(ref reader, typeof(string), options);
                }

                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "properties")
                {
                    var converter = new FeaturePropertiesConverter();
                    Properties = converter.Read(ref reader, typeof(IDictionary<string, object>), options);
                }

            }


            return new Feature<Geometry>(Id, geometry, Properties);
        }

        public override void Write(Utf8JsonWriter writer, Feature value, JsonSerializerOptions options) => throw new NotSupportedException();


    }

    internal sealed class FeatureIdConverter : JsonConverter<string>
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

    internal sealed class FeaturePropertiesConverter : JsonConverter<IDictionary<string, object>>
    {
        public override IDictionary<string, object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
        public override void Write(Utf8JsonWriter writer, IDictionary<string, object> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var kvp in value)
            {
                writer.WritePropertyName(kvp.Key);
                if (kvp.Value.GetType() == typeof(DateTime) ||
                    (kvp.Value.GetType() == typeof(string) && DateTime.TryParse(kvp.Value.ToString(), out _)))
                {
                    writer.WriteStringValue($"azureMapsControl.datetime:{kvp.Value}");
                }
                else
                {
                    JsonSerializer.Serialize(writer, kvp.Value);
                }
            }
            writer.WriteEndObject();
        }
    }

    internal class FeatureJsonConverter<TGeometry> : JsonConverter<Feature<TGeometry>> where TGeometry : Geometry
    {
        public FeatureJsonConverter() { }

        public override Feature<TGeometry> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var originalDepth = reader.CurrentDepth;

            string Id = null;
            IDictionary<string, object> Properties = null;
            TGeometry geometry = null;

            while (reader.TokenType != JsonTokenType.EndObject || originalDepth != reader.CurrentDepth)
            {
                reader.Read();

                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "id")
                {
                    reader.Read();
                    var converter = new FeatureIdConverter();
                    Id = converter.Read(ref reader, typeof(string), options);
                }

                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "geometry")
                {
                    var converter = new GeometryJsonConverter<TGeometry>();
                    geometry = converter.Read(ref reader, typeof(TGeometry), options);
                }

                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "properties")
                {
                    var converter = new FeaturePropertiesConverter();
                    Properties = converter.Read(ref reader, typeof(IDictionary<string, object>), options);
                }

            }


            return new Feature<TGeometry>(Id, geometry, Properties);
        }

        public override void Write(Utf8JsonWriter writer, Feature<TGeometry> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("geometry");
            JsonSerializer.Serialize(writer, value.Geometry);
            writer.WriteString("id", value.Id);
            writer.WritePropertyName("properties");
            JsonSerializer.Serialize(writer, value.Properties);
            writer.WritePropertyName("bbox");
            JsonSerializer.Serialize(writer, value.BBox);
            writer.WriteEndObject();
        }

    }



}
