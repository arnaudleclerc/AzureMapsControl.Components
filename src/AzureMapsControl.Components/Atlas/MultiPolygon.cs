namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;

    public class MultiPolygon : Geometry
    {
        public IEnumerable<IEnumerable<IEnumerable<Position>>> Coordinates { get; set; }
        public BoundingBox BBox { get; set; }
    }
}
