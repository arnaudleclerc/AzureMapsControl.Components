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
    public struct MapStyle
    {
        private readonly string _style;

        /// <summary>
        /// The blank style provides a blank canvas for visualizing data
        /// </summary>
        public static readonly MapStyle Blank = new("blank");

        /// <summary>
        /// The blank_accessible map styles provides a blank canvas for visualizing data. 
        /// The blank_accessible style will continue to provide screen reader updates with map's location details, even though the base map isn't displayed.
        /// </summary>
        public static readonly MapStyle BlankAccessible = new("blank_accessible");

        /// <summary>
        /// The satellite style is a combination of satellite and aerial imagery.
        /// </summary>
        public static readonly MapStyle Satellite = new("satellite");

        /// <summary>
        /// This map style is a hybrid of roads and labels overlaid on top of satellite and aerial imagery.
        /// </summary>
        public static readonly MapStyle SatelliteRoadLabels = new("satellite_road_labels");

        /// <summary>
        /// grayscale dark is a dark version of the road map style.
        /// </summary>
        public static readonly MapStyle GrayscaleDark = new("grayscale_dark");

        /// <summary>
        /// grayscale light is a light version of the road map style.
        /// </summary>
        public static readonly MapStyle GrayscaleLight = new("grayscale_light");

        /// <summary>
        /// night is a dark version of the road map style with colored roads and symbols.
        /// </summary>
        public static readonly MapStyle Night = new("night");

        /// <summary>
        /// A road map is a standard map that displays roads. It also displays natural and artificial features, and the labels for those features.
        /// </summary>
        public static readonly MapStyle Road = new("road");

        /// <summary>
        /// road shaded relief is an Azure Maps main style completed with contours of the Earth.
        /// </summary>
        public static readonly MapStyle RoadShadedRelief = new("road_shaded_relief");

        /// <summary>
        /// high_contrast_dark is a dark map style with a higher contrast than the other styles.
        /// </summary>
        public static readonly MapStyle HighContrastDark = new("high_contrast_dark");

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
            Road,
            RoadShadedRelief,
            HighContrastDark
        };

        public override string ToString() => _style;

        /// <summary>
        /// Return a MapStyle corresponding to the given value
        /// </summary>
        /// <param name="style">Value of the MapStyle</param>
        /// <returns>MapStyle corresponding to the given value. If none was found, returns `default`</returns>
        public static MapStyle FromString(string style)
        {
            return style switch {
                "blank" => Blank,
                "blank_accessible" => BlankAccessible,
                "satellite" => Satellite,
                "satellite_road_labels" => SatelliteRoadLabels,
                "grayscale_dark" => GrayscaleDark,
                "grayscale_light" => GrayscaleLight,
                "night" => Night,
                "road" => Road,
                "road_shaded_relief" => RoadShadedRelief,
                "high_contrast_dark" => HighContrastDark,
                _ => default,
            };
        }
    }

    internal class MapStyleJsonConverter : JsonConverter<MapStyle>
    {
        public override MapStyle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => MapStyle.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, MapStyle value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
