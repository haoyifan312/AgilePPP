using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        public double Salary {  get; set; }

        public ChangeSalariedTransaction(int empId, double salary):
            base(empId)
        {
            Salary = salary;
        }

        protected override Classification GetClassification()
        {
            return new SalariedClassification(Salary);
        }

        protected override Schedule GetSchedule()
        {
            return new MonthlySchedule();
        }
    }
}
