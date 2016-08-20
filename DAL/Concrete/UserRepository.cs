namespace DAL.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Entities;
    using Interfaces;

    /// <summary>
    /// Storage of users ( in memory)
    /// </summary>
    public class UserRepository : MarshalByRefObject, IUserRepository
    {
        public UserRepository(IUserIdIterator iterator = null, IValidator val = null)
        {
            this.Iterator = iterator == null ? new UserIdIterator() : iterator;
            this.Iterator.MakeGenerator();
            this.Validator = val ?? new Validator();
            this.Users = new List<User>();
        }
        
        public List<User> Users { get; set; }

        public IUserIdIterator Iterator { get; private set; }

        public IValidator Validator { get; private set; }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="user">user to be add</param>
        /// <returns>user id or -1 if not validated</returns>
        public int Add(User user)
        {
            if (this.Validator.Validate(user))
            {
                user.Id = this.Iterator.GetUserId();
                this.Users.Add(user);
                return user.Id;
            }

            return -1;
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user">user to be deleted</param>
        public void Delete(User user)
        {
            this.Users.Remove(user);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Ienumerable collection of all users</returns>
        public IEnumerable<User> GetAll()
        {
            return this.Users.AsReadOnly();
        }

        /// <summary>
        /// Not optimized searching by criteria
        /// </summary>
        /// <param name="criteria">Func delegate</param>
        /// <returns></returns>
        public int[] SearchForUsers(Func<User, bool> criteria)
        {
            var results = this.Users.Where(criteria).ToList();
            return results.Select(u => u.Id).ToArray();
        }

        /// <summary>
        /// Save all users in xml file
        /// </summary>
        public void Save()
        {
            XmlSerializer foramtter = new XmlSerializer(typeof(List<User>));
#pragma warning disable 618
            string path = ConfigurationSettings.AppSettings["path"];
#pragma warning restore 618
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                foramtter.Serialize(stream, this.Users);
            }
        }

        /// <summary>
        /// Load users from xml file
        /// </summary>
        public void Load()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
#pragma warning disable 618
            using (StreamReader sr = new StreamReader(ConfigurationSettings.AppSettings["path"]))
#pragma warning restore 618
            {
                List<User> users = (List<User>)formatter.Deserialize(sr);
                foreach (var user in users)
                {
                    this.Users.Add(user);
                }

                this.Iterator.MakeGenerator(this.Users?.Last().Id ?? 0);
            }
        }
    }
}