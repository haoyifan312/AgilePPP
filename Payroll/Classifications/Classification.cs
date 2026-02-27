using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Classifications
{
    internal interface Classification
    {
        internal double CalculatePay(PayCheck pc);
    }
}
