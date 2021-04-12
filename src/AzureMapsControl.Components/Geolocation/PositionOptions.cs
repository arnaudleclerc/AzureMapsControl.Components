namespace AzureMapsControl.Components.Geolocation
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class PositionOptions
    {
        public bool? EnableHighAccuracy { get; set; }
        public int? MaximumAge { get; set; }
        public int? Timeout { get; set; }
    }
}
