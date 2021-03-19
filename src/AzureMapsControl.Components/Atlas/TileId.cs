namespace AzureMapsControl.Components.Atlas
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public struct TileId
    {
        /// <summary>
        /// The x coordinate of the tile.
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// The y coordinate of the tile.
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// The z coordinate of the tile.
        /// </summary>
        public double Z { get; set; }
    }
}
