namespace AzureMapsControl.Components.Markers
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Indicates the marker's location relative to its position on the map.
    /// </summary>
    [JsonConverter(typeof(MarkerAnchorJsonConverter))]
    public sealed class MarkerAnchor
    {
        private readonly string _anchor;

        public static readonly MarkerAnchor Bottom = new MarkerAnchor("bottom");
        public static readonly MarkerAnchor BottomLeft = new MarkerAnchor("bottom-left");
        public static readonly MarkerAnchor BottomRight = new MarkerAnchor("bottom-right");
        public static readonly MarkerAnchor Center = new MarkerAnchor("center");
        public static readonly MarkerAnchor Left = new MarkerAnchor("left");
        public static readonly MarkerAnchor Right = new MarkerAnchor("right");
        public static readonly MarkerAnchor Top = new MarkerAnchor("top");
        public static readonly MarkerAnchor TopLeft = new MarkerAnchor("top-left");
        public static readonly MarkerAnchor TopRight = new MarkerAnchor("top-right");

        private MarkerAnchor(string anchor) => _anchor = anchor;

        public override string ToString() => _anchor;

        internal static MarkerAnchor FromString(string type)
        {
            return type switch {
                "bottom" => Bottom,
                "bottom-left" => BottomLeft,
                "bottom-right" => BottomRight,
                "center" => Center,
                "left" => Left,
                "right" => Right,
                "top" => Top,
                "top-left" => TopLeft,
                "top-right" => TopRight,
                _ => null,
            };
        }
    }

    internal sealed class MarkerAnchorJsonConverter : JsonConverter<MarkerAnchor>
    {
        public override MarkerAnchor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => MarkerAnchor.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, MarkerAnchor value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
