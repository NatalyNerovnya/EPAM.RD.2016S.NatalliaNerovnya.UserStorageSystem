namespace BLL
{
    using System;
    using Interfaces;

    /// <summary>
    /// class for loading data about services from config file
    /// </summary>
    public class DomainLoader : MarshalByRefObject
    {
        /// <summary>
        /// Load service
        /// </summary>
        /// <param name="type">Type from config file</param>
        /// <param name="master">Reference on master</param>
        /// <returns>service ( master or slave )</returns>
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
