namespace AzureMapsControl.Components.Map
{
    using AzureMapsControl.Components.Atlas;

    internal class CameraOptions
    {
        public int? Bearing { get; set; }
        public BoundingBox Bounds { get; set; }
        public Position Center { get; set; }
        public Pixel CenterOffset { get; set; }
        public int? Duration { get; set; }
        public BoundingBox MaxBounds { get; set; }
        public int? MaxZoom { get; set; }
        public int? MinZoom { get; set; }
        public Pixel Offset { get; set; }
        public Padding Padding { get; set; }
        public int? Pitch { get; set; }
        public string Type { get; set; }
        public int? Zoom { get; set; }
    }
}
