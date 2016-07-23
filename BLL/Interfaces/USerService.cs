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

namespace BLL.Interfaces
{
    public abstract class UserService
    {
        protected IUserRepository repository;

        public UserService()
        {
            repository = new UserRepository();
        }
        public abstract int Add(BllUser user);

        public abstract void Delete(BllUser user);

        public List<BllUser> GetAllUsers()
        {
            return repository.GetAll().Select(user => user.ToBllUser()).ToList();
        }

        public int[] SearchForUsers(Func<BllUser, bool> criteria)
        {
            if (ReferenceEquals(criteria, null))
            throw new ArgumentNullException();

            Func<User, bool> predicate = user => criteria.Invoke(user.ToBllUser());
            return repository.SearchForUsers(predicate);
        }

        public void Save()
        {
            repository.Save();
        }

        public void Load()
        {
            repository.Load();
        }


    }
}
