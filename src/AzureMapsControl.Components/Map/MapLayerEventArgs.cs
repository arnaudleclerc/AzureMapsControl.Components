using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Map
{
    [ExcludeFromCodeCoverage]
    public sealed class MapLayerEventArgs : MapEventArgs
    {
        public string Id { get; set; }

        internal MapLayerEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type) => Id = eventArgs.Id;
    }
}
