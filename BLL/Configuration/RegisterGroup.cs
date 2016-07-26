using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BLL.Configuration
{
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
