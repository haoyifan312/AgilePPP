using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class ChangeDirectTransaction : ChangeMethodTransaction
    {
        public string Bank {  get; set; }
        public int Account {  get; set; }

        public ChangeDirectTransaction(int empId, string bank, int account):
            base(empId)
        {
            Bank = bank;
            Account = account;
        }

        protected override PaymentMethod GetMethod()
        {
            return new DirectMethod(Bank, Account);
        }
    }
}
