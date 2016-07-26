namespace BLL.Configuration
{
    using System.Configuration;

    public class GroupElement : ConfigurationElement
    {
        [ConfigurationProperty("groupType", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string GroupType
        {
            get { return (string)base["groupType"]; }
            set { base["groupType"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = false)]
        public string Path
        {
            get { return (string)base["path"]; }
            set { base["path"] = value; }
        }
    }
}
