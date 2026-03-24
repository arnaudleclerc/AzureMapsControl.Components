
using System.Threading.Tasks;

namespace AzureMapsControl.Components.Geolocation;
public interface IGeolocationService
{
    /// <summary>
    /// Checks to see if the geolocation API is supported in the browser.
    /// </summary>
    /// <returns>True if the geolocation API is supported in the browser, otherwise false</returns>
    ValueTask<bool> IsGeolocationSupportedAsync();
}
