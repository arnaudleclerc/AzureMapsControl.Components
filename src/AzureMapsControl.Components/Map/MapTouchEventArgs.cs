namespace AzureMapsControl.Components.Map
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Atlas;

    public class MapTouchEventArgs : MapEventArgs
    {
        public string LayerId { get; }
        public Pixel Pixel { get; }
        public IEnumerable<Pixel> Pixels { get; }
        public Position Position { get; }
        public IEnumerable<Position> Positions { get; }
        public IEnumerable<Feature> Shapes { get; }

        internal MapTouchEventArgs(MapJsEventArgs eventArgs) : base(eventArgs)
        {
            LayerId = eventArgs.LayerId;
            Pixel = eventArgs.Pixel;
            Pixels = eventArgs.Pixels;
            Position = eventArgs.Position;
            Positions = eventArgs.Positions;
            Shapes = eventArgs.Shapes;
        }
    }
}
