namespace BLL.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Configure services
    /// </summary>
    [ConfigurationCollection(typeof(GroupElement))]
    public class GroupCollection : ConfigurationElementCollection
    {
        public GroupElement this[int idx]
        {
            get { return BaseGet(idx) as GroupElement; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GroupElement)element).Path;
        }
    }
}
