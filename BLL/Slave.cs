namespace BLL
{
    using System;
    using System.Linq;
    using BLL.Entities;
    using BLL.Interfaces;
    using NLog;

    public class Slave : UserService, ISlave
    {
        private static readonly Logger Loger = LogManager.GetCurrentClassLogger();
        public IMaster Master { get; }
        public Slave(IMaster master)
        {
            Loger.Trace("Create slave for master");
            if (ReferenceEquals(master, null))
            {
                Loger.Error("master reference is null");
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
                Loger.Error("master doesn't have available place for slave");
                throw new ArgumentException();
            }
            Loger.Trace("Create slave for master");
        }


        public override int Add(BllUser user)
        {
            Loger.Error("Slave try to add user");
            throw new NotImplementedException();
        }

        public override void Delete(BllUser user)
        {
            Loger.Error("Slave try to delete user");
            throw new NotImplementedException();
        }

        public void Update()
        {
            Loger.Trace("Slave update information");
            repository.Users = (Master as UserService).GetAllUsers().Select(u=>u.ToDalUser()).ToList();
            //Save();
        }
    }
}
