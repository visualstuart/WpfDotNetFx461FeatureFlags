using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace WpfDotNetFx461FeatureFlagsLocalAppSettingsConfig
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds required feature management services.
        /// </summary>
        /// <param name="services">The service collection that feature management services are added to.</param>
        /// <returns>A <see cref="IFeatureManagementBuilder"/> that can be used to customize feature management functionality.</returns>
        public static IFeatureManagementBuilder AddFeatureManagement(this IServiceCollection services)
        {
            // Add required services
            services.AddSingleton<IFeatureManager, CustomFeatureManager>();

            return new FeatureManagementBuilder(services);
        }

        public static IServiceCollection AddConfigurationFromJson(this IServiceCollection services, string path)
        {
            services.AddSingleton(new ConfigurationFromJson(path).Build());
            return services;
        }
    }
}
