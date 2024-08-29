using System;

namespace FTGAMEStudio.InitialFramework
{
    public interface IStatusable<T> where T : Enum
    {
        T State { get; }
    }
}
