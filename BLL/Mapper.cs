using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Mapper
    {
        public static User ToDalUser(this BllUser user)
        {
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = (DAL.Entities.Gender)user.Gender,
                BirthDate = user.BirthDate,
                VisaRecords = user.VisaRecords?.Select(v => v.ToDalVisa()).ToList()
            };
        }

        public static BllUser ToBllUser(this User user)
        {
            return new BllUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = (Entities.Gender)user.Gender,
                BirthDate = user.BirthDate,
                VisaRecords = user.VisaRecords?.Select(v => v.ToBllVisa()).ToList()
            };
        }

        public static Visa ToDalVisa(this BllVisa visa)
        {
            return new Visa {
                Country = visa.Country,
                End = visa.End,
                Start = visa.Start
            };
        }

        public static BllVisa ToBllVisa(this Visa visa)
        {
            return new BllVisa
            {
                Country = visa.Country,
                End = visa.End,
                Start = visa.Start
            };
        }


    }
}
