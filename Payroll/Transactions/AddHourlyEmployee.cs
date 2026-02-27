using Payroll.Classifications;
using Payroll.Schedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal class AddHourlyEmployee: AddEmployeeTransaction
    {
        private double _hourlyRate;

        public AddHourlyEmployee(int empId, string name, string address, double hourlyRate):
            base(empId, name, address)
        {
            _hourlyRate = hourlyRate;
        }

        protected override Classification GetClassification()
        {
            return new HourlyClassification(_hourlyRate);
        }

        protected override Schedule GetSchedule()
        {
            return new WeeklySchedule();
        }
    }
}
