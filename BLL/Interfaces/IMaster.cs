using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace BLL.Interfaces
{
    public interface IMaster : IRole
    {
        event Action ActionOnAdd;
        event Action ActionOnDelete;

        IUserRepository GetRepository();
    }
}
