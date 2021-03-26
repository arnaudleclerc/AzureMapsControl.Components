namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class Polygon : Geometry<IEnumerable<IEnumerable<Position>>>
    {
        public BoundingBox BBox { get; set; }

        public Polygon() : base() { }

        public Polygon(IEnumerable<IEnumerable<Position>> coordinates) : base(coordinates) { }

        public Polygon(IEnumerable<IEnumerable<Position>> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "Polygon";
    }
}
