namespace AzureMapsControl.Components
{
    using System;

    using AzureMapsControl.Components.Configuration;
    using AzureMapsControl.Components.Constants;
    using AzureMapsControl.Components.Map;

    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
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
                .AddSingleton<IMapAdderService>(sp => sp.GetRequiredService<MapService>())
                .AddSingleton<IMapService>(sp => sp.GetRequiredService<MapService>())
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
