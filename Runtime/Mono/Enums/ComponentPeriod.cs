using System;

namespace InitialFramework
{
    /// <summary>  
    /// 表示单个生命周期方法，这些方法是游戏对象或组件在其生命周期中只执行一次的。  
    /// </summary>  
    public enum SinglePeriod : byte
    {
        /// <summary>  
        /// 表示对象或组件在唤醒后立即调用的方法。  
        /// </summary>  
        Awake,
        /// <summary>  
        /// 表示在脚本实例被创建时自动调用的初始化方法。  
        /// </summary>  
        Start,
    }

    /// <summary>  
    /// 使用 Flags 属性表示可以通过位运算组合多个 <see cref="SinglePeriod"/> 枚举值。  
    /// </summary>  
    [Flags]
    public enum SinglePeriodFlags : byte
    {
        /// <summary>  
        /// 表示对象或组件在唤醒后立即调用的方法。  
        /// </summary>  
        Awake = 1,
        /// <summary>  
        /// 表示在脚本实例被创建时自动调用的初始化方法。  
        /// </summary>  
        Start = 2,
    }

    /// <summary>  
    /// 表示在游戏循环中可能重复调用的生命周期方法。  
    /// </summary>  
    public enum RepeatedlyPeriod : byte
    {
        /// <summary>  
        /// 每帧调用一次，用于更新游戏状态。  
        /// </summary>  
        Update,
        /// <summary>  
        /// 以固定的时间间隔调用，通常用于物理计算。  
        /// </summary>  
        FixedUpdate,
        /// <summary>  
        /// 在所有 Update 方法调用之后调用，用于最后的游戏状态更新。  
        /// </summary>  
        LateUpdate,
    }

    /// <summary>  
    /// 使用 Flags 属性表示可以通过位运算组合多个 <see cref="RepeatedlyPeriod"/> 枚举值。  
    /// </summary>  
    [Flags]
    public enum RepeatedlyPeriodFlags : byte
    {
        /// <summary>  
        /// 每帧调用一次，用于更新游戏状态。  
        /// </summary>  
        Update = 1,
        /// <summary>  
        /// 以固定的时间间隔调用，通常用于物理计算。  
        /// </summary>  
        FixedUpdate = 2,
        /// <summary>  
        /// 在所有 Update 方法调用之后调用，用于最后的游戏状态更新。  
        /// </summary>  
        LateUpdate = 4,
    }

    /// <summary>  
    /// 所有可能的生命周期方法，包括单次调用和重复调用的方法。  
    /// </summary>  
    public enum Period : byte
    {
        /// <summary>  
        /// 对象或组件在唤醒后立即调用的方法。  
        /// </summary>  
        Awake,
        /// <summary>  
        /// 在脚本实例被创建时自动调用的初始化方法。  
        /// </summary>  
        Start,
        /// <summary>  
        /// 每帧调用一次，用于更新游戏状态。  
        /// </summary>  
        Update,
        /// <summary>  
        /// 以固定的时间间隔调用，通常用于物理计算。  
        /// </summary>  
        FixedUpdate,
        /// <summary>  
        /// 在所有 Update 方法调用之后调用，用于最后的游戏状态更新。  
        /// </summary>  
        LateUpdate
    }

    /// <summary>  
    /// 使用 Flags 属性表示可以通过位运算组合多个 <see cref="Period"/> 枚举值。  
    /// </summary>  
    [Flags]
    public enum PeriodFlags : byte
    {
        /// <summary>  
        /// 对象或组件在唤醒后立即调用的方法。  
        /// </summary>  
        Awake = 1,
        /// <summary>  
        /// 在脚本实例被创建时自动调用的初始化方法。  
        /// </summary>  
        Start = 2,
        /// <summary>  
        /// 每帧调用一次，用于更新游戏状态。  
        /// </summary>  
        Update = 4,
        /// <summary>  
        /// 以固定的时间间隔调用，通常用于物理计算。  
        /// </summary>  
        FixedUpdate = 8,
        /// <summary>  
        /// 在所有 Update 方法调用之后调用，用于最后的游戏状态更新。  
        /// </summary>  
        LateUpdate = 16
    }
}