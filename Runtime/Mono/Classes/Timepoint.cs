using FTGAMEStudio.InitialFramework.Math;
using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework
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

    [Serializable]
    public struct Timepoint
    {
        [SerializeField, Min(1)] private int year;
        [SerializeField] private Month month;
        [SerializeField, Min(1)] private int day;

        [SerializeField, Range(0, 23)] private int hour;
        [SerializeField, Range(0, 59)] private int minute;
        [SerializeField, Range(0, 59)] private int second;

        public readonly bool IsLeapYear => ((Year % 4) == 0 && (Year % 100) != 0) || (Year % 400) == 0;

        public int Year
        {
            readonly get => year;
            set => year = IFMath.ClampMin(value, 1);
        }

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
                        value = Mathf.Clamp(value, 1, IsLeapYear ? 29 : 28);
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
            Month = Month++;

            if (Month == Month.December)
            {
                ToggleYear();
                Month = Month.January;
            }
        }

        public void ToggleDay()
        {
            day++;

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
                    toggleMonth = day > 31;
                    break;
                case Month.April:
                case Month.June:
                case Month.September:
                case Month.November:
                    toggleMonth = day > 30;
                    break;
                case Month.February:
                    toggleMonth = day > (IsLeapYear ? 29 : 28);
                    break;
            }

            if (toggleMonth) ToggleMonth();
        }

        public void ToggleHour()
        {
            hour++;
            if (hour > 23)
            {
                ToggleDay();
                hour = 0;
            }
        }

        public void ToggleMinute()
        {
            minute++;
            if (minute > 59)
            {
                ToggleHour();
                minute = 0;
            }
        }

        public void ToggleSecond()
        {
            second++;
            if (second > 59)
            {
                ToggleMinute();
                second = 0;
            }
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

        public static Timepoint Standard => new(1, Month.January, 1, 0, 0, 0);


        public static explicit operator Timepoint(DateTime dateTime) =>
            new(dateTime.Year, (Month)dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        public static explicit operator DateTime(Timepoint timepoint) =>
            new(timepoint.Year, (int)timepoint.Month, timepoint.Day, timepoint.Hour, timepoint.Minute, timepoint.Second);
    }
}
