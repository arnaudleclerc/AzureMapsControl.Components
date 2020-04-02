namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;

    public class MultiPoint : Geometry
    {
        public IEnumerable<Position> Coordinates { get; set; }
        public BoundingBox Bbox { get; set; }
    }
}
