using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete;
using DAL.Entities;
using DAL.Interfaces;
using NLog;

namespace BLL.Interfaces
{
    public abstract class UserService
    {
        protected IUserRepository repository;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public UserService()
        {
            logger.Trace("Create new Service");
            repository = new UserRepository();
        }
        public abstract int Add(BllUser user);

        public abstract void Delete(BllUser user);

        public List<BllUser> GetAllUsers()
        {
            logger.Trace("GetAllUsers()");
            return repository.GetAll().Select(user => user.ToBllUser()).ToList();
        }

        public int[] SearchForUsers(Func<BllUser, bool> criteria)
        {
            if (ReferenceEquals(criteria, null))
            {
                logger.Error("Search criteria is null");
                throw new ArgumentNullException();
            }
            logger.Trace("SearchForUsers()");
            Func<User, bool> predicate = user => criteria.Invoke(user.ToBllUser());
            return repository.SearchForUsers(predicate);
        }

        public void Save()
        {
            logger.Trace("Save()");
            repository.Save();
        }

        public void Load()
        {
            logger.Trace("Save()");
            repository.Load();
        }


    }
}
