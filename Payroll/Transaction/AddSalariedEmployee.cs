using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Payroll
{
    internal class AddSalariedEmployee : AddEmployeeTransaction
    {
        private double _salary;


        public AddSalariedEmployee(int empId, string name, string address, double salary):
            base(empId, name, address)
        {
            _salary = salary;
        }

        protected override Classification GetClassification()
        {
            return new SalariedClassification(_salary);
        }

        protected override Schedule GetSchedule()
        {
            return new MonthlySchedule();
        }

    }
}
