namespace DAL.Concrete
{
    using DAL.Entities;
    using DAL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using System.Configuration;
    using System.IO;

    public class UserRepository : MarshalByRefObject, IUserRepository
    {
        public List<User> Users { get; set; }

        public IUserIdIterator Iterator { get; private set; }

        public IValidator Validator { get; private set; }

        public UserRepository(IUserIdIterator iterator = null, IValidator val = null )
        {
            Iterator = iterator == null ? new UserIdIterator() : iterator;
            Iterator.MakeGenerator();
            Validator = val == null ? new Validator() : val;
            Users = new List<User>();
        }

        public int Add(User user)
        {
            if (Validator.Validate(user))
            {
                user.Id = Iterator.GetUserId();
                Users.Add(user);
                return user.Id;
            }
            return -1;
        }

        public void Delete(User user)
        {
            Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return Users.AsReadOnly();
        }

        public int[] SearchForUsers(Func<User, bool> criteria)
        {
            var results = Users.Where(criteria).ToList();
            return results.Select(u => u.Id).ToArray();
        }

        public void Save()
        {
            XmlSerializer foramtter = new XmlSerializer(typeof(List<User>));
            string path = ConfigurationSettings.AppSettings["path"];
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                foramtter.Serialize(stream, Users);
            }
        }

        public void Load()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            using (StreamReader sr = new StreamReader(ConfigurationSettings.AppSettings["path"]))
            {
                List<User> users = (List<User>)formatter.Deserialize(sr);
                foreach (var user in users)
                {
                    this.Users.Add(user);
                }
                Iterator.MakeGenerator(Users == null ? 0: Users.Last().Id);

            }

        }
    }
}

