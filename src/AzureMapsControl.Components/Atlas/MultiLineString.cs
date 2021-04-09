namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter<MultiLineString>))]
    public sealed class MultiLineString : Geometry<IEnumerable<IEnumerable<Position>>>
    {
        internal const string GeometryType = "MultiLineString";
        public BoundingBox BBox { get; set; }
        public MultiLineString() : base(GeometryType) { }
        public MultiLineString(IEnumerable<IEnumerable<Position>> coordinates) : base(coordinates, GeometryType) { }

        public MultiLineString(IEnumerable<IEnumerable<Position>> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;
    }
}
