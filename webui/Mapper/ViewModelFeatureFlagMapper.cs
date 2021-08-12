namespace webui.Mapper
{
    public class ViewModelFeatureFlagMapper
    {
        public static Models.FeatureFlagViewModel Map(Service.Models.FeatureFlagDTO featureFlagKey)
        {
            return new Models.FeatureFlagViewModel
            {
                Key = featureFlagKey.Key,
                State = featureFlagKey.State
            };
        }
    }
}
