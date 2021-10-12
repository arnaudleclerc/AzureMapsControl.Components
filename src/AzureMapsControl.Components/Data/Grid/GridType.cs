namespace AzureMapsControl.Components.Data.Grid
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Specifies how data is rendered within a grid system.
    /// </summary>
    [JsonConverter(typeof(GridTypeJsonConverter))]
    public struct GridType
    {
        private readonly string _type;

        /// <summary>
        /// Renders data within a square grid as circles.
        /// </summary>
        public static GridType Circle = new("circle");

        /// <summary>
        /// Renders data within a hexagons grid.
        /// </summary>
        public static GridType Hexagon = new("hexagon");

        /// <summary>
        /// Renders data within a hexagon grid as circles.
        /// </summary>
        public static GridType HexCircle = new("hexCircle");

        /// <summary>
        /// Renders data within a rotate hexagon grid. 
        /// </summary>
        public static GridType PointyHexagon = new("pointyHexagon");

        /// <summary>
        /// Renders data within a square grid.
        /// </summary>
        public static GridType Square = new("square");

        /// <summary>
        /// Renders data within a triangular grid.
        /// </summary>
        public static GridType Triangle = new("triangle");
        

        private GridType(string type) => _type = type;

        public override string ToString() => _type;

        public static GridType FromString(string gridType)
        {
            return gridType switch {
                "circle" => Circle,
                "hexagon" => Hexagon,
                "hexCircle" => HexCircle,
                "pointyHexagon" => PointyHexagon,
                "square" => Square,
                "triangle" => Triangle,
                _ => default
            };
        }
    }

    internal sealed class GridTypeJsonConverter : JsonConverter<GridType>
    {
        public override GridType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => GridType.FromString(reader.GetString());
        public override void Write(Utf8JsonWriter writer, GridType value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
