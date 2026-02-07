using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
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
