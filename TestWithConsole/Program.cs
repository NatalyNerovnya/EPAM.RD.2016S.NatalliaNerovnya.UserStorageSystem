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

            InitGroup.InitializeGroup();
            var master = InitGroup.Master as Master;
            var sl1 = InitGroup.Slaves[0] as Slave;
            var sl2 = InitGroup.Slaves[1] as Slave;

            master.Add(new BllUser()
            {
                FirstName = "Nata",
                LastName = "Nerovnya",
                BirthDate = new DateTime(1995, 3, 29),
                Gender = Gender.Female
            }
                );
            master.Add(new BllUser()
            {
                FirstName = "Natallia",
                LastName = "Nerovnya",
                BirthDate = new DateTime(1995, 3, 29),
                Gender = Gender.Female
            }
                );
            master.Add(new BllUser()
            {
                FirstName = "Natasha",
                LastName = "Nerovnya",
                BirthDate = new DateTime(1995, 3, 29),
                Gender = Gender.Female
            }
                );

            var users = sl1.GetAllUsers();
            

            //master.GetAllUsers();
            //master.Add(new BllUser()
            //{
            //    FirstName = "Nata",
            //    LastName = "Nerovnya",
            //    BirthDate = new DateTime(1995, 3, 29),
            //    Gender = Gender.Female
            //}
            //    );
            //var all = sl1.GetAllUsers();
            //var nataId = sl2.SearchForUsers(u => u.FirstName == "Nata").FirstOrDefault();
            //master.Add(new BllUser()
            //{
            //    FirstName = "Natallia",
            //    LastName = "Nerovnya",
            //    BirthDate = new DateTime(1995, 3, 29),
            //    Gender = Gender.Female
            //}
            //    );
            //master.Add(new BllUser()
            //{
            //    FirstName = "Natasha",
            //    LastName = "Nerovnya",
            //    BirthDate = new DateTime(1995, 3, 29),
            //    Gender = Gender.Female
            //}
            //    );
            //all = sl2.GetAllUsers();
        }
    }
}
