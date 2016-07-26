using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DomainLoader : MarshalByRefObject
    {
        public UserService LoadService(string type, IMaster master)
        {
            switch (type)
            {
                case "master":
                    return Master.GetInstance();
                case "slave":
                    return new Slave(master);
                default:
                    return null;
            }
        }

    }
}
