using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class BiweeklySchedule : Schedule
    {
        public bool IsPayDay(DateOnly date)
        {
            return false;
        }
    }
}
