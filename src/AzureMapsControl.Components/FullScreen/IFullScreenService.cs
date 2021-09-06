namespace AzureMapsControl.Components.FullScreen
{
    using System.Threading.Tasks;

    public interface IFullScreenService
    {
        /// <summary>
        /// Checks to see if the browser supports going into fullscreen mode.
        /// </summary>
        /// <returns>True if the browser supports going into fullscreen mode, otherwise false</returns>
        ValueTask<bool> IsSupportedAsync();
    }
}
