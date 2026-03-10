namespace AzureMapsControl.Components.Map
{
    using System.Threading.Tasks;

    internal interface IMapAdderService : IMapService
    {
        public ValueTask AddMapAsync(Map map);
        
        public ValueTask RemoveMapAsync(string mapId);
    }
}
