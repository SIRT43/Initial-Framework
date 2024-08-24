namespace FTGAMEStudio.InitialFramework.Collections.Liner
{
    public interface ILinerCollection<T>
    {
        public void Push(T value);

        public T Pop();
        public bool TryPop(out T result);

        public T Peek();
        public bool TryPeek(out T result);
    }
}
