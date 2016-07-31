namespace BLL.Interfaces
{
    using System;

    public interface IMaster : IRole
    {
        event Action ActionOnAdd;

        event Action ActionOnDelete;

        int NumberOfSlaves { get; set; }
    }
}
