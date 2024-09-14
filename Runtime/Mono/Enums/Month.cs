using System;

namespace InitialFramework
{
    public enum Month : byte
    {
        /// <summary>
        /// 一月。
        /// </summary>
        January = 1,
        /// <summary>
        /// 二月。
        /// </summary>
        February,
        /// <summary>
        /// 三月。
        /// </summary>
        March,
        /// <summary>
        /// 四月。
        /// </summary>
        April,
        /// <summary>
        /// 五月。
        /// </summary>
        May,
        /// <summary>
        /// 六月。
        /// </summary>
        June,
        /// <summary>
        /// 七月。
        /// </summary>
        July,
        /// <summary>
        /// 八月。
        /// </summary>
        August,
        /// <summary>
        /// 九月。
        /// </summary>
        September,
        /// <summary>
        /// 十月。
        /// </summary>
        October,
        /// <summary>
        /// 十一月。
        /// </summary>
        November,
        /// <summary>
        /// 十二月。
        /// </summary>
        December
    }

    [Flags]
    public enum MonthFlags
    {
        /// <summary>
        /// 一月。
        /// </summary>
        January = 1,
        /// <summary>
        /// 二月。
        /// </summary>
        February = 2,
        /// <summary>
        /// 三月。
        /// </summary>
        March = 4,
        /// <summary>
        /// 四月。
        /// </summary>
        April = 8,
        /// <summary>
        /// 五月。
        /// </summary>
        May = 16,
        /// <summary>
        /// 六月。
        /// </summary>
        June = 32,
        /// <summary>
        /// 七月。
        /// </summary>
        July = 64,
        /// <summary>
        /// 八月。
        /// </summary>
        August = 128,
        /// <summary>
        /// 九月。
        /// </summary>
        September = 256,
        /// <summary>
        /// 十月。
        /// </summary>
        October = 512,
        /// <summary>
        /// 十一月。
        /// </summary>
        November = 1024,
        /// <summary>
        /// 十二月。
        /// </summary>
        December = 2048
    }
}
