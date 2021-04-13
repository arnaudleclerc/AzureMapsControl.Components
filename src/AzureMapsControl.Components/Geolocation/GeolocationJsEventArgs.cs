namespace AzureMapsControl.Components.Geolocation
{
    using System.Diagnostics.CodeAnalysis;

    using AzureMapsControl.Components.Atlas;

    [ExcludeFromCodeCoverage]
    internal class GeolocationJsEventArgs
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        public Feature<Point> Feature { get; set; }
        public string Type { get; set; }
    }
}
