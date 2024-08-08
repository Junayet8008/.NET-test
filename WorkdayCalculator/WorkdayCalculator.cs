using System;
using System.Collections.Generic;

namespace WorkdayCalculator
{
    public class WorkdayCalculator : IWorkdayCalculator
    {
        private readonly HashSet<DateTime> _holidays = new HashSet<DateTime>();
        private readonly List<(int month, int day)> _recurringHolidays = new List<(int, int)>();
        private TimeSpan _workdayStart;
        private TimeSpan _workdayEnd;

        public void SetHoliday(DateTime date)
        {
            _holidays.Add(date.Date);
        }

        public void SetRecurringHoliday(int month, int day)
        {
            _recurringHolidays.Add((month, day));
        }

        public void SetWorkdayStartAndStop(int startHours, int startMinutes, int stopHours, int stopMinutes)
        {
            _workdayStart = new TimeSpan(startHours, startMinutes, 0);
            _workdayEnd = new TimeSpan(stopHours, stopMinutes, 0);
        }

        public DateTime GetWorkdayIncrement(DateTime startDate, decimal incrementInWorkdays)
        {
            if (startDate.TimeOfDay > _workdayEnd)
            {
                startDate = new DateTime(startDate.Year,startDate.Month,startDate.Day,_workdayEnd.Hours,_workdayEnd.Minutes, 0);
            }
            if (startDate.TimeOfDay < _workdayStart)
            {
                startDate = PreviousWorkdayEnd(startDate);
            }

            TimeSpan workdayDuration = _workdayEnd - _workdayStart;

            double totalMinutes = (double)incrementInWorkdays * workdayDuration.TotalMinutes;
            DateTime resultDate;
            if(totalMinutes == 0)
            {
                return startDate;
            }
            if (totalMinutes > 0)
            {
                resultDate = MoveForward(startDate, totalMinutes);
            }
            else
            {
                resultDate = MoveBackwards(startDate, -totalMinutes);
            }
            return resultDate;
        }

        private DateTime NextWorkdayStart(DateTime date)
        {
            DateTime nextDay = date.Date.AddDays(1);
            while (!IsWorkday(nextDay))
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay.Date + _workdayStart;
        }

        private DateTime PreviousWorkdayEnd(DateTime date)
        {
            DateTime prevDay = date.Date.AddDays(-1);
            while (!IsWorkday(prevDay))
            {
                prevDay = prevDay.AddDays(-1);
            }
            return prevDay.Date + _workdayEnd;
        }

        private DateTime MoveForward(DateTime startDate, double totalMinutes)
        {
            DateTime currentDate = startDate;
            while (totalMinutes > 0)
            {
                double remainingMinutesInDay = (_workdayEnd - currentDate.TimeOfDay).TotalMinutes;
                if (totalMinutes <= remainingMinutesInDay)
                {
                    return currentDate.AddMinutes(totalMinutes);
                }

                totalMinutes -= remainingMinutesInDay;
                currentDate = NextWorkdayStart(currentDate);
            }
            throw new InvalidOperationException("Unexpected state while moving forward.");
        }

        private DateTime MoveBackwards(DateTime startDate, double totalMinutes)
        {
            DateTime currentDate = startDate;
            while (totalMinutes > 0)
            {
                double elapsedMinutesInDay = (currentDate.TimeOfDay - _workdayStart).TotalMinutes;
                if (totalMinutes <= elapsedMinutesInDay)
                {
                    return currentDate.AddMinutes(-totalMinutes);
                }

                totalMinutes -= elapsedMinutesInDay;
                currentDate = PreviousWorkdayEnd(currentDate);
            }
            throw new InvalidOperationException("Unexpected state while moving backwards.");
        }

        private bool IsWorkday(DateTime date)
        {
            if (_holidays.Contains(date.Date))
            {
                return false;
            }

            var recurringHoliday = _recurringHolidays.Find(h => h.month == date.Month && h.day == date.Day);
            if (recurringHoliday != default)
            {
                return false;
            }

            // Check if it's a weekend
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
