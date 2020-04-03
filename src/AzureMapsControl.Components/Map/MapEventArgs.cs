namespace AzureMapsControl.Components.Map
{
    public class MapEventArgs
    {
        public Atlas.Map Map { get; }
        public string Type { get; }

        internal MapEventArgs(Atlas.Map map, string type)
        {
            Map = map;
            Type = type;
        }
    }
}
