using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using System.Windows;
using WpfDotNetFx461FeatureFlagsCommon;

namespace WpfDotNetFx461FeatureFlagsLocalAppSettingsConfig
{
    public partial class MainWindow : Window
    {
        public bool TeamOneWipEnabled { get; set; }

        public MainWindow(
            IFeatureManager featureManager,
            ITargetingContextAccessor targetingContextAccessor)
        {
            TargetingContext context = targetingContextAccessor.GetContextAsync().Result;
            TeamOneWipEnabled = featureManager.IsEnabledAsync(FeatureFlags.TeamOneWIP, context).Result;

            InitializeComponent();
            DataContext = this;
        }
    }
}
