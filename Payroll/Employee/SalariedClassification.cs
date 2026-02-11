using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
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
