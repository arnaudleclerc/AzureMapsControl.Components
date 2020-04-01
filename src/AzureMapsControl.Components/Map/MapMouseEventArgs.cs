namespace AzureMapsControl.Map
{
    using System.Collections.Generic;
    using AzureMapsControl.Atlas;

    public class MapMouseEventArgs : MapEventArgs
    {
        public string LayerId { get; }
        public IEnumerable<Feature> Shapes { get; }
        public Pixel Pixel { get; }
        public Position Position { get; }

        internal MapMouseEventArgs(MapJsEventArgs eventArgs) : base(eventArgs)
        {
            LayerId = eventArgs.LayerId;
            Shapes = eventArgs.Shapes;
            Pixel = eventArgs.Pixel;
            Position = eventArgs.Position;
        }
    }
}
