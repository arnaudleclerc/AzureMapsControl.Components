namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Collections.Generic;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Options used when rendering raster tiled images in a TileLayer.
    /// </summary>
    public sealed class TileLayerOptions : MediaLayerOptions
    {
        public TileLayerOptions(Uri tileUrl) => TileUrl = tileUrl.ToString();
        public TileLayerOptions(string tileUrl) => TileUrl = tileUrl;

        /// <summary>
        /// A bounding box that specifies where tiles are available.
        /// When specified, no tiles outside of the bounding box will be requested.
        /// </summary>
        public BoundingBox Bounds { get; set; }

        /// <summary>
        /// Specifies if the tile systems coordinates uses the Tile Map Services specification, which reverses the Y coordinate axis.
        /// </summary>
        public bool? IsTMS { get; set; }

        /// <summary>
        /// An integer specifying the maximum zoom level in which tiles are available from the tile source.
        /// </summary>
        public int? MaxSourceZoom { get; set; }

        /// <summary>
        /// An integer specifying the minimum zoom level in which tiles are available from the tile source.
        /// </summary>
        public int? MinSourceZoom { get; set; }

        /// <summary>
        /// An array of subdomain values to apply to the tile URL.
        /// </summary>
        public IEnumerable<string> Subdomains { get; set; }

        /// <summary>
        /// An integer value that specifies the width and height dimensions of the map tiles.
        /// </summary>
        public int? TileSize { get; set; }

        /// <summary>
        /// A http/https URL to a TileJSON resource or a tile URL template that uses the following parameters:
        /// {x}: X position of the tile. Usually also needs {y} and {z}.
        /// {y}: Y position of the tile. Usually also needs {x} and {z}.
        /// {z}: Zoom level of the tile. Usually also needs {x} and {y}.
        /// {quadkey}: Tile quadKey id based on the Bing Maps tile system naming convention.
        /// {bbox-epsg-3857}: A bounding box string with the format {west},{south},{east},{north} in the EPSG 4325 Spacial Reference System.
        /// {subdomain}: A placeholder where the subdomain values if specified will be added.
        /// </summary>
        public string TileUrl { get; set; }
    }
}
