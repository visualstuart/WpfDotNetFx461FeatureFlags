using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement.FeatureFilters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfDotNetFx461FeatureFlagsCommon;
using Microsoft.Extensions.Configuration.Json;

namespace WpfDotNetFx461FeatureFlagsLocalAppSettingsConfig
{

    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            // what was done in first local app config...

            //ServiceCollection services = new ServiceCollection();
            //services
            //    .AddConfigurationFromJson("appsettings.json")
            //    .AddFeatureManagement();

            //services.AddSingleton<MainWindow>();

            //host.Services = services.BuildServiceProvider();

            // what was done in Azure app config...

            var builder = Host.CreateDefaultBuilder();

            // configure configuration
            //builder
            //    .ConfigureAppConfiguration((hostingContext, configBuilder) =>
            //    {
            //        configBuilder
            //            .UseFeatureFlags();
            //    });

            // configure services
            builder
                .ConfigureServices((context, services) =>
                {
                    services
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
