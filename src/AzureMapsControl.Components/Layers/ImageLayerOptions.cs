namespace AzureMapsControl.Components.Layers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    public sealed class ImageLayerOptions : MediaLayerOptions
    {
        /// <summary>
        /// URL to an image to overlay. Images hosted on other domains must have CORs enabled.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// An array of positions for the corners of the image listed in clockwise order: [top left, top right, bottom right, bottom left].
        /// </summary>
        public IEnumerable<Position> Coordinates { get; }

        public ImageLayerOptions(string url, IEnumerable<Position> coordinates)
        {
            Url = url;
            Coordinates = coordinates;
        }
    }
}
