
using System.Diagnostics.CodeAnalysis;

namespace AzureMapsControl.Components.Geolocation;
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
