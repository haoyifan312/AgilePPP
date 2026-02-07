using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class ChangeHoldTransaction : ChangeMethodTransaction
    {
        public string Address { get; set; }

        public ChangeHoldTransaction(int empId, string address):
            base(empId)
        {
            Address = address;
        }

        protected override PaymentMethod GetMethod()
        {
            return new HoldMethod(Address);
        }
    }
}
