namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MultiPoint : Geometry
    {
        public IEnumerable<Position> Coordinates { get; set; }
        public BoundingBox Bbox { get; set; }

        public MultiPoint() : base() { }

        public MultiPoint(IEnumerable<Position> coordinates) : base(Guid.NewGuid().ToString()) => Coordinates = coordinates;

        public MultiPoint(IEnumerable<Position> coordinates, BoundingBox bbox) : this(coordinates) => Bbox = bbox;

        public MultiPoint(string id) : base(id) { }

        public MultiPoint(string id, IEnumerable<Position> coordinates) : base(id) => Coordinates = coordinates;

        public MultiPoint(string id, IEnumerable<Position> coordinates, BoundingBox bbox) : this(id, coordinates) => Bbox = bbox;

        internal override string GetGeometryType() => "MultiPoint";
    }
}
