using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Interfaces;
using DAL.Entities;
using NLog;

namespace BLL
{
    public class Slave : UserService, ISlave
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public IMaster Master { get; }
        public Slave(IMaster master)
        {
            logger.Trace("Create slave for master");
            if (ReferenceEquals(master, null))
            {
                logger.Error("master reference is null");
                throw new ArgumentNullException();
            }
            if (master.NumberOfSlaves > 0)
            {
                Master = master;
                Master.ActionOnAdd += Update;
                Master.ActionOnDelete += Update;
                Master.NumberOfSlaves--;
            }
            else
            {
                logger.Error("master doesn't have available place for slave");
                throw new ArgumentException();
            }
            logger.Trace("Create slave for master");
        }


        public override int Add(BllUser user)
        {
            logger.Error("Slave try to add user");
            throw new NotImplementedException();
        }

        public override void Delete(BllUser user)
        {
            logger.Error("Slave try to delete user");
            throw new NotImplementedException();
        }

        public void Update()
        {
            logger.Trace("Slave update information");
            repository.Users = Master.GetRepository().Users;
            Save();
        }
    }
}
