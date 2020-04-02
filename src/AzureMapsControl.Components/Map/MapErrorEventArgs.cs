namespace AzureMapsControl.Components.Map
{
    public class MapErrorEventArgs : MapEventArgs
    {
        public string Error { get; }

        internal MapErrorEventArgs(MapJsEventArgs eventArgs) : base(eventArgs) => Error = eventArgs.Error;
    }
}
