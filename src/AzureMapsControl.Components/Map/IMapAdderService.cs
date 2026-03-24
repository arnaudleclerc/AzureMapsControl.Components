
using System.Threading.Tasks;

namespace AzureMapsControl.Components.Map;
internal interface IMapAdderService : IMapService
{
    ValueTask AddMapAsync(Map map);
}
