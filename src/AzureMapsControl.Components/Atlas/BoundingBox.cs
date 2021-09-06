namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(BoundingBoxJsonConverter))]
    public sealed class BoundingBox
    {
        /// <summary>
        /// The east edge of the bounding box.
        /// </summary>
        public double East { get; set; }

        /// <summary>
        /// The north edge of the bounding box.
        /// </summary>
        public double North { get; set; }

        /// <summary>
        /// The south edge of the bounding box.
        /// </summary>
        public double South { get; set; }

        /// <summary>
        /// The west edge of the bounding box.
        /// </summary>
        public double West { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="west">The west edge of the bounding box.</param>
        /// <param name="south">The south edge of the bounding box.</param>
        /// <param name="east">The east edge of the bounding box.</param>
        /// <param name="north">The north edge of the bounding box.</param>
        public BoundingBox(double west, double south, double east, double north)
        {
            West = west;
            South = south;
            East = east;
            North = north;
        }
    }

    internal sealed class BoundingBoxJsonConverter : JsonConverter<BoundingBox>
    {
        public override BoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.None)
            {
                reader.Read();
            }

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                reader.Read();
                var west = reader.GetDouble();
                reader.Read();
                var south = reader.GetDouble();
                reader.Read();
                var east = reader.GetDouble();
                reader.Read();
                var north = reader.GetDouble();
                reader.Read();
                return new BoundingBox(west, south, east, north);
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
        {
            if (value is not null)
            {
                writer.WriteStartArray();
                writer.WriteNumberValue(value.West);
                writer.WriteNumberValue(value.South);
                writer.WriteNumberValue(value.East);
                writer.WriteNumberValue(value.North);
                writer.WriteEndArray();
            }
        }
    }
}
