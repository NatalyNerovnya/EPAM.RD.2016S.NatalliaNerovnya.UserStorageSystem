namespace BLL.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Take group of elements 
    /// </summary>
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
