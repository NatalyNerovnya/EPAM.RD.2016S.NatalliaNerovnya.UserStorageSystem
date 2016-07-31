namespace DAL.Interfaces
{
    using System.Collections.Generic;

    public interface IUserIdIterator
    {
        int GetUserId();

        IEnumerator<int> MakeGenerator(int initialValue = 0);
    }
}
