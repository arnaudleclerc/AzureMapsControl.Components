namespace AzureMapsControl.Components
{
    using System;

    using AzureMapsControl.Components.Animations;
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
                .AddScoped<IAnimationService, AnimationService>()
                .AddOptions<AzureMapsConfiguration>()
                .Configure(configure)
                .Validate(configuration => configuration.Validate(), "The given AzureMapsConfiguration is invalid");

            return services;
        }

        internal static string ToAnimationsNamespace(this string method) => $"{JsConstants.Namespaces.AzureMapsControl}.{JsConstants.Namespaces.Animation}.{method}";
        internal static string ToCoreNamespace(this string method) => $"{JsConstants.Namespaces.AzureMapsControl}.{JsConstants.Namespaces.Core}.{method}";
        internal static string ToDrawingNamespace(this string method) => $"{JsConstants.Namespaces.AzureMapsControl}.{JsConstants.Namespaces.Drawing}.{method}";
        internal static string ToPopupNamespace(this string method) => $"{JsConstants.Namespaces.AzureMapsControl}.{JsConstants.Namespaces.Popup}.{method}";
        internal static string ToSourceNamespace(this string method) => $"{JsConstants.Namespaces.AzureMapsControl}.{JsConstants.Namespaces.Source}.{method}";

    }
}
