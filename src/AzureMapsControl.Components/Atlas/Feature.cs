namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class Feature
    {
        public string Type { get; set; }
        public BoundingBox BBox { get; set; }
        public string Id { get; set; }
    }
}
