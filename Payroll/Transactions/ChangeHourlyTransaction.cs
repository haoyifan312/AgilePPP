using Payroll.Classifications;
using Payroll.Schedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal class ChangeHourlyTransaction : ChangeClassificationTransaction
    {
        public double HourlyRate { get; set; }

        public ChangeHourlyTransaction(int empId, double hourlyRate):
            base(empId)
        {
            HourlyRate = hourlyRate;
        }

        protected override Classification GetClassification()
        {
            return new HourlyClassification(HourlyRate);
        }

        protected override Schedule GetSchedule()
        {
            return new WeeklySchedule();
        }
    }
}
