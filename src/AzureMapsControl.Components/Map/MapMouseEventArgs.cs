namespace AzureMapsControl.Components.Map
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Atlas;

    public class MapMouseEventArgs : MapEventArgs
    {
        public string LayerId { get; }
        public IEnumerable<Feature> Shapes { get; }
        public Pixel Pixel { get; }
        public Position Position { get; }

        internal MapMouseEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type)
        {
            LayerId = eventArgs.LayerId;
            Shapes = eventArgs.Shapes;
            Pixel = eventArgs.Pixel;
            Position = eventArgs.Position;
        }
    }
}
