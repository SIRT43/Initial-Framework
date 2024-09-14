using System;

namespace InitialFramework
{
    public enum Month : byte
    {
        /// <summary>
        /// һ�¡�
        /// </summary>
        January = 1,
        /// <summary>
        /// ���¡�
        /// </summary>
        February,
        /// <summary>
        /// ���¡�
        /// </summary>
        March,
        /// <summary>
        /// ���¡�
        /// </summary>
        April,
        /// <summary>
        /// ���¡�
        /// </summary>
        May,
        /// <summary>
        /// ���¡�
        /// </summary>
        June,
        /// <summary>
        /// ���¡�
        /// </summary>
        July,
        /// <summary>
        /// ���¡�
        /// </summary>
        August,
        /// <summary>
        /// ���¡�
        /// </summary>
        September,
        /// <summary>
        /// ʮ�¡�
        /// </summary>
        October,
        /// <summary>
        /// ʮһ�¡�
        /// </summary>
        November,
        /// <summary>
        /// ʮ���¡�
        /// </summary>
        December
    }

    [Flags]
    public enum MonthFlags
    {
        /// <summary>
        /// һ�¡�
        /// </summary>
        January = 1,
        /// <summary>
        /// ���¡�
        /// </summary>
        February = 2,
        /// <summary>
        /// ���¡�
        /// </summary>
        March = 4,
        /// <summary>
        /// ���¡�
        /// </summary>
        April = 8,
        /// <summary>
        /// ���¡�
        /// </summary>
        May = 16,
        /// <summary>
        /// ���¡�
        /// </summary>
        June = 32,
        /// <summary>
        /// ���¡�
        /// </summary>
        July = 64,
        /// <summary>
        /// ���¡�
        /// </summary>
        August = 128,
        /// <summary>
        /// ���¡�
        /// </summary>
        September = 256,
        /// <summary>
        /// ʮ�¡�
        /// </summary>
        October = 512,
        /// <summary>
        /// ʮһ�¡�
        /// </summary>
        November = 1024,
        /// <summary>
        /// ʮ���¡�
        /// </summary>
        December = 2048
    }
}
