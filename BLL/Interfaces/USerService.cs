namespace BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Communication;
    using DAL.Concrete;
    using DAL.Entities;
    using DAL.Interfaces;
    using Entities;
    using NLog;

    public abstract class UserService : MarshalByRefObject
    {
        protected IUserRepository repository;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private Communicator communicator;

        public UserService()
        {
            this.logger.Trace("Create new Service");
            this.repository = new UserRepository();
        }

        public virtual Communicator Communicator
        {
            get
            {
                return this.communicator;
            }

            set
            {
                if (object.ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException(nameof(value) + " is null!");
                }

                this.communicator = value;
            }
        }     

        public abstract int Add(BllUser user);

        public abstract void Delete(BllUser user);

        public List<BllUser> GetAllUsers()
        {
            this.logger.Trace("GetAllUsers()");
            return this.repository.GetAll().Select(user => user.ToBllUser()).ToList();
        }

        public int[] SearchForUsers(Func<BllUser, bool> criteria)
        {
            if (object.ReferenceEquals(criteria, null))
            {
                this.logger.Error("Search criteria is null");
                throw new ArgumentNullException();
            }

            this.logger.Trace("SearchForUsers()");
            Func<User, bool> predicate = user => criteria.Invoke(user.ToBllUser());
            return this.repository.SearchForUsers(predicate);
        }

        public void Save()
        {
            this.logger.Trace("Save()");
            this.repository.Save();
        }

        public void Load()
        {
            this.logger.Trace("Save()");
            this.repository.Load();
        }

        public virtual void AddCommunicator(Communicator communicator)
        {
            if (communicator == null)
            {
                return;
            }

            Communicator = communicator;
        }
    }
}
