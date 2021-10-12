namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(PitchAlignmentJsonConverter))]
    public struct PitchAlignment
    {
        private readonly string _pitchAlignment;

        public static readonly PitchAlignment Auto = new("auto");

        /// <summary>
        /// The circle is aligned to the plane of the map.
        /// </summary>
        public static readonly PitchAlignment Map = new("map");

        /// <summary>
        /// The circle is aligned to the plane of the viewport
        /// </summary>
        public static readonly PitchAlignment ViewPort = new("viewport");

        private PitchAlignment(string type) => _pitchAlignment = type;

        public override string ToString() => _pitchAlignment;

        /// <summary>
        /// Return a PitchAlignment corresponding to the given value
        /// </summary>
        /// <param name="pitchAlignment">Value of the PitchAlignment</param>
        /// <returns>PitchAlignment corresponding to the given value. If none was found, returns `default`</returns>
        public static PitchAlignment FromString(string pitchAlignment)
        {
            return pitchAlignment switch {
                "auto" => Auto,
                "map" => Map,
                "viewport" => ViewPort,
                _ => default,
            };
        }
    }

    internal class PitchAlignmentJsonConverter : JsonConverter<PitchAlignment>
    {
        public override PitchAlignment Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => PitchAlignment.FromString(reader.GetString());

        public override void Write(Utf8JsonWriter writer, PitchAlignment value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
