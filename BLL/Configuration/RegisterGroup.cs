namespace BLL.Configuration
{
    using System.Configuration;

    public class RegisterGroup : ConfigurationSection
    {
        [ConfigurationProperty("Group")]
        public GroupCollection GroupItems => (GroupCollection)base["Group"];

        public static RegisterGroup GetConfig()
        {
            return (RegisterGroup)ConfigurationManager.GetSection("RegisterGroups") ??
                   new RegisterGroup();
        }
    }
}
