namespace AzureMapsControl.Components.Atlas
{
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
        public MultiPolygon()
        {
        }
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates) => Coordinates = coordinates;
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "MultiPolygon";
    }
}
