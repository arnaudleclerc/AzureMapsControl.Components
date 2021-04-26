namespace AzureMapsControl.Components.Animations.Options
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(PlayTypeJsonConverter))]
    public struct PlayType
    {
        private readonly string _type;

        public static readonly PlayType Together = new PlayType("together");
        public static readonly PlayType Sequential = new PlayType("sequential");
        public static readonly PlayType Interval = new PlayType("interval");

        private PlayType(string type) => _type = type;

        public override string ToString() => _type;

        internal static PlayType FromString(string type)
        {
            return type switch {
                "together" => Together,
                "sequential" => Sequential,
                "interval" => Interval,
                _ => default
            };
        }
    }

    internal class PlayTypeJsonConverter : JsonConverter<PlayType>
    {
        public override PlayType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => PlayType.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, PlayType value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
