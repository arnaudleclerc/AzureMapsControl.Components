namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MultiPolygon : Geometry<IEnumerable<IEnumerable<IEnumerable<Position>>>>
    {
        public BoundingBox BBox { get; set; }
        public MultiPolygon() : base() { }
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates) : base(coordinates) { }
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "MultiPolygon";
    }
}
