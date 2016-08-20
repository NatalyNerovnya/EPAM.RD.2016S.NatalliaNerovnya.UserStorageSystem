namespace BLL
{
    using System;
    using System.Configuration;
    using Entities;
    using Interfaces;
    using NLog;

    /// <summary>
    /// Master service
    /// </summary>
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

        /// <summary>
        /// Return Master reference
        /// </summary>
        /// <returns>reference on master</returns>
        public static Master GetInstance()
        {
           Logger.Trace("GetInstance()");
            return instance ?? (instance = new Master());            
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="user">User to be add</param>
        /// <returns>id of new user</returns>
        public override int Add(BllUser user)
        {
            var id = repository.Add(user.ToDalUser());
            this.ActionOnAdd();
            Communicator?.SendAdd();
            Logger.Trace("Add user with id " + id.ToString());
            return id;
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user">User to be deleted</param>
        public override void Delete(BllUser user)
        {
            Logger.Trace("Delete user with id " + user.Id.ToString());
            repository.Delete(user.ToDalUser());
            this.ActionOnDelete();
            Communicator?.SendDelete();
        }
    }
}
