
using System.Threading.Tasks;

using AzureMapsControl.Components.Logger;
using AzureMapsControl.Components.Runtime;

using Microsoft.Extensions.Logging;

namespace AzureMapsControl.Components.Geolocation;
internal sealed class GeolocationService(IMapJsRuntime mapJsRuntime, ILogger<GeolocationService> logger) : IGeolocationService
{
    /// <summary>
    /// Checks to see if the geolocation API is supported in the browser.
    /// </summary>
    /// <returns>True if the geolocation API is supported in the browser, otherwise false</returns>
    public async ValueTask<bool> IsGeolocationSupportedAsync()
    {
        logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GeolocationService_IsGeolocationSupportedAsync, "GeolocationService - IsGeolocationSupportedAsync");
        return await mapJsRuntime.InvokeAsync<bool>(Constants.JsConstants.Methods.GeolocationControl.IsGeolocationSupported.ToGeolocationControlNamespace()).ConfigureAwait(false);
    }
}
