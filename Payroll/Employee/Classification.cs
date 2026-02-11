using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal interface Classification
    {
        internal double CalculatePay(PayCheck pc);
    }
}
