using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class Employee
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public Affiliation ItsAffiliation { get; set; }

        public Classification? ItsClassification { get; set; }
        public Schedule? ItsSchedule { get; set; }
        public PaymentMethod? ItsPaymentMethod{ get; set;}

        public Employee(string name, string address) 
        {
            Name = name;
            Address = address;
            ItsAffiliation = new NoAffiliation();
        }
    }
}
