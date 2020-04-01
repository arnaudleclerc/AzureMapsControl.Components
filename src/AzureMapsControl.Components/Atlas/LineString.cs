namespace AzureMapsControl.Atlas
{
    using System.Collections.Generic;

    public class LineString : Geometry
    {
        public IEnumerable<Position> Coordinates { get; set; }
        public BoundingBox BBox { get; set; }
    }
}
