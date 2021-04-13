namespace AzureMapsControl.Components.Geolocation
{
    using System.Threading.Tasks;

    using AzureMapsControl.Components.Logger;
    using AzureMapsControl.Components.Runtime;

    using Microsoft.Extensions.Logging;

    internal class GeolocationService : IGeolocationService
    {
        private readonly IMapJsRuntime _mapJsRuntime;
        private readonly ILogger<GeolocationService> _logger;

        public GeolocationService(IMapJsRuntime mapJsRuntime, ILogger<GeolocationService> logger)
        {
            _mapJsRuntime = mapJsRuntime;
            _logger = logger;
        }

        /// <summary>
        /// Checks to see if the geolocation API is supported in the browser.
        /// </summary>
        /// <returns>True if the geolocation API is supported in the browser, otherwise false</returns>
        public async ValueTask<bool> IsGeolocationSupportedAsync()
        {
            _logger?.LogAzureMapsControlInfo(AzureMapLogEvent.GeolocationService_IsGeolocationSupportedAsync, "GeolocationService - IsGeolocationSupportedAsync");
            return await _mapJsRuntime.InvokeAsync<bool>(Constants.JsConstants.Methods.GeolocationControl.IsGeolocationSupported.ToGeolocationControlNamespace());
        }
    }
}
