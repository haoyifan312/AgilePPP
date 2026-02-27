using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Schedules
{
    internal interface Schedule
    {
        DateOnly GetPayBeginDate(DateOnly payDay);
        public bool IsPayDay(DateOnly date);
    }
}
