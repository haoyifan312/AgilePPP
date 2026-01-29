using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class HourlyClassification: Classification
    {
        public double HourlyRate { get; set; }

        public HourlyClassification(double hourlyRate):
            base()
        {
            HourlyRate = hourlyRate;
        }
    }
}
