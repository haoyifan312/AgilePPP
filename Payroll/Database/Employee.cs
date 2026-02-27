using Payroll.Classifications;
using Payroll.Methods;
using Payroll.Schedules;
using Payroll.Affiliations;

using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Database
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

        public bool IsPayDay(DateOnly date)
        {
            if (null == ItsSchedule)
                throw new Exception($"Schedule for {Name} is not setup");
            return ItsSchedule.IsPayDay(date);
        }

        internal void Payday(PayCheck pc)
        {
            if (null == ItsClassification)
                throw new Exception($"Classification for {Name} is not setup");
            double grossPay = ItsClassification.CalculatePay(pc);
            double deductions = ItsAffiliation.CalculateDeductions(pc);
            pc.GrossPay = grossPay;
            pc.Deductions = deductions;
        }

        internal DateOnly GetPaymentBeginDate(DateOnly payDay)
        {
            if (null == ItsSchedule)
                throw new Exception($"Schedule for {Name} is not setup");

            return ItsSchedule.GetPayBeginDate(payDay);
        }
    }
}
