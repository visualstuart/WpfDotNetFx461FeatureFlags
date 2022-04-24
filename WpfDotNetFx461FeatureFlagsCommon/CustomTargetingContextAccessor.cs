using Microsoft.FeatureManagement.FeatureFilters;
using System;
using System.Threading.Tasks;

namespace WpfDotNetFx461FeatureFlagsCommon
{
    public class CustomTargetingContextAccessor : ITargetingContextAccessor
    {
        private readonly ICustomContextAccessor contextAccessor;

        public CustomTargetingContextAccessor(ICustomContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor ??
                throw new ArgumentNullException(nameof(contextAccessor));
        }

        public ValueTask<TargetingContext> GetContextAsync()
        {
            string name = contextAccessor.User.Name;
            TargetingContext targetingContext = new TargetingContext { UserId = name };
            return new ValueTask<TargetingContext>(targetingContext);
        }
    }

}
