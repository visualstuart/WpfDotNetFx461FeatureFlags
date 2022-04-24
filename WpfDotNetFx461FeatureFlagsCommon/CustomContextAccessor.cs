namespace WpfDotNetFx461FeatureFlagsCommon
{
    public class CustomContextAccessor : ICustomContextAccessor
    {
        public ICustomUser User { get; }

        public CustomContextAccessor(string name)
        {
            User = new CustomUser(name);
        }
    }
}
