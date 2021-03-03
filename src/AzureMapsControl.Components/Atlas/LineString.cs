namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class LineString : Geometry<IEnumerable<Position>>
    {
        public BoundingBox BBox { get; set; }

        public LineString() : base() { }
        public LineString(string id) : base(id) { }

        public LineString(IEnumerable<Position> coordinates) : base(coordinates) { }

        public LineString(IEnumerable<Position> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "LineString";
    }
}
