namespace BLL
{
    using System;
    using System.Configuration;
    using Entities;
    using Interfaces;
    using NLog;

    public class Master : UserService, IMaster
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static Master instance;

        private Master()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            this.NumberOfSlaves = int.Parse(ConfigurationSettings.AppSettings["slavesNumber"]);
#pragma warning restore CS0618 // Type or member is obsolete
            Logger.Trace("Create master");
        }
        
        public event Action ActionOnAdd = delegate { };

        public event Action ActionOnDelete = delegate { };

        public int NumberOfSlaves { get; set; }

        public static Master GetInstance()
        {
           Logger.Trace("GetInstance()");
            return instance ?? (instance = new Master());            
        }

        public override int Add(BllUser user)
        {
            var id = repository.Add(user.ToDalUser());
            this.ActionOnAdd();
            Logger.Trace("Add user with id " + id.ToString());
            return id;
        }

        public override void Delete(BllUser user)
        {
            Logger.Trace("Delete user with id " + user.Id.ToString());
            repository.Delete(user.ToDalUser());
            this.ActionOnDelete();
        }
    }
}
