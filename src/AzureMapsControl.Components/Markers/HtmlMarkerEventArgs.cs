namespace AzureMapsControl.Components.Markers
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Map;

    [ExcludeFromCodeCoverage]
    public sealed class HtmlMarkerEventArgs : MapEventArgs
    {
        public HtmlMarker HtmlMarker { get; }
        internal HtmlMarkerEventArgs(Map map, string type, HtmlMarker htmlMarker) : base(map, type) => HtmlMarker = htmlMarker;
    }
}
