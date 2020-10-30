namespace AzureMapsControl.Components.Layers
{
    using System.Collections.Generic;
    using System.Linq;

    using AzureMapsControl.Components.Atlas;
    public sealed class ImageLayerOptions : MediaLayerOptions
    {

        private class ImageLayerJsOptions
        {
            public string Url { get; set; }
            public IEnumerable<IEnumerable<double>> Coordinates { get; set; }
        }

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

        internal override object GenerateJsOptions()
        {
            return new ImageLayerJsOptions {
                Url = Url,
                Coordinates = Coordinates.Select(c => new double[] { c.Longitude, c.Latitude })
            };
        }

    }
}
