using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class MonthlySchedule : Schedule
    {
        public bool IsPayDay(DateOnly date)
        {
            return DateOnlyUtils.IsLastDayOfMonth(date);
        }
    }
}
