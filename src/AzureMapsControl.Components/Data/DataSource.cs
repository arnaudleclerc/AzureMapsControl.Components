namespace AzureMapsControl.Components.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Atlas;

    public sealed class DataSource
    {
        private List<Geometry> _geometries;

        internal Func<string, string, Task> ImportDataFromUrlCallback { get; set; }
        internal Func<string, IEnumerable<Geometry>, Task> AddCallback { get; set; }

        /// <summary>
        /// A unique id that the user assigns to the data source
        /// </summary>
        public string Id { get; }

        public IEnumerable<Geometry> Geometries => _geometries;

        /// <summary>
        /// The options for the data source
        /// </summary>
        public DataSourceOptions Options { get; set; }

        public DataSource() : this(Guid.NewGuid().ToString()) { }

        public DataSource(string id) => Id = id;

        public async Task AddAsync(IEnumerable<Geometry> geometries) => await AddAsync(geometries.ToArray());

        public async Task AddAsync(params Geometry[] geometries)
        {
            if (_geometries == null)
            {
                _geometries = new List<Geometry>();
            }

            _geometries.AddRange(geometries);
            await AddCallback.Invoke(Id, geometries);
        }

        /// <summary>
        /// Imports data from an URL into a data source
        /// </summary>
        /// <param name="dataSourceId">ID of the data source on which the data will be imported</param>
        /// <param name="url">Url to import the data from</param>
        /// <returns></returns>
        public async Task ImportDataFromUrlAsync(string url) => await ImportDataFromUrlCallback.Invoke(Id, url);
    }
}
