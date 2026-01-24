using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class Employee
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        public Classification? ItsClassification { get; set; }
        public Schedule? ItsSchedule { get; set; }
        public PaymentMethod? ItsPaymentMethod{ get; set;}

        public Employee(string name, string address) 
        {
            Name = name;
            Address = address;
        }


    }
}
