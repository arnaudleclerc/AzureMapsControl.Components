namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /**
     * Represent a pixel coordinate or offset.
     */
    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(PixelJsonConverter))]
    public sealed class Pixel
    {
        /// <summary>
        /// The horizontal pixel offset
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// The vertical pixel offset
        /// </summary>
        public double Y { get; set; }

        public Pixel() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">The horizontal pixel offset</param>
        /// <param name="y">The vertical pixel offset</param>
        public Pixel(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    internal sealed class PixelJsonConverter : JsonConverter<Pixel>
    {
        public override Pixel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType == JsonTokenType.None)
            {
                reader.Read();
            }

            var pixel = new Pixel();
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                reader.Read();
                pixel.X = reader.GetDouble();
                reader.Read();
                pixel.Y = reader.GetDouble();
                reader.Read();
            }

            return pixel;
        }

        public override void Write(Utf8JsonWriter writer, Pixel value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(value.X);
            writer.WriteNumberValue(value.Y);
            writer.WriteEndArray();
        }
    }
}
