namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Tile object returned by the map when a source data event occurs.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public struct Tile
    {
        /// <summary>
        /// The ID of the tile.
        /// </summary>
        public TileId Id { get; set; }
        /// <summary>
        /// The size of the tile.
        /// </summary>
        public double Size { get; set; }
        /// <summary>
        /// The state of the tile.
        /// `"loading"`: Tile data is in the process of loading.
        /// `"loaded"`: Tile data has been loaded.
        /// `"reloading"`: Tile data has been loaded and is being updated.
        /// `"unloaded"`: The data has been deleted.
        /// `"errored"`: Tile data was not loaded because of an error.
        /// `"expired"`: Tile data was previously loaded, but has expired per its HTTP headers and is in the process of refreshing.
        /// </summary>
        public string State { get; set; }
    }
}
