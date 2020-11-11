namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class LineString : Geometry
    {
        public IEnumerable<Position> Coordinates { get; set; }
        public BoundingBox BBox { get; set; }

        public LineString() : base() { }
        public LineString(string id) : base(id) { }

        public LineString(IEnumerable<Position> coordinates) : base(Guid.NewGuid().ToString()) => Coordinates = coordinates;

        public LineString(IEnumerable<Position> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "LineString";
    }
}
