namespace FTGAMEStudio.InitialFramework.Collections.Liner
{
    public interface ILinerCollection<T>
    {
        void Push(T value);

        T Pop();
        bool TryPop(out T result);

        T Peek();
        bool TryPeek(out T result);
    }
}
