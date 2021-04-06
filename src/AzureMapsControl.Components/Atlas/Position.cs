namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(PositionJsonConverter))]
    public sealed class Position
    {
        /// <summary>
        /// The position's elevation.
        /// </summary>
        public int? Elevation { get; set; }

        /// <summary>
        /// The position's latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The position's longitude.
        /// </summary>
        public double Longitude { get; set; }

        public Position() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="longitude">The position's longitude.</param>
        /// <param name="latitude">The position's latitude.</param>
        public Position(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="longitude">The position's longitude.</param>
        /// <param name="latitude">The position's latitude.</param>
        /// <param name="elevation">The position's elevation.</param>
        public Position(double longitude, double latitude, int elevation) : this(longitude, latitude) => Elevation = elevation;
    }

    internal sealed class PositionJsonConverter : JsonConverter<Position>
    {
        public override Position Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var position = new Position();
                reader.Read();
                position.Longitude = reader.GetDouble();
                reader.Read();
                position.Latitude = reader.GetDouble();
                reader.Read();
                if (reader.TokenType == JsonTokenType.Number)
                {
                    position.Elevation = reader.GetInt32();
                    reader.Read();
                }
                return position;
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, Position value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(value.Longitude);
            writer.WriteNumberValue(value.Latitude);
            if (value.Elevation.HasValue)
            {
                writer.WriteNumberValue(value.Elevation.Value);
            }
            writer.WriteEndArray();
        }
    }
}
