using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Schedules
{
    internal class BiweeklySchedule : Schedule
    {
        public DateOnly GetPayBeginDate(DateOnly payDay)
        {
            int daysAgo = 2 * 7 - 1;
            return payDay.AddDays(-daysAgo);
        }

        public bool IsPayDay(DateOnly date)
        {
            return false;
        }
    }
}
