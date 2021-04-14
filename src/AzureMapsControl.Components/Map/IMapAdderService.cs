namespace AzureMapsControl.Components.Map
{
    using System.Threading.Tasks;

    internal interface IMapAdderService : IMapService
    {
        ValueTask AddMapAsync(Map map);
    }
}
