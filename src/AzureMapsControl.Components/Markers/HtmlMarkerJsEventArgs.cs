using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Markers
{
    [ExcludeFromCodeCoverage]
    public sealed class HtmlMarkerJsEventArgs
    {
        public string MarkerId { get; set; }
        public string Type { get; set; }
    }
}
