namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MultiPolygon : Geometry
    {
        public IEnumerable<IEnumerable<IEnumerable<Position>>> Coordinates
        {
            get; set;
        }
        public BoundingBox BBox
        {
            get; set;
        }
        public MultiPolygon() : base() { }
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates) : base(Guid.NewGuid().ToString()) => Coordinates = coordinates;
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;
        public MultiPolygon(string id) : base(id) { }
        public MultiPolygon(string id, IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates) : base(id) => Coordinates = coordinates;
        public MultiPolygon(string id, IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates, BoundingBox bbox) : this(id, coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "MultiPolygon";
    }
}
