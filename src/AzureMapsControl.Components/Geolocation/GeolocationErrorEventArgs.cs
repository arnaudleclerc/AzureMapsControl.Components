namespace AzureMapsControl.Components.Geolocation
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class GeolocationErrorEventArgs
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        internal GeolocationErrorEventArgs(GeolocationJsEventArgs eventArgs)
        {
            Code = eventArgs.Code;
            Message = eventArgs.Message;
        }
    }
}
