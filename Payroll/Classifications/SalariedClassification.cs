using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Classifications
{
    internal class SalariedClassification : Classification
    {
        public double Salary { get; set; }
        public SalariedClassification(double salary) 
        {
            Salary = salary;
        }

        double Classification.CalculatePay(PayCheck pc)
        {
            return Salary;
        }
    }
}
