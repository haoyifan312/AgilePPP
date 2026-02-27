using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Methods
{
    internal class HoldMethod : PaymentMethod
    {
        public string Address { get; set; }

        public HoldMethod(string address)
        {
            Address = address;
        }
    }
}
