namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter<LineString>))]
    public sealed class LineString : Geometry<IEnumerable<Position>>
    {
        internal const string InnerGeometryType = "LineString";

        public BoundingBox BBox { get; set; }

        public LineString() : base(InnerGeometryType) { }

        public LineString(IEnumerable<Position> coordinates) : base(coordinates, InnerGeometryType) { }

        public LineString(IEnumerable<Position> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;
    }
}
