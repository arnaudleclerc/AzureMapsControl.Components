namespace AzureMapsControl.Components.Atlas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Map
    {
        public string Id { get; }

        internal Func<IEnumerable<Control>, Task> AddControlsCallback { get; }

        internal Map(string id, Func<IEnumerable<Control>, Task> addControlsCallback)
        {
            Id = id;
            AddControlsCallback = addControlsCallback;
        }

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(params Control[] controls) => await AddControlsCallback(controls?.Where(control => control != null));

        /// <summary>
        /// Adds controls to the map
        /// </summary>
        /// <param name="controls">Controls to add to the map</param>
        public async Task AddControlsAsync(IEnumerable<Control> controls) => await AddControlsCallback(controls?.Where(control => control != null));

    }
}
