namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MapErrorEventArgs : MapEventArgs
    {
        public string Error { get; }

        internal MapErrorEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type) => Error = eventArgs.Error;
    }
}
