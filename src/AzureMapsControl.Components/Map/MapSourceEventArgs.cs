namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    /// <summary>
    /// Event object returned when a source event occurs
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class MapSourceEventArgs : MapEventArgs
    {
        /// <summary>
        /// The source for which the event was triggered
        /// </summary>
        public Source Source { get; }

        internal MapSourceEventArgs(Map map, MapJsEventArgs eventArgs) : base(map, eventArgs.Type) => Source = eventArgs.Source;
    }
}
