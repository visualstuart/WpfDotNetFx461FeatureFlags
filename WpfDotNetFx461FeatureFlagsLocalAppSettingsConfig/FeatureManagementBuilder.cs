using Microsoft.Extensions.DependencyInjection;

namespace WpfDotNetFx461FeatureFlagsLocalAppSettingsConfig
{
    internal class FeatureManagementBuilder : IFeatureManagementBuilder
    {
        public FeatureManagementBuilder(IServiceCollection _)
        {
        }

        public IFeatureManagementBuilder AddFeatureFilter<TargetingFilter>()
        {
            return this;
        }
    }
}
