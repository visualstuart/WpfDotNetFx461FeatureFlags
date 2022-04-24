using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using System;
using System.Windows;
using WpfDotNetFx461FeatureFlagsCommon;

namespace WpfDotNetFx461FeatureFlagsAzureAppConfig
{
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            var builder = Host.CreateDefaultBuilder();

            // configure configuration
            builder
                .ConfigureAppConfiguration((hostingContext, configBuilder) =>
                {
                    var connectionString =
                        "Endpoint=https://celarierappconfig00.azconfig.io;Id=IpUi-l2-s0:gHQ8LtGb6NhL/SREasA/;Secret=GFUUuSQYi7Kdv7Tp3VQSzzE5EbyDVsKo2DZQ6x+pLyU=";
                        //configBuilder.Build().GetConnectionString("AppConfig");

                    // configure loading configuration from Azure App Configuration
                    //  and specify the option to use feature flags from that configuration
                    configBuilder
                        .AddAzureAppConfiguration(options => options
                            .Connect(connectionString)
                            .UseFeatureFlags());
                });

            // configure services
            builder
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddAzureAppConfiguration()
                        .AddFeatureManagement()
                        .AddFeatureFilter<TargetingFilter>();

                    services.AddSingleton<MainWindow>();

                // hardcode the user for now...
                services.AddSingleton<
                    ICustomContextAccessor>(new CustomContextAccessor("test@contoso.com"));
                services.AddSingleton<
                    ITargetingContextAccessor, CustomTargetingContextAccessor>();
                });

            // build the host
            host = builder.Build();
        }

        private async void Application_StartupAsync(object sender, StartupEventArgs e)
        {
            await host.StartAsync();

            host.Services.GetService<MainWindow>()?.Show();
        }

        private async void Application_ExitAsync(object sender, ExitEventArgs e)
        {
            await host.StopAsync(TimeSpan.FromSeconds(5));
        }
    }
}
