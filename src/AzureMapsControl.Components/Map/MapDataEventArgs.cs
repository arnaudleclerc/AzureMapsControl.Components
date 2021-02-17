namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Event object returned by the maps when a data event occurs.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MapDataEventArgs : MapEventArgs
    {
        /// <summary>
        /// The type of data that has changed.
        /// Either `"source"` or `"style"`
        /// </summary>
        public string DataType { get; }
        /// <summary>
        /// True if the event has a `dataType` of `"source"` and the source has no outstanding network requests.
        /// </summary>
        public bool? IsSourceLoaded { get; }
        /// <summary>
        /// If the `dataType` is `"source"` this is the related `Source` object.
        /// </summary>
        public Source Source { get; }
        /// <summary>
        /// Specified if the `dataType` is `"source"` and the event signals that internal data has been received or changed.
        /// Either `"metadata"` or `"content"`
        /// </summary>
        public string SourceDataType { get; }
        /// <summary>
        /// The tile being loaded or changed.
        /// Specified if `dataType` is `"source"` and the event is related to loading of a tile.
        /// </summary>
        public Tile Tile { get; }

        internal MapDataEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type)
        {
            DataType = eventArgs.DataType;
            IsSourceLoaded = eventArgs.IsSourceLoaded;
            Source = eventArgs.Source;
            SourceDataType = eventArgs.SourceDataType;
            Tile = eventArgs.Tile;
        }
    }
}
