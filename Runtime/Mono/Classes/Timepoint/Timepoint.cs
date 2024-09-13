using System;

namespace InitialFramework
{
    /// <summary>
    /// 可序列化的日期时间表达方案。
    /// 
    /// <para>它可以与 <see cref="DateTime"/> 兼容。</para>
    /// </summary>
    [Serializable]
    public class Timepoint : TimepointBase
    {
        public static implicit operator Timepoint(DateTime dateTime) =>
            new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        public static implicit operator DateTime(Timepoint timepoint) =>
            new(timepoint.Year, timepoint.Month, timepoint.Day, timepoint.Hour, timepoint.Minute, timepoint.Second);


        public static Timepoint MinValue => new(1, 1, 1, 0, 0, 0);
        public static Timepoint MaxValue => new(9999, 12, 31, 23, 59, 59);
        public static Timepoint Timestamp => new(1970, 1, 1, 0, 0, 0);



        public override bool LeapYear => IsLeapYear(Year);

        public override IntRange YearRange => new(1, 9999);
        public override IntRange MonthRange => new(1, 12);
        public override IntRange DayRange => new(1, Month == 2 ? (LeapYear ? 29 : 28) : (Month is 4 or 6 or 9 or 11 ? 30 : 31));
        public override IntRange HourRange => new(0, 23);
        public override IntRange MinuteRange => new(0, 59);
        public override IntRange SecondRange => new(0, 59);



        public Timepoint(int year, int month, int day, int hour, int minute, int second)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
            Second = second;
        }
    }
}
