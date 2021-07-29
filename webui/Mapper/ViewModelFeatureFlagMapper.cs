namespace webui.Mapper
{
    public class ViewModelFeatureFlagMapper
    {
        public static Models.FeatureFlagViewModel Map(string featureFlagKey)
        {
            return new Models.FeatureFlagViewModel
            {
                Key = featureFlagKey,
            };
        }
    }
}
