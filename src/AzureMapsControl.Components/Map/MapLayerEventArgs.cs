namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MapLayerEventArgs : MapEventArgs
    {
        public string Id { get; set; }

        internal MapLayerEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type) => Id = eventArgs.Id;
    }
}
