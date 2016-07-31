namespace BLL.Interfaces
{
    public interface ISlave : IRole
    {
        IMaster Master { get; }

        void Update();
    }
}
