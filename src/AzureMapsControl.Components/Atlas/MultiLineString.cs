namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MultiLineString : Geometry
    {
        public IEnumerable<IEnumerable<Position>> Coordinates
        {
            get; set;
        }
        public BoundingBox BBox
        {
            get; set;
        }
        public MultiLineString()
        {
        }

        public MultiLineString(IEnumerable<IEnumerable<Position>> coordinates) => Coordinates = coordinates;

        public MultiLineString(IEnumerable<IEnumerable<Position>> coordinates, BoundingBox bbox): this(coordinates) => BBox = bbox;

        internal override string GetGeometryType() => "MultiLineString";
    }
}
