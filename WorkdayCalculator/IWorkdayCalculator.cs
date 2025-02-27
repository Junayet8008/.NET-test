﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkdayCalculator
{
    public interface IWorkdayCalculator
    {
        void SetHoliday(DateTime date);
        void SetRecurringHoliday(int month, int day);
        void SetWorkdayStartAndStop(int startHours, int startMinutes, int stopHours, int stopMinutes);
        DateTime GetWorkdayIncrement(DateTime startDate, decimal incrementInWorkdays);
    }
}
