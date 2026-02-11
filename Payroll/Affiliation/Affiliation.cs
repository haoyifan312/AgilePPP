using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal interface Affiliation
    {
        internal double CalculateDeductions(PayCheck pc);
    }

    internal class NoAffiliation : Affiliation
    {
        double Affiliation.CalculateDeductions(PayCheck pc)
        {
            return 0.0;
        }
    }
}
