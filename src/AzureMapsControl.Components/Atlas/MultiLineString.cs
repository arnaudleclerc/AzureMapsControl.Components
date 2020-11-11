namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MultiLineString : Geometry
    {
        public IEnumerable<IEnumerable<Position>> Coordinates
        {
            get; set;
        }
        public BoundingBox BBox
        {
            get; set;
        }
        public MultiLineString() : base() { }
        public MultiLineString(string id) : base(id) { }

        public MultiLineString(IEnumerable<IEnumerable<Position>> coordinates) : base(Guid.NewGuid().ToString()) => Coordinates = coordinates;

        public MultiLineString(IEnumerable<IEnumerable<Position>> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "MultiLineString";
    }
}
