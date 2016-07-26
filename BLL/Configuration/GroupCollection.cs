namespace BLL.Configuration
{
    using System.Configuration;

    [ConfigurationCollection(typeof(GroupElement))]
    public class GroupCollection : ConfigurationElementCollection
    {
        public GroupElement this[int idx] => (GroupElement)BaseGet(idx);

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
