using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Schedules
{
    internal class MonthlySchedule : Schedule
    {
        public DateOnly GetPayBeginDate(DateOnly payDay)
        {
            return new DateOnly(payDay.Year, payDay.Month, 1);
        }

        public bool IsPayDay(DateOnly date)
        {
            return DateOnlyUtils.IsLastDayOfMonth(date);
        }
    }
}
