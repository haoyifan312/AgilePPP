using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Classifications;
using Payroll.Database;
using Payroll.Methods;
using Payroll.Schedules;

namespace Payroll.Transactions
{
    abstract class AddEmployeeTransaction : Transaction
    {
        private int _empId;
        private string _address;
        private string _name;

        protected abstract Classification GetClassification();
        protected abstract Schedule GetSchedule();

        public AddEmployeeTransaction(int empid, string name, string address)
        {
            _empId = empid;
            _address = address;
            _name = name;
        }

        public void Execute()
        {
            var employee = new Employee(_name, _address);

            var classification = GetClassification();
            employee.ItsClassification = classification;

            var schedule = GetSchedule();
            employee.ItsSchedule = schedule;

            employee.ItsPaymentMethod = GetPaymentMethod();

            PayrollDatabase.GetInstance().AddEmployee(_empId, employee);
        }

        virtual protected PaymentMethod GetPaymentMethod()
        {
            return new HoldMethod(_address);
        }
    }
}
