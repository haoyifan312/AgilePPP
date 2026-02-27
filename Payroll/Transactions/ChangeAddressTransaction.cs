using Payroll.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal class ChangeAddressTransaction : ChangeEmployeeTransaction
    {
        public string Address { get; set; }

        public ChangeAddressTransaction(int empId, string address):
            base(empId)
        {
            Address = address;
        }

        protected override void Change(Employee e)
        {
            e.Address = Address;
        }
    }
}
