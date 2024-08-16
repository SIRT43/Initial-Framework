using System;

namespace FTGAMEStudio.InitialFramework.Math
{
    public enum Vector2Axis : byte
    {
        x,
        y
    }

    [Flags]
    public enum Vector2AxisFlags : byte
    {
        x = 1,
        y = 2
    }

    public enum Vector3Axis : byte
    {
        x,
        y,
        z
    }

    [Flags]
    public enum Vector3AxisFlags : byte
    {
        x = 1,
        y = 2,
        z = 4
    }

    public enum Vector4Axis : byte
    {
        x,
        y,
        z,
        w
    }

    [Flags]
    public enum Vector4AxisFlags : byte
    {
        x = 1,
        y = 2,
        z = 4,
        w = 16
    }

    public enum QuaternionAxis : byte
    {
        w,
        x,
        y,
        z
    }

    [Flags]
    public enum QuaternionAxisFlags : byte
    {
        w = 1,
        x = 2,
        y = 4,
        z = 16
    }
}
