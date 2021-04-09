namespace AzureMapsControl.Components.Map
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    public sealed class MapMouseEventArgs : MapEventArgs
    {
        public string LayerId { get; }
        public IEnumerable<Shape<Geometry>> Shapes { get; }
        public IEnumerable<Feature<Geometry>> Features { get; }
        public Pixel Pixel { get; }
        public Position Position { get; }

        internal MapMouseEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type)
        {
            Features = eventArgs.Features;
            LayerId = eventArgs.LayerId;
            Pixel = eventArgs.Pixel;
            Position = eventArgs.Position;
            Shapes = eventArgs.Shapes;
        }
    }
}
