using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class WeeklySchedule : Schedule
    {
        public bool IsPayDay(DateOnly date)
        {
            return date.DayOfWeek == DayOfWeek.Friday;
        }
    }
}
