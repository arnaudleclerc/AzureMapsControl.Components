namespace AzureMapsControl.Components.Animations.Options
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(InterpolationJsonConverter))]
    public struct Interpolation
    {
        private readonly string _interpolation;

        public static readonly Interpolation Linear = new Interpolation("linear");

        public static readonly Interpolation Nearest = new Interpolation("nearest");

        public static readonly Interpolation Min = new Interpolation("min");

        public static readonly Interpolation Max = new Interpolation("max");

        public static readonly Interpolation Avg = new Interpolation("avg");

        private Interpolation(string interpolation) => _interpolation = interpolation;

        public override string ToString() => _interpolation;

        public static Interpolation FromString(string interpolation)
        {
            return interpolation switch {
                "linear" => Linear,
                "nearest" => Nearest,
                "min" => Min,
                "max" => Max,
                "avg" => Avg,
                _ => default
            };
        }
    }

    internal class InterpolationJsonConverter : JsonConverter<Interpolation>
    {
        public override Interpolation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Interpolation.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, Interpolation value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
