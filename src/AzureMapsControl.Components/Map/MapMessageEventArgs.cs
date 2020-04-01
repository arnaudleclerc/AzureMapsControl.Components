namespace AzureMapsControl.Map
{
    public class MapMessageEventArgs : MapEventArgs
    {
        public string Message { get; }
        internal MapMessageEventArgs(MapJsEventArgs eventArgs): base(eventArgs) => Message = eventArgs.Message;
    }
}
