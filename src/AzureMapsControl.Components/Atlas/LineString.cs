namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class LineString : Geometry
    {
        public IEnumerable<Position> Coordinates { get; set; }
        public BoundingBox BBox { get; set; }

        public LineString() { }

        public LineString(IEnumerable<Position> coordinates) => Coordinates = coordinates;

        public LineString(IEnumerable<Position> coordinates, BoundingBox bbox): this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "LineString";
    }
}
