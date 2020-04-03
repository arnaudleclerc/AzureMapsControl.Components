namespace AzureMapsControl.Components.Map
{
    public class MapErrorEventArgs : MapEventArgs
    {
        public string Error { get; }

        internal MapErrorEventArgs(Atlas.Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type) => Error = eventArgs.Error;
    }
}
