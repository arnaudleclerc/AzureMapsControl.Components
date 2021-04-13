namespace AzureMapsControl.Components.Geolocation
{
    using System.Threading.Tasks;

    public interface IGeolocationService
    {
        /// <summary>
        /// Checks to see if the geolocation API is supported in the browser.
        /// </summary>
        /// <returns>True if the geolocation API is supported in the browser, otherwise false</returns>
        Task<bool> IsGeolocationSupportedAsync();
    }
}
