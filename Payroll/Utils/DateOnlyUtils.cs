using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class DateOnlyUtils
    {
        static public bool IsLastDayOfMonth(DateOnly date)
        {
            return date.Day == DateTime.DaysInMonth(date.Year, date.Month);
        }
    }
}
