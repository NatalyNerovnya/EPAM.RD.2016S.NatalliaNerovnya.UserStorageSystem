using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Interfaces;
using DAL.Entities;

namespace BLL
{
    public class Slave : UserService, ISlave
    {
       public IMaster Master { get; }
        public Slave(IMaster master)
        {
            if (ReferenceEquals(master, null))
                throw new ArgumentNullException();
            Master = master;
            Master.ActionOnAdd += Update;
            Master.ActionOnDelete += Update;

            
        }


        public override int Add(BllUser user)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BllUser user)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            repository.Users = Master.GetRepository().Users; 
            Save();
        }
    }
}
