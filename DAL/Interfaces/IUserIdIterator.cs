using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserIdIterator
    {
        int GetUserId();
        IEnumerator<int> MakeGenerator(int initialValue = 0);
    }
}
