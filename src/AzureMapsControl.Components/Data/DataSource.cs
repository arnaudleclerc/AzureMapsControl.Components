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
        internal Func<string, IEnumerable<string>, Task> RemoveCallback { get; set; }
        internal Func<string, Task> ClearCallback { get; set; }

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

        /// <summary>
        /// Add geometries to the data source
        /// </summary>
        /// <param name="geometries">Geometries to add</param>
        /// <returns></returns>
        public async Task AddAsync(IEnumerable<Geometry> geometries)
        {
            if (geometries == null || !geometries.Any())
            {
                return;
            }

            if (_geometries == null)
            {
                _geometries = new List<Geometry>();
            }

            _geometries.AddRange(geometries);
            await AddCallback.Invoke(Id, geometries);
        }

        /// <summary>
        /// Add geometries to the data source
        /// </summary>
        /// <param name="geometries">Geometries to add</param>
        /// <returns></returns>
        public async Task AddAsync(params Geometry[] geometries) => await AddAsync(geometries as IEnumerable<Geometry>);

        /// <summary>
        /// Remove geometries from the data source
        /// </summary>
        /// <param name="geometryIds">IDs of the geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<string> geometryIds)
        {
            if (_geometries != null)
            {
                var ids = _geometries.Where(geometry => geometryIds.Contains(geometry.Id)).Select(geometry => geometry.Id);
                if (ids.Any())
                {
                    await RemoveCallback.Invoke(Id, ids);
                    _geometries.RemoveAll(geometry => geometryIds.Contains(geometry.Id));
                }
            }
        }

        /// <summary>
        /// Remove geometries from the data source
        /// </summary>
        /// <param name="geometryIds">IDs of the geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(params string[] geometryIds) => await RemoveAsync(geometryIds as IEnumerable<string>);

        /// <summary>
        /// Remove geometries from the datasource
        /// </summary>
        /// <param name="geometries">Geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<Geometry> geometries) => await RemoveAsync(geometries.Select(g => g.Id));

        /// <summary>
        /// Remove geometries from the data source
        /// </summary>
        /// <param name="geometries">Geometries to remove</param>
        /// <returns></returns>
        public async Task RemoveAsync(params Geometry[] geometries) => await RemoveAsync(geometries.Select(g => g.Id));

        /// <summary>
        /// Imports data from an URL into a data source
        /// </summary>
        /// <param name="dataSourceId">ID of the data source on which the data will be imported</param>
        /// <param name="url">Url to import the data from</param>
        /// <returns></returns>
        public async Task ImportDataFromUrlAsync(string url) => await ImportDataFromUrlCallback.Invoke(Id, url);

        /// <summary>
        /// Clear the datasource
        /// </summary>
        /// <returns></returns>
        public async Task ClearAsync()
        {
            await ClearCallback.Invoke(Id);
            _geometries = null;
        }
    }
}
