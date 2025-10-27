namespace AzureMapsControl.Components.Indoor
{
    using System.Threading.Tasks;

    public interface IIndoorService
    {
        /// <summary>
        /// Create an instance of IndoorManager
        /// </summary>
        /// <param name="mapId">ID of the map to associate the indoor manager with</param>
        /// <param name="options">Options of the indoor manager</param>
        /// <returns>Indoor Manager</returns>
        ValueTask<IndoorManager> CreateIndoorManagerAsync(string mapId, IndoorManagerOptions options);

        /// <summary>
        /// Create an instance of IndoorManager
        /// </summary>
        /// <param name="mapId">ID of the map to associate the indoor manager with</param>
        /// <param name="options">Options of the indoor manager</param>
        /// <param name="eventFlags">Events which will be triggered by the IndoorManager</param>
        /// <returns>Indoor Manager</returns>
        ValueTask<IndoorManager> CreateIndoorManagerAsync(string mapId, IndoorManagerOptions options, IndoorManagerEventActivationFlags eventFlags);

        /// <summary>
        /// Create an instance of IndoorManager (legacy method for backward compatibility)
        /// </summary>
        /// <param name="options">Options of the indoor manager</param>
        /// <returns>Indoor Manager</returns>
        [System.Obsolete("Use CreateIndoorManagerAsync(mapId, options) instead for multi-map support")]
        ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options);

        /// <summary>
        /// Create an instance of IndoorManager (legacy method for backward compatibility)
        /// </summary>
        /// <param name="options">Options of the indoor manager</param>
        /// <param name="eventFlags">Events which will be triggered by the IndoorManager</param>
        /// <returns>Indoor Manager</returns>
        [System.Obsolete("Use CreateIndoorManagerAsync(mapId, options, eventFlags) instead for multi-map support")]
        ValueTask<IndoorManager> CreateIndoorManagerAsync(IndoorManagerOptions options, IndoorManagerEventActivationFlags eventFlags);
    }
}
