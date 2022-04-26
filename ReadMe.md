# Sample: Feature Management on WPF on .NET Framework 4.6.1

24 April 2022

This sample demonstrates using Microsoft.FeatureManagement 2.5.1 (latest) in a WPF desktop application
that targets .NET Framework 4.6.1.
The sample uses conditional feature flags, where the condition is a **targeting filter**
and the targeted audience is an allow list of user IDs.

There are two sample applications:
* WpfDotNetFx461FeatureFlagsAzureAppConfig loads feature flags from Azure App Configuration;
* WpfDotNetFx461FeatureFlagsLocalAppSettingsConfig loads feature flags from the local AppSettings.json file.

The library WpfDotNetFx461FeatureFlagsCommon contains a few types used in both applications.
* `ICustomUser`/`CustomUser` defines a custom user type for expressing the user's identity.
* `ICustomContextAccessor`/`CustomContextAccessor` provides access to a custom user.
* `CustomTargetingContextAccessor` provides access to the custom targeting context for feature filtering.

If the user identity is passed in a regular targeting context, most of these custom context types wouldn't be necessary.

**Notes on version compatibility.** 
* .NET Framework 4.6.1 supports .NET Standard 2.0 and no higher,
see [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0) in the "Select .NET Standard version" section.
* Microsoft.Framework 2.5.1 (latest) supports .NET Standard 2.0
* Microsoft.Extentions.Configuration.AzureAppConfiguration 5.0.0 (latest)
supports .NET Standard 2.0.


# Azure App Configuration

WpfDotNetFx461FeatureFlagsAzureAppConfig needs an Azure App Configuration connection string in the `App` constructor in `App.xaml.cs`.

# Local AppSettings.json Configuration

WpfDotNetFx461FeatureFlagsLocalAppSettingsConfig defines a `CustomFeatureManager`
that derives from `IFeatureManager` which implement feature management over 
configuration loaded from AppSettings.json.

# Caveats

Does not yet implement groups or percentages in the targeting filter.
Groups seems straightforward. Percentages, however, require a persistent store
in order for feature enablement to be 'sticky' for the user:
once the feature is enabled for a user, it continues to be enabled for that user.

Does not implement other filter types.

# References

## Microsoft.FeatureManagement source code

* FeatureManagement-Dotnet / src / Microsoft.FeatureManagement
  * [Targeting](https://github.com/microsoft/FeatureManagement-Dotnet/tree/main/src/Microsoft.FeatureManagement/Targeting)
    * [ContextualTargetingFilter.cs](https://github.com/microsoft/FeatureManagement-Dotnet/blob/main/src/Microsoft.FeatureManagement/Targeting/ContextualTargetingFilter.cs)
Implements the targeting filter for a regular context.

## Microsoft docs

Microsoft.Extensions.Configuration

* [Configuration in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration)

Azure App Configuration

* [Use feature filters to enable conditional feature flags](https://docs.microsoft.com/en-us/azure/azure-app-configuration/howto-feature-filters-aspnet-core)


