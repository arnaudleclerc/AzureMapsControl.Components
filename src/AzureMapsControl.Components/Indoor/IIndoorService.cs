
using System.Threading.Tasks;

namespace AzureMapsControl.Components.Indoor;
public interface IIndoorService
{
    /// <summary>
    /// Create an instance of IndoorManager
    /// </summary>
    /// <param name="options">Options of the indoor manager</param>
    /// <returns>Indoor Manager</returns>
    ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options);

    /// <summary>
    /// Create an instance of IndoorManager
    /// </summary>
    /// <param name="options">Options of the indoor manager</param>
    /// <param name="eventFlags">Events which will be triggered by the IndoorManager</param>
    /// <returns>Indoor Manager</returns>
    ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options, IndoorManagerEventActivationFlags eventFlags);
}
