namespace BLL
{
    using System;
    using BLL.Interfaces;

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
