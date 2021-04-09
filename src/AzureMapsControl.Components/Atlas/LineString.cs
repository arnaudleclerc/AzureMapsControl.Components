namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter<LineString>))]
    public sealed class LineString : Geometry<IEnumerable<Position>>
    {
        internal const string GeometryType = "LineString";

        public BoundingBox BBox { get; set; }

        public LineString() : base(GeometryType) { }

        public LineString(IEnumerable<Position> coordinates) : base(coordinates, GeometryType) { }

        public LineString(IEnumerable<Position> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;
    }
}
