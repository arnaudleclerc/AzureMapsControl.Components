namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter<MultiPoint>))]
    public sealed class MultiPoint : Geometry<IEnumerable<Position>>
    {
        internal const string InnerGeometryType = "MultiPoint";
        public BoundingBox BBox { get; set; }

        public MultiPoint() : base(InnerGeometryType) { }

        public MultiPoint(IEnumerable<Position> coordinates) : base(coordinates, InnerGeometryType) { }

        public MultiPoint(IEnumerable<Position> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;
    }
}
