namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;

    public sealed class MultiPoint : Geometry
    {
        public IEnumerable<Position> Coordinates { get; set; }
        public BoundingBox Bbox { get; set; }

        public MultiPoint() { }

        public MultiPoint(IEnumerable<Position> coordinates) => Coordinates = coordinates;

        public MultiPoint(IEnumerable<Position> coordinates, BoundingBox bbox): this(coordinates) => Bbox = bbox;

        internal override string GetGeometryType() => "MultiPoint";
    }
}
