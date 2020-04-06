namespace AzureMapsControl.Components.Markers
{
    using System.Collections.Generic;

    internal class HtmlMarkerCreationOptions
    {
        public string Id { get; set; }
        public IEnumerable<string> Events { get; set; }
        public HtmlMarkerOptions Options { get; set; }
    }
}
