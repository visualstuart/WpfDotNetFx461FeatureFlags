namespace WpfDotNetFx461FeatureFlagsCommon
{
    public class CustomUser : ICustomUser
    {
        public string Name { get; }

        public CustomUser(string name)
        {
            Name = name;
        }
    }

}
