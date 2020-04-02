namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;

    public class Polygon : Geometry
    {
        public IEnumerable<IEnumerable<Position>> Coordinates { get; set; }
        public BoundingBox BBox { get; set; }
    }
}
