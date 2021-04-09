namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter<MultiPolygon>))]
    public sealed class MultiPolygon : Geometry<IEnumerable<IEnumerable<IEnumerable<Position>>>>
    {
        internal const string GeometryType = "MultiPolygon";
        public BoundingBox BBox { get; set; }
        public MultiPolygon() : base(GeometryType) { }
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates) : base(coordinates, GeometryType) { }
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;
    }
}
