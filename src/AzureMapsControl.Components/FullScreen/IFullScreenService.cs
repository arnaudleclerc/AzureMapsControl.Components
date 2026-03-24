
using System.Threading.Tasks;

namespace AzureMapsControl.Components.FullScreen;
public interface IFullScreenService
{
    /// <summary>
    /// Checks to see if the browser supports going into fullscreen mode.
    /// </summary>
    /// <returns>True if the browser supports going into fullscreen mode, otherwise false</returns>
    ValueTask<bool> IsSupportedAsync();
}
