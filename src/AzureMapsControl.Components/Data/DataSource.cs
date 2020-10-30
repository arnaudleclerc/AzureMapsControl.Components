namespace AzureMapsControl.Components.Data
{
    using System;

    public sealed class DataSource
    {
        /// <summary>
        /// A unique id that the user assigns to the data source
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The options for the data source
        /// </summary>
        public DataSourceOptions Options { get; set; }

        public DataSource() : this(Guid.NewGuid().ToString()) { }

        public DataSource(string id) => Id = id;
    }
}
