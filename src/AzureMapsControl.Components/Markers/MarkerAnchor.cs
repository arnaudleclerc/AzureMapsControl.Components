namespace AzureMapsControl.Components.Markers
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Indicates the marker's location relative to its position on the map.
    /// </summary>
    [JsonConverter(typeof(MarkerAnchorJsonConverter))]
    public struct MarkerAnchor
    {
        private readonly string _anchor;

        public static readonly MarkerAnchor Bottom = new("bottom");
        public static readonly MarkerAnchor BottomLeft = new("bottom-left");
        public static readonly MarkerAnchor BottomRight = new("bottom-right");
        public static readonly MarkerAnchor Center = new("center");
        public static readonly MarkerAnchor Left = new("left");
        public static readonly MarkerAnchor Right = new("right");
        public static readonly MarkerAnchor Top = new("top");
        public static readonly MarkerAnchor TopLeft = new("top-left");
        public static readonly MarkerAnchor TopRight = new("top-right");

        private MarkerAnchor(string anchor) => _anchor = anchor;

        public override string ToString() => _anchor;

        /// <summary>
        /// Return a MarkerAnchor corresponding to the given value
        /// </summary>
        /// <param name="markerAnchor">Value of the MarkerAnchor</param>
        /// <returns>MarkerAnchor corresponding to the given value. If none was found, returns `default`</returns>
        public static MarkerAnchor FromString(string markerAnchor)
        {
            return markerAnchor switch {
                "bottom" => Bottom,
                "bottom-left" => BottomLeft,
                "bottom-right" => BottomRight,
                "center" => Center,
                "left" => Left,
                "right" => Right,
                "top" => Top,
                "top-left" => TopLeft,
                "top-right" => TopRight,
                _ => default,
            };
        }
    }

    internal sealed class MarkerAnchorJsonConverter : JsonConverter<MarkerAnchor>
    {
        public override MarkerAnchor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => MarkerAnchor.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, MarkerAnchor value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
