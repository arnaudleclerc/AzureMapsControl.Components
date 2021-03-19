namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Bearing of the map (rotation)
    /// </summary>
    [JsonConverter(typeof(BearingJsonConverter))]
    public struct Bearing
    {
        internal int Degrees { get; }

        public static Bearing North = new Bearing(0);
        public static Bearing East = new Bearing(90);
        public static Bearing South = new Bearing(180);
        public static Bearing West = new Bearing(270);

        private Bearing(int degrees) => Degrees = degrees;

        public static Bearing FromDegrees(int degrees)
        {
            switch (degrees)
            {

                case 0:
                    return North;

                case 90:
                    return East;

                case 180:
                    return South;

                case 270:
                    return West;

                default:
                    return default;
            }
        }
    }

    internal sealed class BearingJsonConverter : JsonConverter<Bearing>
    {
        public override Bearing Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Bearing.FromDegrees(reader.GetInt32());
        public override void Write(Utf8JsonWriter writer, Bearing value, JsonSerializerOptions options) => writer.WriteNumberValue(value.Degrees);
    }
}
