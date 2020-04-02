namespace AzureMapsControl.Components.Map
{
    public class MapLayerEventArgs : MapEventArgs
    {
        public string Id { get; set; }

        internal MapLayerEventArgs(MapJsEventArgs eventArgs) : base(eventArgs) => Id = eventArgs.Id;
    }
}
