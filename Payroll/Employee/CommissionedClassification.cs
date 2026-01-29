using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class CommissionedClassification: Classification
    {
        public double Salary { set; get; }
        public double CommissionRate { set; get; }

        public CommissionedClassification(double salary, double commissionRate)
        {
            Salary = salary;
            CommissionRate = commissionRate;
        }
    }
}
