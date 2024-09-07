using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// 可序列化的日期时间表达方案。
    /// 
    /// <para>它可以与 <see cref="DateTime"/> 兼容。</para>
    /// </summary>
    [Serializable]
    public struct Timepoint
    {
        public static bool IsLeapYear(int year) => ((year % 4) == 0 && (year % 100) != 0) || (year % 400) == 0;
        public static bool IsLeapYear(Timepoint timepoint) => IsLeapYear(timepoint.year);


        public static implicit operator Timepoint(DateTime dateTime) =>
            new(dateTime.Year, (Month)dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        public static implicit operator DateTime(Timepoint timepoint) =>
            new(timepoint.Year, (int)timepoint.Month, timepoint.Day, timepoint.Hour, timepoint.Minute, timepoint.Second);


        public static Timepoint Timestamp => new(1970, Month.January, 1, 0, 0, 0);



        [SerializeField, Min(1)] private int year;
        [SerializeField] private Month month;
        [SerializeField, Min(1)] private int day;

        [SerializeField, Range(0, 23)] private int hour;
        [SerializeField, Range(0, 59)] private int minute;
        [SerializeField, Range(0, 59)] private int second;

        public int Year
        {
            readonly get => year;
            set => year = IFMath.ClampMin(value, 1);
        }

        public readonly bool LeapYear => IsLeapYear(Year);


        public Month Month
        {
            readonly get => month;
            set => month = value;
        }


        public int Day
        {
            readonly get => day;
            set
            {
                switch (Month)
                {
                    case Month.January:
                    case Month.March:
                    case Month.May:
                    case Month.July:
                    case Month.August:
                    case Month.October:
                    case Month.December:
                        value = Mathf.Clamp(value, 1, 31);
                        break;
                    case Month.April:
                    case Month.June:
                    case Month.September:
                    case Month.November:
                        value = Mathf.Clamp(value, 1, 30);
                        break;
                    case Month.February:
                        value = Mathf.Clamp(value, 1, LeapYear ? 29 : 28);
                        break;
                }
                day = value;
            }
        }


        public int Hour
        {
            readonly get => hour;
            set => hour = Mathf.Clamp(value, 0, 23);
        }

        public int Minute
        {
            readonly get => minute;
            set => minute = Mathf.Clamp(value, 0, 59);
        }

        public int Second
        {
            readonly get => second;
            set => second = Mathf.Clamp(value, 0, 59);
        }


        public void ToggleYear() => Year++;

        public void ToggleMonth()
        {
            Month++;

            if (Month != Month.December) return;

            ToggleYear();
            Month = Month.January;
        }

        public void ToggleDay()
        {
            Day++;

            bool toggleMonth = false;

            switch (Month)
            {
                case Month.January:
                case Month.March:
                case Month.May:
                case Month.July:
                case Month.August:
                case Month.October:
                case Month.December:
                    toggleMonth = Day > 31;
                    break;
                case Month.April:
                case Month.June:
                case Month.September:
                case Month.November:
                    toggleMonth = Day > 30;
                    break;
                case Month.February:
                    toggleMonth = Day > (LeapYear ? 29 : 28);
                    break;
            }

            if (!toggleMonth) return;

            ToggleMonth();
            Day = 1;
        }

        public void ToggleHour()
        {
            Hour++;

            if (Hour < 23) return;

            ToggleDay();
            Hour = 0;
        }

        public void ToggleMinute()
        {
            Minute++;

            if (Minute < 59) return;

            ToggleHour();
            Minute = 0;
        }

        public void ToggleSecond()
        {
            Second++;

            if (Second < 59) return;

            ToggleMinute();
            Second = 0;
        }


        public Timepoint(int year, Month month, int day, int hour, int minute, int second) : this()
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
