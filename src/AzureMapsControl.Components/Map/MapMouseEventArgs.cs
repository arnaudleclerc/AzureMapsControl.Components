namespace AzureMapsControl.Components.Map
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    public sealed class MapMouseEventArgs : MapEventArgs
    {
        public string LayerId { get; }
        public Pixel Pixel { get; }
        public Position Position { get; }

        internal MapMouseEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type)
        {
            LayerId = eventArgs.LayerId;
            Pixel = eventArgs.Pixel;
            Position = eventArgs.Position;
        }
    }
}
