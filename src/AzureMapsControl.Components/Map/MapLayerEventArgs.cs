namespace AzureMapsControl.Components.Map
{
    public class MapLayerEventArgs : MapEventArgs
    {
        public string Id { get; set; }

        internal MapLayerEventArgs(Atlas.Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type) => Id = eventArgs.Id;
    }
}
