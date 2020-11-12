namespace AzureMapsControl.Components.Data
{
    using System.Collections.Generic;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options for a `VectorTileSource`.
    /// </summary>
    public sealed class VectorTileSourceOptions : SourceOptions
    {
        /// <summary>
        /// A bounding box that specifies where tiles are available.
        /// When specified, no tiles outside of the bounding box will be requested.
        /// </summary>
        public BoundingBox Bounds { get; set; }

        /// <summary>
        /// Specifies is the tile systems y coordinate uses the OSGeo Tile Map Services which reverses the Y coordinate axis.
        /// </summary>
        public bool? IsTMS { get; set; }

        /// <summary>
        ///  An integer specifying the maximum zoom level to render the layer at.
        /// </summary>
        public int? MaxZoom { get; set; }

        /// <summary>
        /// An integer specifying the minimum zoom level to render the layer at.
        /// </summary>
        public int? MinZoom { get; set; }

        /// <summary>
        /// An array of one or more tile source URLs.
        /// </summary>
        public IEnumerable<string> Tiles { get; set; }

        /// <summary>
        /// An integer value that specifies the width and height dimensions of the map tiles.
        /// For a seamless experience, the tile size must by a multiplier of 2. (i.e. 256, 512, 1024…).
        /// </summary>
        public int? TileSize { get; set; }

        /// <summary>
        /// A URL to a TileJSON resource.
        /// </summary>
        public string Url { get; set; }
    }
}
