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

        public void Delete(User user)
        {
            this.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return this.Users.AsReadOnly();
        }

        public int[] SearchForUsers(Func<User, bool> criteria)
        {
            var results = this.Users.Where(criteria).ToList();
            return results.Select(u => u.Id).ToArray();
        }

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