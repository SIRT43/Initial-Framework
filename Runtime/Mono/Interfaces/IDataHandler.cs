namespace FTGAMEStudio.InitialFramework
{
    public interface IResetable
    {
        public void Reset();
    }

    public interface IClearable
    {
        public void Clear();
    }

    public interface IDataHandler : IResetable, IClearable { }
}