using UnityEngine;

namespace InitialFramework
{
    public abstract class TimepointBase
    {
        public static bool IsLeapYear(int year) => ((year % 4) == 0 && (year % 100) != 0) || (year % 400) == 0;
        public static bool IsLeapYear(TimepointBase timepoint) => IsLeapYear(timepoint.Year);



        [SerializeField] private int year;
        [SerializeField] private int month;
        [SerializeField] private int day;

        [SerializeField] private int hour;
        [SerializeField] private int minute;
        [SerializeField] private int second;



        public abstract bool LeapYear { get; }

        public abstract IntRange YearRange { get; }
        public abstract IntRange MonthRange { get; }
        public abstract IntRange DayRange { get; }
        public abstract IntRange HourRange { get; }
        public abstract IntRange MinuteRange { get; }
        public abstract IntRange SecondRange { get; }



        public int Year
        {
            get => year;
            set => year = YearRange.Clamp(value);
        }

        public int Month
        {
            get => month;
            set => month = MonthRange.Clamp(value);
        }

        public int Day
        {
            get => day;
            set => day = DayRange.Clamp(value);
        }

        public int Hour
        {
            get => hour;
            set => hour = HourRange.Clamp(value);
        }

        public int Minute
        {
            get => minute;
            set => minute = MinuteRange.Clamp(value);
        }

        public int Second
        {
            get => second;
            set => second = SecondRange.Clamp(value);
        }



        public void ToggleYear() => Year++;

        public void ToggleMonth()
        {
            Month++;

            if (Month < MonthRange.max) return;

            ToggleYear();
            Month = MonthRange.min;
        }

        public void ToggleDay()
        {
            Day++;

            if (Day < DayRange.max) return;

            ToggleMonth();
            Day = DayRange.min;
        }

        public void ToggleHour()
        {
            Hour++;

            if (Hour < HourRange.max) return;

            ToggleDay();
            Hour = HourRange.min;
        }

        public void ToggleMinute()
        {
            Minute++;

            if (Minute < MinuteRange.max) return;

            ToggleHour();
            Minute = MinuteRange.min;
        }

        public void ToggleSecond()
        {
            Second++;

            if (Second < SecondRange.max) return;

            ToggleMinute();
            Second = SecondRange.min;
        }
    }
}
