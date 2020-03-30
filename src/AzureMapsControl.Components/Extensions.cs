namespace AzureMapsControl
{
    using System;
    using AzureMapsControl.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        public static IServiceCollection AddAzureMapsControl(this IServiceCollection services, Action<AzureMapConfiguration> configure)
        {
            services
                .AddOptions<AzureMapConfiguration>()
                .Configure(configure)
                .Validate(configuration => configuration.Validate());

            return services;
        }
    }
}
