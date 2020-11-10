namespace AzureMapsControl.Components.Map
{
    using System.Threading.Tasks;

    internal interface IMapAdderService : IMapService
    {
        Task AddMapAsync(Map map);
    }
}
