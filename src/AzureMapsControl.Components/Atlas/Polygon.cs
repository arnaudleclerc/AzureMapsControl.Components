namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;

    public sealed class Polygon : Geometry
    {
        public IEnumerable<IEnumerable<Position>> Coordinates { get; set; }
        public BoundingBox BBox { get; set; }

        public Polygon() { }

        public Polygon(IEnumerable<IEnumerable<Position>> coordinates) => Coordinates = coordinates;

        public Polygon(IEnumerable<IEnumerable<Position>> coordinates, BoundingBox bbox): this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "Polygon";
    }
}
