using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Interfaces;
using DAL.Interfaces;
using NLog;
using System.Diagnostics;

namespace BLL
{
    public class Master : UserService, IMaster
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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
            logger.Trace("Create master");
        }

        public static Master GetInstance()
        {
            logger.Trace("GetInstance()");
            return instance ?? (instance = new Master());
            
        }

        public override int Add(BllUser user)
        {
            var id = repository.Add(user.ToDalUser());
            ActionOnAdd();
            logger.Trace("Add user with id " + id.ToString());
            return id;
        }

        public override void Delete(BllUser user)
        {
            logger.Trace("Delete user with id " + user.Id.ToString());
            repository.Delete(user.ToDalUser());
            ActionOnDelete();
        }
        
    }
}
