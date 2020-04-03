namespace AzureMapsControl.Components.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;
    using AzureMapsControl.Components.Markers;

    /// <summary>
    /// Representation of a map
    /// </summary>
    public sealed class Map
    {
        private readonly Func<IEnumerable<Control>, Task> _addControlsCallback;
        private readonly Func<IEnumerable<HtmlMarker>, Task> _addHtmlMarkersCallback;

        /// <summary>
        /// ID of the map
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Controls added to the map
        /// </summary>
        public IEnumerable<Control> Controls { get; private set; }

        /// <summary>
        /// HtmlMarkers added to the map
        /// </summary>
        public IEnumerable<HtmlMarker> HtmlMarkers { get; private set; }

        internal Map(string id,
            Func<IEnumerable<Control>, Task> addControlsCallback,
            Func<IEnumerable<HtmlMarker>, Task> addHtmlMarkersCallback)
        {
            Id = id;
            _addControlsCallback = addControlsCallback;
            _addHtmlMarkersCallback = addHtmlMarkersCallback;
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(params Control[] controls)
        {
            Controls = controls?.Where(control => control != null);
            await _addControlsCallback.Invoke(Controls);
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(IEnumerable<Control> controls)
        {
            Controls = controls?.Where(control => control != null);
            await _addControlsCallback.Invoke(Controls);
        }

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(params HtmlMarker[] markers)
        {
            HtmlMarkers = (HtmlMarkers ?? new HtmlMarker[markers.Length]).Concat(markers?.Where(marker => marker != null));
            await _addHtmlMarkersCallback.Invoke(HtmlMarkers);
        }

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(IEnumerable<HtmlMarker> markers)
        {
            HtmlMarkers = (HtmlMarkers ?? new HtmlMarker[markers.Count()]).Concat(markers?.Where(marker => marker != null));
            await _addHtmlMarkersCallback.Invoke(HtmlMarkers);
        }

    }
}
