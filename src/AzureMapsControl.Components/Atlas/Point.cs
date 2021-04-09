namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter<Point>))]
    public class Point : Geometry<Position>
    {
        internal const string GeometryType = "Point";
        public Point() : base(GeometryType) { }

        public Point(Position coordinates) : base(coordinates, GeometryType) { }
    }
}
