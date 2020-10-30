namespace AzureMapsControl.Components
{
    using System;

    using AzureMapsControl.Components.Configuration;
    using AzureMapsControl.Components.Constants;
    using AzureMapsControl.Components.Map;

    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        internal const string MethodAddMap = "addMap";
        internal const string MethodAddControl = "addControls";
        internal const string MethodAddHtmlMarkers = "addHtmlMarkers";
        internal const string MethodSetOptions = "setOptions";
        internal const string MethodRemoveHtmlMarkers = "removeHtmlMarkers";
        internal const string MethodUpdateHtmlMarkers = "updateHtmlMarkers";
        internal const string MethodAddDrawingToolbar = "addDrawingToolbar";
        internal const string MethodUpdateDrawingToolbar = "updateDrawingToolbar";
        internal const string MethodAddLayer = "addLayer";
        internal const string MethodRemoveLayers = "removeLayers";
        internal const string MethodAddDataSource = "addDataSource";
        internal const string MethodDataSourceImportDataFromUrl = "dataSource_importDataFromUrl";

        /// <summary>
        /// Register the configuration to use the AzureMapsControl components
        /// </summary>
        /// <param name="services">Current list of services</param>
        /// <param name="configure">Configuration</param>
        /// <returns>Services</returns>
        public static IServiceCollection AddAzureMapsControl(this IServiceCollection services, Action<AzureMapsConfiguration> configure)
        {
            services
                .AddSingleton<MapService>()
                .AddOptions<AzureMapsConfiguration>()
                .Configure(configure);

            return services;
        }

        /// <summary>
        /// Formats the given Js Interop method to the namespace specific method
        /// </summary>
        /// <param name="method">Method</param>
        /// <returns>JsInterop method with namespace</returns>
        internal static string ToAzureMapsControlNamespace(this string method) => $"{JsConstants.Namespace}.{method}";
    }
}
