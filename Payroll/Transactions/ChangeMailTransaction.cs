using Payroll.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal class ChangeMailTransaction : ChangeMethodTransaction
    {
        public string Address { get; set; }

        public ChangeMailTransaction(int empId, string address):
            base(empId)
        {
            Address = address;
        }

        protected override PaymentMethod GetMethod()
        {
            return new MailMethod(Address);
        }
    }
}
