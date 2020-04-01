namespace AzureMapsControl.Map
{
    public class MapEventArgs
    {
        public string MapId { get; }
        public string Type { get; }

        internal MapEventArgs(MapJsEventArgs eventArgs)
        {
            MapId = eventArgs.MapId;
            Type = eventArgs.Type;
        }
    }
}
