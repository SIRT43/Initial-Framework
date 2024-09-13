using System;

namespace InitialFramework
{
    public interface IStatusable<T> where T : Enum
    {
        T State { get; }
    }
}
