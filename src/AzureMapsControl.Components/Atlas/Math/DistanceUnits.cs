namespace AzureMapsControl.Components.Atlas.Math
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Units of measurement for distances.
    /// </summary>
    [JsonConverter(typeof(DistanceUnitsJsonConverter))]
    public struct DistanceUnits
    {
        private readonly string _unit;

        /// <summary>
        /// Represents a distance in meters (m).
        /// </summary>
        public static DistanceUnits Meters = new("meters");

        /// <summary>
        /// Represents a distance in kilometers (km).
        /// </summary>
        public static DistanceUnits Kilometers = new("kilometers");

        /// <summary>
        /// Represents a distance in feet (ft).
        /// </summary>
        public static DistanceUnits Feet = new("feet");

        /// <summary>
        /// Represents a distance in miles (mi).
        /// </summary>
        public static DistanceUnits Miles = new("miles");

        /// <summary>
        /// Represents a distance in nautical miles.
        /// </summary>
        public static DistanceUnits NauticalMiles = new("nauticalMiles");

        /// <summary>
        /// Represents a distance in yards (yds).
        /// </summary>
        public static DistanceUnits Yards = new("yards");

        private DistanceUnits(string unit) => _unit = unit;

        public override string ToString() => _unit;

        /// <summary>
        /// Return a DistanceUnits corresponding to the given value
        /// </summary>
        /// <param name="unit">Value of the Interpolation</param>
        /// <returns>DistanceUnits corresponding to the given value. If none was found, returns `default`</returns>
        public static DistanceUnits FromString(string unit)
        {
            return unit switch {
                "meters" => Meters,
                "kilometers" => Kilometers,
                "feet" => Feet,
                "miles" => Miles,
                "nauticalMiles" => NauticalMiles,
                "yards" => Yards,
                _ => default
            };
        }
    }

    internal sealed class DistanceUnitsJsonConverter : JsonConverter<DistanceUnits>
    {
        public override DistanceUnits Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => DistanceUnits.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, DistanceUnits value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
