using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal interface Schedule
    {
        public bool IsPayDay(DateOnly date);
    }
}
