namespace DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Entities;

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
