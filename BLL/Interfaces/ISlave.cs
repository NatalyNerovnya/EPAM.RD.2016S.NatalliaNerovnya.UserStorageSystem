using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface ISlave : IRole
    {
        IMaster Master { get; }
        void Update();
    }
}
