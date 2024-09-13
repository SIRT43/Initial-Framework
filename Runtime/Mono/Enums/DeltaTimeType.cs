using System;

namespace InitialFramework
{
    public enum DeltaTimeType
    {
        DeltaTime,
        FixedDeltaTime
    }

    [Flags]
    public enum DeltaTimeTypeFlags
    {
        DeltaTime = 1,
        FixedDeltaTime = 2
    }
}
