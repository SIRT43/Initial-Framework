namespace FTGAMEStudio.InitialFramework
{
    public interface IResetable
    {
        void Reset();
    }

    public interface IClearable
    {
        void Clear();
    }

    public interface IDataHandler : IResetable, IClearable { }
}