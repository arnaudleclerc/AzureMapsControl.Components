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
        private readonly Func<IEnumerable<HtmlMarkerUpdate>, Task> _updateHtmlMarkersCallback;

        private Control[] _controls;
        private HtmlMarker[] _htmlMarkers;

        /// <summary>
        /// ID of the map
        /// </summary>
        public string Id { get; }

        internal IEnumerable<Control> Controls
        {
            get => _controls;
            set => _controls = value?.ToArray();
        }

        internal IEnumerable<HtmlMarker> HtmlMarkers
        {
            get => _htmlMarkers;
            set => _htmlMarkers = value?.ToArray();
        }

        internal Map(string id,
            Func<IEnumerable<Control>, Task> addControlsCallback,
            Func<IEnumerable<HtmlMarker>, Task> addHtmlMarkersCallback,
            Func<IEnumerable<HtmlMarkerUpdate>, Task> updateHtmlMarkersCallback)
        {
            Id = id;
            _addControlsCallback = addControlsCallback;
            _addHtmlMarkersCallback = addHtmlMarkersCallback;
            _updateHtmlMarkersCallback = updateHtmlMarkersCallback;
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(params Control[] controls)
        {
            _controls = controls?.Where(control => control != null).ToArray();
            await _addControlsCallback.Invoke(_controls);
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(IEnumerable<Control> controls)
        {
            _controls = controls?.Where(control => control != null).ToArray();
            await _addControlsCallback.Invoke(_controls);
        }

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(params HtmlMarker[] markers)
        {
            _htmlMarkers = (_htmlMarkers ?? new HtmlMarker[0]).Concat(markers?.Where(marker => marker != null)).ToArray();
            await _addHtmlMarkersCallback.Invoke(_htmlMarkers);
        }

        /// <summary>
        /// Add HtmlMarkers to the map
        /// </summary>
        /// <param name="markers">Html Marker to add to the map</param>
        /// <returns></returns>
        public async Task AddHtmlMarkersAsync(IEnumerable<HtmlMarker> markers)
        {
            _htmlMarkers = (_htmlMarkers ?? new HtmlMarker[0]).Concat(markers?.Where(marker => marker != null)).ToArray();
            await _addHtmlMarkersCallback.Invoke(_htmlMarkers);
        }

        /// <summary>
        /// Update HtmlMarkers on the map
        /// </summary>
        /// <param name="updates">HtmlMarkers to update</param>
        /// <returns></returns>
        public async Task UpdateHtmlMarkersAsync(params HtmlMarkerUpdate[] updates) => await _updateHtmlMarkersCallback.Invoke(updates);

        /// <summary>
        /// Update HtmlMarkers on the map
        /// </summary>
        /// <param name="updates">HtmlMarkers to update</param>
        /// <returns></returns>
        public async Task UpdateHtmlMarkersAsync(IEnumerable<HtmlMarkerUpdate> updates) => await _updateHtmlMarkersCallback.Invoke(updates);
    }
}
