namespace BLL
{
    using System;
    using System.Linq;
    using Communication;
    using Entities;
    using Interfaces;
    using NLog;

    /// <summary>
    /// Slave service
    /// </summary>
    public class Slave : UserService, ISlave
    {
        private static readonly Logger Loger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="master">Master of the groupe</param>
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

        public IMaster Master
        {
            get;
        }

        /// <summary>
        /// For communication work
        /// </summary>
        public override Communicator Communicator
        {
            get
            {
                return base.Communicator;
            }

            set
            {
                base.Communicator = value;
                this.Communicator.UserAdded += this.Update;
                this.Communicator.UserDeleted += this.Update;
            }
        }

        /// <summary>
        /// Throw exception
        /// </summary>
        /// <param name="user">user to be add</param>
        /// <returns>NotImplementedException</returns>
        public override int Add(BllUser user)
        {
            Loger.Error("Slave try to add user");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Throw exception
        /// </summary>
        /// <param name="user">user to be deleted</param>
        public override void Delete(BllUser user)
        {
            Loger.Error("Slave try to delete user");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update repository 
        /// </summary>
        public void Update()
        {
            Loger.Trace("Slave update information");
            var userRepository = this.Master as UserService;
            if (userRepository != null)
            {
                repository.Users = userRepository.GetAllUsers().Select(u => u.ToDalUser()).ToList();
            }
        }

        /// <summary>
        /// Begin lisstening master
        /// </summary>
        /// <param name="communicator"></param>
        public override void AddCommunicator(Communicator communicator)
        {
            base.AddCommunicator(communicator);
            this.Communicator.UserAdded += this.Update;
            this.Communicator.UserDeleted += this.Update;
        }
    }
}
