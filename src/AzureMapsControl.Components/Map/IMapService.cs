namespace AzureMapsControl.Components.Map
{
    public interface IMapService
    {
        /// <summary>
        /// The map
        /// </summary>
        Map Map { get; }

        /// <summary>
        /// Triggered when the ready event has been triggered on the map
        /// </summary>
        event MapReadyEvent OnMapReadyAsync;
    }
}
