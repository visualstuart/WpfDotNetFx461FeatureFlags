using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WpfDotNetFx461FeatureFlagsLocalAppSettingsConfig
{
    class CustomFeatureManager : IFeatureManager
    {
        private const string SectionName = "FeatureManagement";

        private readonly IConfigurationSection section;

        public CustomFeatureManager(IConfiguration configuration)
        {
            section = configuration.GetSection(SectionName);
        }

        // Unfiltered feature

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<bool> IsEnabledAsync(string feature) =>
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            IsEnabled(feature);

        public bool IsEnabled(object feature) =>
            IsEnabled(GetFeatureName(feature));

        private bool IsEnabled(string name) =>
            bool.TryParse(section[name], out bool enabled) && enabled;

        // Filtered feature

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<bool> IsEnabledAsync<TContext>(string feature, TContext context) =>
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            IsEnabled(feature, context);

        public bool IsEnabled<TContext>(object feature, TContext context) =>
            IsEnabled(GetFeatureName(feature), context);

        private bool IsEnabled<TContext>(string feature, TContext context)
        {
            TargetingContext targetingContext = context as TargetingContext ??
                throw new InvalidOperationException("Currently only TargetingContext context type is supported.");

            IConfigurationSection enabledForSection = 
                section.GetSection($"{feature}:EnabledFor");
            EnabledFor[] enabledFors = 
                enabledForSection.Get<EnabledFor[]>();

            // currently only checking audience users, not groups
            foreach(var enabledFor in enabledFors)
            {
                if (enabledFor.Name == "Targeting")
                {
                    // see https://github.com/microsoft/FeatureManagement-Dotnet/blob/main/src/Microsoft.FeatureManagement/Targeting/ContextualTargetingFilter.cs
                    Parameters settings = enabledFor.Parameters;
                    if (targetingContext.UserId != null &&
                        settings.Audience.Users != null &&
                        settings.Audience.Users.Any(user => targetingContext.UserId.Equals(user)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static string GetFeatureName(object feature)
        {
            var featureType = feature.GetType();
            if (!featureType.IsEnum)
            {
                throw new ArgumentException($"{nameof(feature)} must be an enum.");
            }

            return Enum.GetName(featureType, feature);
        }

        public IAsyncEnumerable<string> GetFeatureNamesAsync()
        {
            throw new NotImplementedException();
        }

        // Serialization classes for feature management configuration

        public class EnabledFor
        {
            public string Name { get; set; }
            public Parameters Parameters { get; set; }
        }

        public class Parameters
        {
            public Audience Audience { get; set; }
        }
    }
}
