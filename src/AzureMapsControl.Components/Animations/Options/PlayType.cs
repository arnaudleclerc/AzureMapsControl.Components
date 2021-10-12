namespace AzureMapsControl.Components.Animations.Options
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(PlayTypeJsonConverter))]
    public struct PlayType
    {
        private readonly string _playType;

        public static readonly PlayType Together = new("together");
        public static readonly PlayType Sequential = new("sequential");
        public static readonly PlayType Interval = new("interval");

        private PlayType(string type) => _playType = type;

        public override string ToString() => _playType;

        /// <summary>
        /// Return a PlayType corresponding to the given value
        /// </summary>
        /// <param name="playType">Value of the PlayType</param>
        /// <returns>PlayType corresponding to the given value. If none was found, returns `default`</returns>
        public static PlayType FromString(string playType)
        {
            return playType switch {
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
