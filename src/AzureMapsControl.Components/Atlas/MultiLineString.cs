namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MultiLineString : Geometry<IEnumerable<IEnumerable<Position>>>
    {
        public BoundingBox BBox { get; set; }
        public MultiLineString() : base() { }
        public MultiLineString(IEnumerable<IEnumerable<Position>> coordinates) : base(coordinates) { }

        public MultiLineString(IEnumerable<IEnumerable<Position>> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "MultiLineString";
    }
}
