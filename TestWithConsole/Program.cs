using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.Entities;

namespace TestWithConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Master master = Master.GetInstance();
            Slave sl1 = new Slave(master);
            Slave sl2 = new Slave(master);

            master.Add(new BllUser()
            {
                FirstName = "Nata",
                LastName = "Nerovnya",
                BirthDate = new DateTime(1995, 3, 29),
                Gender = Gender.Female
            }
                );

        }
    }
}
