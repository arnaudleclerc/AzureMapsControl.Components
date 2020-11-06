namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class MapMessageEventArgs : MapEventArgs
    {
        public string Message { get; }
        internal MapMessageEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type) => Message = eventArgs.Message;
    }
}
