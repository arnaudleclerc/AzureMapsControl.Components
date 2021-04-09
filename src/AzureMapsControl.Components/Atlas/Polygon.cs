namespace AzureMapsControl.Components.Atlas
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    [JsonConverter(typeof(GeometryJsonConverter<Polygon>))]
    public sealed class Polygon : Geometry<IEnumerable<IEnumerable<Position>>>
    {
        internal const string GeometryType = "Polygon";
        public BoundingBox BBox { get; set; }

        public Polygon() : base(GeometryType) { }

        public Polygon(IEnumerable<IEnumerable<Position>> coordinates) : base(coordinates, GeometryType) { }

        public Polygon(IEnumerable<IEnumerable<Position>> coordinates, BoundingBox bbox) : this(coordinates) => BBox = bbox;
    }
}
