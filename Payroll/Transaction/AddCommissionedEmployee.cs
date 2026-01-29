using Payroll.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class AddCommissionedEmployee: AddEmployeeTransaction
    {
        private double _salary;
        private double _commissionRate;

        public AddCommissionedEmployee(int empid, string address, string name, 
            double salary, double commisionRate) : 
            base(empid, address, name)
        {
            _salary = salary;
            _commissionRate = commisionRate;
        }

        protected override Classification GetClassification()
        {
            return new CommissionedClassification(_salary, _commissionRate);
        }

        protected override Schedule GetSchedule()
        {
            return new BiweeklySchedule();
        }
    }
}
