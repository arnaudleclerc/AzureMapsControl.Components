namespace AzureMapsControl.Components.Markers
{
    using System.Collections.Generic;

    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal class HtmlMarkerCreationOptions : HtmlMarkerUpdateOptions
    {
        public IEnumerable<string> Events { get; set; }
    }
}
