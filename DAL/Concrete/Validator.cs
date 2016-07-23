using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class Validator : IValidator
    {
        public bool Validate(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            if (user.FirstName == "Fool" || user.LastName == "Fool")
                return false;
            return true;
        }
    }
}
