using System;

namespace InitialFramework
{
    public enum MemberType
    {
        Field,
        Property,
        Method,
        Variable,
    }

    [Flags]
    public enum MemberTypeFlags
    {
        Field = 1,
        Property = 2,
        Method = 4,
        Variable = 8,
    }
}
