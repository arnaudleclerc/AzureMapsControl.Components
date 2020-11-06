namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class MapEventArgs
    {
        public Map Map { get; }
        public string Type { get; }

        internal MapEventArgs(Map map, string type)
        {
            Map = map;
            Type = type;
        }
    }
}
