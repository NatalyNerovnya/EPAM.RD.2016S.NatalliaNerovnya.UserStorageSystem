using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        List<User> Users { get; set; }
        IUserIdIterator Iterator { get; }
        IValidator Validator { get; }
        

        int Add(User user);

        void Delete(User user);

        IEnumerable<User> GetAll();

        int[] SearchForUsers(Func<User, bool> criteria);

        void Save();

        void Load();
    }
}
