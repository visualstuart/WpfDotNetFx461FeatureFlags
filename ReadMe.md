# Sample: Feature Management on WPF on .NET Framework 4.1.6

This sample demonstrates using Microsoft.FeatureManagement 2.2.0 in a WPF desktop application
that targets .NET Framework 4.1.6.
The sample uses conditional feature flags, where the condition is a **targeting filter**
and the targeted audience is an allow list of user IDs.

There are two sample applications:
* WpfDotNetFx461FeatureFlagsAzureAppConfig loads feature flags from Azure App Configuration;
* WpfDotNetFx461FeatureFlagsLocalAppSettingsConfig loads feature flags from the local AppSettings.json file.

The library WpfDotNetFx461FeatureFlagsCommon contains a few types used in both applications.
* `ICustomUser`/`CustomUser` defines a custom user type for expressing the user's identity.
* `ICustomContextAccessor`/`CustomContextAccessor` provides access to a custom user.
* `CustomTargetingContextAccessor` provides access to the custom targeting context for feature filtering.


# Azure App Configuration

WpfDotNetFx461FeatureFlagsAzureAppConfig needs an Azure App Configuration connection string in the `App` constructor in `App.xaml.cs`.

# Local AppSettings.json Configuration

WpfDotNetFx461FeatureFlagsLocalAppSettingsConfig defines a `CustomFeatureManager`
that derives from `IFeatureManager` which implement feature management over 
configuration loaded from AppSettings.json.



