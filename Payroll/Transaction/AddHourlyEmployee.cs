using Payroll.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transaction
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
