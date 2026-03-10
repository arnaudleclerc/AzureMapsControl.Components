namespace AzureMapsControl.Components.Map
{
    using System.Collections.Generic;

    public interface IMapService
    {
        /// <summary>
        /// The maps indexed by their ID
        /// </summary>
        IReadOnlyDictionary<string, Map> Maps { get; }

        /// <summary>
        /// Get a specific map by its ID
        /// </summary>
        /// <param name="mapId">The ID of the map to retrieve</param>
        /// <returns>The map instance or null if not found</returns>
        Map GetMap(string mapId);

        ///<summary>
        /// The latest map added to the service
        /// deprecated: use GetMap with specific ID instead
        /// </summary>
        Map Map { get; }

        /// <summary>
        /// Triggered when the ready event has been triggered on a map
        /// </summary>
        event MapReadyEvent OnMapReadyAsync;
    }
}
