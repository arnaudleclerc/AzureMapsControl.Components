namespace AzureMapsControl.Atlas
{
    using System.Collections.Generic;

    public class MultiLineString : Geometry
    {
        public IEnumerable<IEnumerable<Position>> Coordinates { get; set; }
        public BoundingBox BBox { get; set; }
    }
}
