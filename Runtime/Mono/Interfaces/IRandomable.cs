namespace InitialFramework
{
    public interface IRandomable<T> : IRange<T>
    {
        T Range();
    }
}
