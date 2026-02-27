using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Methods
{
    internal class MailMethod : PaymentMethod
    {
        public string Address { get; set; }

        public MailMethod(string address)
        {
            Address = address;
        }
    }
}
