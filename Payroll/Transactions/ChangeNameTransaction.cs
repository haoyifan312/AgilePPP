using Payroll.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal class ChangeNameTransaction : ChangeEmployeeTransaction
    {
        public string Name { get; set; }

        public ChangeNameTransaction(int empId, string name):
            base(empId)
        {
            Name = name;
        }

        protected override void Change(Employee e)
        {
            e.Name = Name;
        }
    }
}
