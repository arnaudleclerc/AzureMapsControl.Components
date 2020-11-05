namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(PitchAlignmentJsonConverter))]
    public sealed class PitchAlignment
    {
        private readonly string _type;

        /// <summary>
        /// The circle is aligned to the plane of the map.
        /// </summary>
        public static PitchAlignment Map = new PitchAlignment("map");

        /// <summary>
        /// The circle is aligned to the plane of the viewport
        /// </summary>
        public static PitchAlignment ViewPort = new PitchAlignment("viewport");

        private PitchAlignment(string type) => _type = type;

        public override string ToString() => _type;

        internal static PitchAlignment FromString(string type)
        {
            if (Map.ToString() == type)
            {
                return Map;
            }

            return ViewPort.ToString() == type ? ViewPort : null;
        }
    }

    internal class PitchAlignmentJsonConverter : JsonConverter<PitchAlignment>
    {
        public override PitchAlignment Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => PitchAlignment.FromString(reader.GetString());

        public override void Write(Utf8JsonWriter writer, PitchAlignment value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
