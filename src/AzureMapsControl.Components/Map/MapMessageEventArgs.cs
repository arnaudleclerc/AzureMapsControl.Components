namespace AzureMapsControl.Components.Map
{
    public class MapMessageEventArgs : MapEventArgs
    {
        public string Message { get; }
        internal MapMessageEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type) => Message = eventArgs.Message;
    }
}
