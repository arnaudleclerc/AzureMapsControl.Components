namespace AzureMapsControl.Components.Layers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Markers;

    /// <summary>
    /// Options for the HTML Marker Layer class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class HtmlMarkerLayerOptions : SourceLayerOptions
    {
        /// <summary>
        /// Specifies if the layer should update while the map is moving. When set to false, rendering in the map view will occur after the map has finished moving. New data is not rendered until the moveend event fires. 
        /// When set to true, the layer will constantly re-render as the map is moving which ensures new data is always rendered right away, but may reduce overall performance.
        /// </summary>
        public bool? UpdateWhileMoving { get; set; }

        /// <summary>
        /// A callback function that generates a HtmlMarker for a given data point.
        /// The `id` and `properties` values will be added to the marker as properties within the layer after being created by this callback function.
        /// </summary>
        public Func<HtmlMarker, string, Position, IDictionary<string, object>> MarkerCallback { get; set; }
    }
}

