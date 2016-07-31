using BLL.Communication;

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

       public Slave(IMaster master)
        {
            Loger.Trace("Create slave for master");
            if (object.ReferenceEquals(master, null))
            {
                Loger.Error("master reference is null");
                throw new ArgumentNullException();
            }

            if (master.NumberOfSlaves > 0)
            {
                this.Master = master;
                this.Master.ActionOnAdd += this.Update;
                this.Master.ActionOnDelete += this.Update;
                this.Master.NumberOfSlaves--;
            }
            else
            {
                Loger.Error("master doesn't have available place for slave");
                throw new ArgumentException();
            }

            Loger.Trace("Create slave for master");
        }

        public IMaster Master { get; }

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
            var userRepository = this.Master as UserService;
            if (userRepository != null)
            {
                repository.Users = userRepository.GetAllUsers().Select(u => u.ToDalUser()).ToList();
            }
        }

        public override void AddCommunicator(Communicator communicator)
        {
            base.AddCommunicator(communicator);
            Communicator.UserAdded += Update;
            Communicator.UserDeleted += Update;
        }
    }
}
