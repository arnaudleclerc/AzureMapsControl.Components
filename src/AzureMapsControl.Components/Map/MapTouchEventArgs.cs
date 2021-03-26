namespace AzureMapsControl.Components.Map
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    public sealed class MapTouchEventArgs : MapEventArgs
    {
        public string LayerId { get; }
        public IEnumerable<Feature> Shapes { get; }
        public Pixel Pixel { get; }
        public IEnumerable<Pixel> Pixels { get; }
        public Position Position { get; }
        public IEnumerable<Position> Positions { get; }

        internal MapTouchEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type)
        {
            LayerId = eventArgs.LayerId;
            Pixel = eventArgs.Pixel;
            Pixels = eventArgs.Pixels;
            Position = eventArgs.Position;
            Positions = eventArgs.Positions;
        }
    }
}
