using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class DirectMethod : PaymentMethod
    {
        public string Bank {  get; set; }
        public int Account {  get; set; }

        public DirectMethod(string bank, int account)
        {
            Bank = bank;
            Account = account;
        }
    }
}
