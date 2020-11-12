namespace AzureMapsControl.Components.Data
{
    using System;

    public abstract class Source
    {
        internal SourceType SourceType { get; }

        /// <summary>
        /// A unique id that the user assigns to the data source
        /// </summary>
        public string Id { get; }

        internal Source(string id, SourceType type)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            SourceType = type;
        }

        internal abstract SourceOptions GetSourceOptions();

    }

    public abstract class Source<TOptions>: Source where TOptions : SourceOptions
    {
        /// <summary>
        /// Options of the source
        /// </summary>
        public TOptions Options { get; set; }

        internal Source(string id, SourceType type): base(id, type) { }

        internal override SourceOptions GetSourceOptions() => Options;
    }
}
