using Payroll.Classifications;
using Payroll.Schedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        public double Salary { get; set; }
        public double CommissionRate { get; set; }

        public ChangeCommissionedTransaction(int empId, double salary, double commissionRate):
            base(empId)
        {
            Salary = salary;
            CommissionRate = commissionRate;
        }

        protected override Classification GetClassification()
        {
            return new CommissionedClassification(Salary, CommissionRate);
        }

        protected override Schedule GetSchedule()
        {
            return new BiweeklySchedule();
        }
    }
}
