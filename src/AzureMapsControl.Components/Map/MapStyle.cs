namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The map styles
    /// </summary>
    [JsonConverter(typeof(MapStyleJsonConverter))]
    public sealed class MapStyle
    {
        private readonly string _style;

        /// <summary>
        /// The blank style provides a blank canvas for visualizing data
        /// </summary>
        public static readonly MapStyle Blank = new MapStyle("blank");

        /// <summary>
        /// The blank_accessible map styles provides a blank canvas for visualizing data. 
        /// The blank_accessible style will continue to provide screen reader updates with map's location details, even though the base map isn't displayed.
        /// </summary>
        public static readonly MapStyle BlankAccessible = new MapStyle("blank_accessible");

        /// <summary>
        /// The satellite style is a combination of satellite and aerial imagery.
        /// </summary>
        public static readonly MapStyle Satellite = new MapStyle("satellite");

        /// <summary>
        /// This map style is a hybrid of roads and labels overlaid on top of satellite and aerial imagery.
        /// </summary>
        public static readonly MapStyle SatelliteRoadLabels = new MapStyle("satellite_road_labels");

        /// <summary>
        /// grayscale dark is a dark version of the road map style.
        /// </summary>
        public static readonly MapStyle GrayscaleDark = new MapStyle("grayscale_dark");

        /// <summary>
        /// grayscale light is a light version of the road map style.
        /// </summary>
        public static readonly MapStyle GrayscaleLight = new MapStyle("grayscale_light");

        /// <summary>
        /// night is a dark version of the road map style with colored roads and symbols.
        /// </summary>
        public static readonly MapStyle Night = new MapStyle("night");

        /// <summary>
        /// road shaded relief is an Azure Maps main style completed with contours of the Earth.
        /// </summary>
        public static readonly MapStyle RoadShadedRelief = new MapStyle("road_shaded_relief");

        /// <summary>
        /// high_contrast_dark is a dark map style with a higher contrast than the other styles.
        /// </summary>
        public static readonly MapStyle HighContrastDark = new MapStyle("high_contrast_dark");

        private MapStyle(string style) => _style = style;

        /// <summary>
        /// Returns an enumerable containing all types of map style
        /// </summary>
        /// <returns>Enumerable containing all types of map style</returns>
        public static IEnumerable<MapStyle> All() => new[] {
            Blank,
            BlankAccessible,
            Satellite,
            SatelliteRoadLabels,
            GrayscaleDark,
            GrayscaleLight,
            Night,
            RoadShadedRelief,
            HighContrastDark
        };

        public override string ToString() => _style;

        internal static MapStyle FromString(string value)
        {
            switch (value)
            {
                case "blank":
                    return Blank;
                case "blank_accessible":
                    return BlankAccessible;
                case "satellite":
                    return Satellite;
                case "satellite_road_labels":
                    return SatelliteRoadLabels;
                case "grayscale_dark":
                    return GrayscaleDark;
                case "grayscale_light":
                    return GrayscaleLight;
                case "night":
                    return Night;
                case "road_shaded_relief":
                    return RoadShadedRelief;
                case "high_contrast_dark":
                    return HighContrastDark;
                default:
                    return null;
            }
        }
    }

    internal class MapStyleJsonConverter : JsonConverter<MapStyle>
    {
        public override MapStyle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => MapStyle.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, MapStyle value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
