namespace AzureMapsControl.Components.Markers
{
    using AzureMapsControl.Components.Map;

    public class HtmlMarkerEventArgs : MapEventArgs
    {
        public HtmlMarker HtmlMarker { get; }
        internal HtmlMarkerEventArgs(Map map, string type, HtmlMarker htmlMarker) : base(map, type) => HtmlMarker = htmlMarker;
    }
}
