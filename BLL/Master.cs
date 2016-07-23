using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class Master : UserService, IMaster
    {
        private static Master instance;
        public event Action ActionOnAdd = delegate { };
        public event Action ActionOnDelete = delegate { };

        public int NumberOfSlaves { get; set; }
        public IUserRepository GetRepository()
        {
            return repository;
        }

        private Master()
        {
            NumberOfSlaves = Int32.Parse(ConfigurationSettings.AppSettings["slavesNumber"]);
        }

        public static Master GetInstance()
        {
            return instance ?? (instance = new Master());
        }

        public override int Add(BllUser user)
        {
            var id = repository.Add(user.ToDalUser());
            ActionOnAdd();
            return id;
        }

        public override void Delete(BllUser user)
        {
            repository.Delete(user.ToDalUser());
            ActionOnDelete();
        }
        
    }
}
