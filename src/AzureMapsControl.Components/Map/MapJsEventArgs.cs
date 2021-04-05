namespace AzureMapsControl.Components.Map
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    internal class MapJsEventArgs
    {
        public string Error { get; set; }

        public string Type { get; set; }

        public string LayerId { get; set; }
        public IEnumerable<Feature> Features { get; set; }
        public IEnumerable<Shape> Shapes { get; set; }
        public Pixel Pixel { get; set; }
        public Position Position { get; set; }

        public string DataType { get; set; }
        public bool? IsSourceLoaded { get; set; }
        public Source Source { get; set; }
        public string SourceDataType { get; set; }
        public string Style { get; set; }
        public Tile Tile { get; set; }

        public string Message { get; set; }

        public IEnumerable<Pixel> Pixels { get; set; }
        public IEnumerable<Position> Positions { get; set; }

        public string Id { get; set; }
    }
}
