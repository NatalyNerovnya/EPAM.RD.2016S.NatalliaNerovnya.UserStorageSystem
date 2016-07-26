using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IMaster : IRole
    {
        event Action ActionOnAdd;
        event Action ActionOnDelete;
        int NumberOfSlaves { get; set; }


    }
}
