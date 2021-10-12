namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using AzureMapsControl.Components.Atlas;


    /// <summary>
    /// Options used when rendering geometries in a SymbolLayer.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class SymbolLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// Options used to customize the icons of the symbols.
        /// </summary>
        public IconOptions IconOptions { get; set; }

        /// <summary>
        /// Distance in pixels between two symbol anchors along a line. Must be greater or equal to 1.
        /// </summary>
        public ExpressionOrNumber LineSpacing { get; set; }

        /// <summary>
        /// Options used to customize the text of the symbols.
        /// </summary>
        public TextOptions TextOptions { get; set; }

        /// <summary>
        /// Specifies the label placement relative to its geometry.
        /// </summary>
        public SymbolLayerPlacement Placement { get; set; }
    }

    /// <summary>
    /// Specifies the label placement relative to its geometry.
    /// </summary>
    [JsonConverter(typeof(SymbolLayerPlacementJsonConverter))]
    public struct SymbolLayerPlacement
    {
        private readonly string _symbolLayerPlacement;

        /// <summary>
        /// The label is placed along the line of the geometry.
        /// Can only be used on LineString and Polygon geometries.
        /// </summary>
        public static readonly SymbolLayerPlacement Line = new("line");

        /// <summary>
        /// The label is placed at the center of the line of the geometry.
        /// Can only be used on `LineString` and `Polygon` geometries
        /// </summary>
        public static readonly SymbolLayerPlacement LineCenter = new("line-center");

        /// <summary>
        /// The label is placed at the point where the geometry is located.
        /// </summary>
        public static readonly SymbolLayerPlacement Point = new("point");

        private SymbolLayerPlacement(string symbolLayerPlacement) => _symbolLayerPlacement = symbolLayerPlacement;

        public override string ToString() => _symbolLayerPlacement;

        /// <summary>
        /// Return a SymbolLayerPlacement corresponding to the given value
        /// </summary>
        /// <param name="symbolLayerPlacement">Value of the SymbolLayerPlacement</param>
        /// <returns>SymbolLayerPlacement corresponding to the given value. If none was found, returns `default`</returns>
        public static SymbolLayerPlacement FromString(string symbolLayerPlacement)
        {
            return symbolLayerPlacement switch {
                "line" => Line,
                "line-center" => LineCenter,
                "point" => Point,
                _ => default,
            };
        }
    }

    internal class SymbolLayerPlacementJsonConverter : JsonConverter<SymbolLayerPlacement>
    {
        public override SymbolLayerPlacement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SymbolLayerPlacement.FromString(reader.GetString());

        public override void Write(Utf8JsonWriter writer, SymbolLayerPlacement value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
